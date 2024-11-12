package main

import (
	_ "embed"
	"os"
	"os/signal"
	"syscall"
	"time"

	log "github.com/sirupsen/logrus"

	"github.com/jdonkervliet/opencraft-go/server"
)

//go:embed data/blocks.yml
var blocks string

type GameData struct {
	Blocks []struct {
		Name  string `yaml:"name"`
		Color string `yaml:"color"`
	} `yaml:"blocks"`
	Items []struct {
		Name string `yaml:"name"`
	} `yaml:"items"`
}

func main() {
	// timeoutChan := time.NewTimer(10 * time.Second)
	osSignalChan := make(chan os.Signal, 1)
	signal.Notify(osSignalChan, os.Interrupt, syscall.SIGINT, syscall.SIGTERM)
	ticker := time.NewTicker(50 * time.Millisecond)

	game := server.NewGame()
	err := game.Start()
	if err != nil {
		log.Fatal(err)
	}

	func() {
		for {
			select {
			// case <-timeoutChan.C:
			// 	return
			case <-osSignalChan:
				return
			case <-ticker.C:
				game.Update()
			}
		}
	}()

	// fmt.Println()

	// fmt.Println(blocks)

	// b := []byte(blocks)
	// var gameData GameData

	// start := time.Now()
	// fmt.Print("Parsing game data... ")
	// if err := yaml.Unmarshal(b, &gameData); err != nil {
	// 	panic(err)
	// }
	// done := time.Since(start)
	// fmt.Printf("done! (%v)\n", done)

	// for _, block := range gameData.Blocks {
	// 	fmt.Println(block.Name, block.Color)
	// }

	// for _, item := range gameData.Items {
	// 	fmt.Println(item.Name)
	// }
}
