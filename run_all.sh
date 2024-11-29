#! /bin/zsh

UNITY_PATH=/Applications/Unity/Hub/Editor/2022.3.26f1/Unity.app/Contents/MacOS/Unity

telegraf --config client/unity/my-first-unity-project/telegraf.conf & # Telegraf agent
influxd -config client/unity/my-first-unity-project/influxdb.conf & # Metrics db
signaling-server/webserver_mac & # Signaling server
server/go/opencraft-go/opencraft-go & # Game server

$UNITY_PATH -projectPath client/unity/my-first-unity-project/ & # Unity client
$UNITY_PATH -projectPath client/unity/my-first-unity-project_clone0/ & # Unity client

jobs


