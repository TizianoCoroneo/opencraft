#! /bin/sh
set -eu
signaling-server/webserver_mac & # Signaling server 7981
server/go/opencraft-go/opencraft-go # Game server port: 7979
