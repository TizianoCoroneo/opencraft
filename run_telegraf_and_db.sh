#! /bin/zsh

tmux split-window -v -p 30 'influxd -config ./influxdb.conf'
tmux split-window -h -p 50 'telegraf --config telegraf.conf'


