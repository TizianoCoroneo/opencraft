#! /bin/sh

UNITY_PATH=$(uname -s | grep -qi darwin && \
    echo "/Applications/Unity/Hub/Editor/2022.3.26f1/Unity.app/Contents/MacOS/Unity" || \
    echo "$HOME/Unity/Hub/Editor/2022.3.26f1/Editor/Unity")
__NV_PRIME_RENDER_OFFLOAD=1 __GLX_VENDOR_LIBRARY_NAME=nvidia $UNITY_PATH -projectPath client/unity/my-first-unity-project/ -runTests -testPlatform PlayMode -testFilter GoForward60Seconds 
