#! /bin/bash
set -euo pipefail

telegraf --config telegraf_server.conf
