#! /bin/sh
set -eu

cleanup() {
  echo "Caught Ctrl+C! Terminating all background processes..."
  kill 0
  wait
  exit 1
}

cd bin
( ./build_server.sh )

( ./run_server.sh ) &
( ./run_signaling.sh ) &
./run_unity_thin.sh

cleanup
