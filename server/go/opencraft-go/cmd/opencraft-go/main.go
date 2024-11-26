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

// GameData represents the types of objects that be present in the game such as
// blocks and items. These can be specified in yaml format to make them easy to
// change.
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
			case <-osSignalChan:
				return
			case <-ticker.C:
				game.Update()
			}
		}
	}()
}
