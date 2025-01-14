#! /bin/sh
set -eu

cd ../server/go/opencraft-go/cmd/opencraft-go/
go build
machine=$(uname -s | grep -qi darwin && echo "mac" || echo "linux")
mv opencraft-go "../../opencraft-go-${machine}"

