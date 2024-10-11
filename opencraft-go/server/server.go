package server

import (
	"net"
	"time"

	"github.com/g3n/engine/math32"
	"github.com/jdonkervliet/opencraft-go/model"
	"github.com/jdonkervliet/opencraft-go/protos"
	log "github.com/sirupsen/logrus"
	"google.golang.org/protobuf/proto"
)

type IncomingMessage struct {
	Address *net.UDPAddr
	Data    *protos.ToServer
}

type ServerPlayer struct {
	model.Player
	controllers []*net.UDPAddr
}

func newServerPlayer() *ServerPlayer {
	p := &ServerPlayer{
		controllers: make([]*net.UDPAddr, 0),
	}
	p.Position = math32.Vector3{X: 2, Y: 2, Z: 2}
	return p
}

// A single game.
type Game struct {
	World        model.World
	Players      map[uint32]*ServerPlayer
	TickDuration time.Duration

	running bool
	msgBuf  chan *IncomingMessage
	s       *net.UDPConn
}

func NewGame() *Game {
	return &Game{
		World:        *model.NewWorld(),
		Players:      make(map[uint32]*ServerPlayer),
		TickDuration: 50 * time.Millisecond,
	}
}

// Update the game by one step.
func (g *Game) Update() {
	g.HandleMessages()
}

func (g *Game) IsRunning() bool {
	return g.running
}

func (g *Game) Stop() {
	g.running = false
}

func (g *Game) handleIWantPlayer(msg *protos.IWantPlayer, sender *net.UDPAddr) {
	var i uint32

	if msg.PlayerID == 0 {
		// Assign to new player with random ID
		for i = 1; i > 0; {
			_, ok := g.Players[i]
			if !ok {
				break
			}
			i++
		}
	} else {
		// Assign to player with given ID
		i = msg.PlayerID
	}

	p, ok := g.Players[i]
	if !ok {
		p = newServerPlayer()
		g.Players[i] = p
	}
	p.controllers = append(p.controllers, sender)

	loc := &protos.Vec3{X: p.Position.X, Y: p.Position.Y, Z: p.Position.Z}
	reply := &protos.ToClient{
		Payload: &protos.ToClient_YouArePlayer{
			YouArePlayer: &protos.YouArePlayer{
				PlayerID:      i,
				SpawnLocation: loc,
			},
		},
	}
	replyBytes, err := proto.Marshal(reply)
	if err != nil {
		log.Fatal(err)
	}
	if _, err := g.s.WriteToUDP(replyBytes, sender); err != nil {
		log.Fatal(err)
	}
}

func (g *Game) handleIWantMovePlayer(msg *protos.IWantMovePlayer, sender *net.UDPAddr) {
	p, ok := g.Players[msg.PlayerID]
	if ok {
		// No checks whatsoever!
		msgPos := msg.NewPosition
		newPos := math32.Vector3{X: msgPos.X, Y: msgPos.Y, Z: msgPos.Z}
		p.Position = newPos
	}
}

func (g *Game) handleIWantChangeBlock(msg *protos.IWantChangeBlock, sender *net.UDPAddr) {
	msgPos := msg.BlockPosition
	msgTyp := msg.BlockType
	pos := model.IntPos3{X: int(msgPos.X), Y: int(msgPos.Y), Z: int(msgPos.Z)}
	typ := uint8(msgTyp)
	if err := g.World.SetBlockType(pos, typ); err != nil {
		log.Warn(err)
	}
}

func (g *Game) handleIWantColumn(msg *protos.IWantColumn, sender *net.UDPAddr) {
	pos := &protos.Pos2{X: msg.ColumnPos.X, Z: msg.ColumnPos.Z}
	chunks := make([]*protos.ChunkData, 1)
	buf := make([]byte, 16*16*16)
	for i := 0; i < 16*16; i++ {
		buf[i] = 1
	}
	chunkData := &protos.ChunkData{BlockTypes: buf}
	chunks[0] = chunkData
	reply := &protos.ToClient{
		Payload: &protos.ToClient_ColumnData{
			ColumnData: &protos.ColumnData{
				Position: pos,
				Chunks:   chunks,
			},
		},
	}
	replyBytes, err := proto.Marshal(reply)
	if err != nil {
		log.Fatal(err)
	}
	if _, err := g.s.WriteToUDP(replyBytes, sender); err != nil {
		log.Fatal(err)
	}
}

func (g *Game) handleMessage(msg *IncomingMessage, s *net.UDPConn) {
	log.Info("received msg")
	switch x := msg.Data.Payload.(type) {
	case *protos.ToServer_IWantPlayer:
		g.handleIWantPlayer(x.IWantPlayer, msg.Address)
	case *protos.ToServer_IWantMovePlayer:
		g.handleIWantMovePlayer(x.IWantMovePlayer, msg.Address)
	case *protos.ToServer_IWantChangeBlock:
		g.handleIWantChangeBlock(x.IWantChangeBlock, msg.Address)
	case *protos.ToServer_IWantColumn:
		g.handleIWantColumn(x.IWantColumn, msg.Address)
	default:
		log.Warn("unknown msg type", x)
	}
}

func (g *Game) HandleMessages() {
	// Handle incoming packets.
	// Move on if there are no packets to receive.
	nMsgs := len(g.msgBuf)
	for i := 0; i < nMsgs; i++ {
		g.handleMessage(<-g.msgBuf, g.s)
	}
}

func (g *Game) Start() error {
	g.running = true

	udpAddr, err := net.ResolveUDPAddr("udp", "0.0.0.0:7979")
	if err != nil {
		return err
	}
	g.s, err = net.ListenUDP("udp", udpAddr)
	if err != nil {
		return err
	}

	g.msgBuf = make(chan *IncomingMessage, 1024)

	go func() {
		// Read from UDP listener in endless loop
		defer g.s.Close()
		for g.running {
			log.Infoln("running")
			buf := make([]byte, 512)
			n, addr, err := g.s.ReadFromUDP(buf[0:])
			if err != nil {
				log.Warn(err)
				return
			}
			log.Infof("read %v bytes", n)
			var msg protos.ToServer
			if err := proto.Unmarshal(buf[:n], &msg); err != nil {
				log.Warn(err)
				continue
			}

			log.Infof("got msg %v from %v", msg.String(), addr)

			iMsg := IncomingMessage{
				Address: addr,
				Data:    &msg,
			}
			g.msgBuf <- &iMsg
		}
	}()

	// go func() {
	// 	for g.running {
	// 		start := time.Now()

	// 		g.HandleMessages()

	// 		elapsed := time.Since(start)
	// 		sleepTime := g.TickDuration - elapsed
	// 		if sleepTime > 0 {
	// 			// log.Infof("sleeping %v\n", sleepTime)
	// 			time.Sleep(sleepTime)
	// 		} else {
	// 			log.Warn("server overloaded!")
	// 		}
	// 	}
	// }()

	return nil
}
