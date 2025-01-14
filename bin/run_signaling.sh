#! /bin/sh
set -eu

machine=$(uname -s | grep -qi darwin && echo "mac" || echo "linux")
signaling-server/webserver_${machine} -p 7981
