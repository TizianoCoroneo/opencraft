#! /bin/zsh

UNITY_PATH=/Applications/Unity/Hub/Editor/2022.3.26f1/Unity.app/Contents/MacOS/Unity

telegraf --config client/unity/my-first-unity-project/telegraf.conf & # Telegraf agent
$UNITY_PATH -projectPath client/unity/my-first-unity-project/ & # Unity client

