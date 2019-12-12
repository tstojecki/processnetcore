#!/bin/sh
source=$1
target=$2
unzip -q "${source}/*.zip" -d "${target}"
