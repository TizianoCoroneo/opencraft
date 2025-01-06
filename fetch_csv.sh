#! /bin/sh
set -eu

mkdir -p stats

machine=$(uname -s | grep -qi darwin && echo "mac" || echo "linux")
if [ "$machine" = "linux" ]; then
    cp $HOME/.config/unity3d/DefaultCompany/my-first-unity-project/stats/* stats/
else
    echo "TODO: find path on mac"
fi
