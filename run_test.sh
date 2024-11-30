#! /bin/sh

# macos path
# UNITY_PATH=/Applications/Unity/Hub/Editor/2022.3.26f1/Unity.app/Contents/MacOS/Unity
# linux path
UNITY_PATH=~/Unity/Hub/Editor/2022.3.26f1/Editor/Unity
$UNITY_PATH -projectPath client/unity/my-first-unity-project/ -runTests -testResults ./test_results.xml -testPlatform PlayMode -testFilter GoForward60Seconds 
