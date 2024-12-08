#! /bin/sh
set -eu

machine=$(uname -s | grep -qi darwin && echo "mac" || echo "linux")
server/go/opencraft-go/opencraft-go-${machine} # Game server port: 7979
