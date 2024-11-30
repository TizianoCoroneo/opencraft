package server

import (
	"bufio"
	"github.com/g3n/engine/math32"
	"github.com/jdonkervliet/opencraft-go/model"
	"github.com/jdonkervliet/opencraft-go/protos"
	log "github.com/sirupsen/logrus"
	"google.golang.org/protobuf/encoding/protodelim"
	"math/rand"
	"net"
	"time"
)

type IncomingMessage struct {
	Connection net.Conn
	Data       *protos.ToServer
}

type ServerPlayer struct {
	model.Player
	controllers []net.Conn
}

func newServerPlayer() *ServerPlayer {
	p := &ServerPlayer{
		controllers: make([]net.Conn, 0),
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
	s       net.Listener
}

func NewGame() *Game {
	rand.Seed(time.Now().UnixNano())

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

func (g *Game) handleIWantPlayer(msg *protos.IWantPlayer, conn net.Conn) {
	log.Info("processing I want player")
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
	p.controllers = append(p.controllers, conn)

	loc := &protos.Vec3{X: p.Position.X, Y: p.Position.Y, Z: p.Position.Z}
	reply := &protos.ToClient{
		Payload: &protos.ToClient_YouArePlayer{
			YouArePlayer: &protos.YouArePlayer{
				PlayerID:      i,
				SpawnLocation: loc,
			},
		},
	}
	if _, err := protodelim.MarshalTo(conn, reply); err != nil {
		log.Fatal(err)
	}
}

func (g *Game) handleIWantMovePlayer(msg *protos.IWantMovePlayer, conn net.Conn) {
	log.Info("processing I want move player")
	p, ok := g.Players[msg.PlayerID]
	if ok {
		// No checks whatsoever!
		msgPos := msg.NewPosition
		newPos := math32.Vector3{X: msgPos.X, Y: msgPos.Y, Z: msgPos.Z}
		p.Position = newPos
	}
}

func (g *Game) handleIWantChangeBlock(msg *protos.IWantChangeBlock, conn net.Conn) {
	log.Info("processing I want change block")
	msgPos := msg.BlockPosition
	msgTyp := msg.BlockType
	pos := model.IntPos3{X: int(msgPos.X), Y: int(msgPos.Y), Z: int(msgPos.Z)}
	typ := uint8(msgTyp)
	if err := g.World.SetBlockType(pos, typ); err != nil {
		log.Warn(err)
	}
}

func (g *Game) handleIWantColumn(msg *protos.IWantColumn, conn net.Conn) {
	log.Info("processing I want column")
	pos := &protos.Pos2{X: msg.ColumnPos.X, Z: msg.ColumnPos.Z}
	chunks := make([]*protos.ChunkData, 1)
	buf := make([]byte, 16*16*16)
	for i := 0; i < 16*16; i++ {
		if rand.Intn(2) == 0 {
			buf[i] = 1
		} else {
			buf[i] = 2
		}
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
	if _, err := protodelim.MarshalTo(conn, reply); err != nil {
		log.Fatal(err)
	}
}

func (g *Game) handleMessage(msg *IncomingMessage) {
	log.Info("received msg")
	switch x := msg.Data.Payload.(type) {
	case *protos.ToServer_IWantPlayer:
		g.handleIWantPlayer(x.IWantPlayer, msg.Connection)
	case *protos.ToServer_IWantMovePlayer:
		g.handleIWantMovePlayer(x.IWantMovePlayer, msg.Connection)
	case *protos.ToServer_IWantChangeBlock:
		g.handleIWantChangeBlock(x.IWantChangeBlock, msg.Connection)
	case *protos.ToServer_IWantColumn:
		g.handleIWantColumn(x.IWantColumn, msg.Connection)
	default:
		log.Warn("unknown msg type", x)
	}
}

func (g *Game) HandleMessages() {
	// Handle incoming packets.
	// Move on if there are no packets to receive.
	nMsgs := len(g.msgBuf)
	for i := 0; i < nMsgs; i++ {
		g.handleMessage(<-g.msgBuf)
	}
}

func (g *Game) Start() error {
	g.running = true

	udpAddr, err := net.ResolveTCPAddr("tcp", ":7979")
	if err != nil {
		return err
	}
	g.s, err = net.ListenTCP("tcp", udpAddr)
	if err != nil {
		return err
	}

	g.msgBuf = make(chan *IncomingMessage, 1024)

	go func() {
		// Read from UDP listener in endless loop
		defer g.s.Close()
		for g.running {
			log.Infoln("running")
			conn, err := g.s.Accept()
			log.Infof("accepted connection from %v", conn.RemoteAddr())
			if err != nil {
				return
			}
			go func(conn net.Conn) {
				defer conn.Close()
				for {
					rdr := bufio.NewReader(conn)
					var msg protos.ToServer
					if err := protodelim.UnmarshalFrom(rdr, &msg); err != nil {
						log.Warn(err)
						break
					}

					log.Infof("got msg %v from %v", msg.String(), conn.RemoteAddr())

					iMsg := IncomingMessage{
						Connection: conn,
						Data:       &msg,
					}
					g.msgBuf <- &iMsg
				}
			}(conn)
		}
	}()

	return nil
}
