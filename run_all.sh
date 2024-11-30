#! /bin/zsh

set -euo pipefail

UNITY_PATH=/Applications/Unity/Hub/Editor/2022.3.26f1/Unity.app/Contents/MacOS/Unity

tmux set-option remain-on-exit on
tmux split-window -h -p 30 'telegraf --config telegraf.conf' # Telegraf agent
tmux split-window -h -p 30 'influxd --config influxdb.conf' # Metrics db
tmux split-window -h -p 30 'signaling-server/webserver_mac' # Signaling server for macOS
tmux split-window -h -p 30 'server/go/opencraft-go/opencraft-go' # Game server

$UNITY_PATH -projectPath client/unity/my-first-unity-project/ & # Unity client

