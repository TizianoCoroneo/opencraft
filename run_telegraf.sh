#! /bin/sh
set -euo pipefail

DB_ADDRESS=192.168.1.1 # influxDB instance address
DB_PORT=8086 # influxDB HTTP port

$DB_ADDRESS $DB_PORT telegraf --config telegraf.conf
