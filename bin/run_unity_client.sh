#! /bin/sh

UNITY_PATH=$(uname -s | grep -qi darwin && \
    echo "/Applications/Unity/Hub/Editor/2022.3.26f1/Unity.app/Contents/MacOS/Unity" || \
    echo "$HOME/Unity/Hub/Editor/2022.3.26f1/Editor/Unity")
$UNITY_PATH -projectPath client/unity/my-first-unity-project/ -runTests -testPlatform PlayMode -testFilter GoForward60Seconds 
