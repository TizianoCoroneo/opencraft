#! /bin/sh
set -eu

./bin/build_server.sh

./bin/run_server.sh &
./bin/run_signaling.sh &
./bin/run_unity_thin.sh &

wait
