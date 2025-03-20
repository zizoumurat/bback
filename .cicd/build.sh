#!/bin/bash

apt-get update

dotnet restore "src/Buyersoft.WebAPI/Buyersoft.WebAPI.csproj" --runtime win-x64
dotnet build "src/Buyersoft.WebAPI/Buyersoft.WebAPI.csproj" --no-restore
dotnet publish "src/Buyersoft.WebAPI/Buyersoft.WebAPI.csproj" --no-restore -c Release -r win-x64 -o $BITBUCKET_CLONE_DIR/release
