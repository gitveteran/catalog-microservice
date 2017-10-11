#!/bin/bash
set -ev

HEROKU_USERNAME=$1
HEROKU_API_KEY=$2

# Create publish artifact
dotnet publish -c Release

# Build the Docker images
docker build -t catalog-microservice ./bin/Release/netcoreapp2.0/publish
docker tag catalog-microservice registry.heroku.com/catalog-microservice/web

# Login to Docker Hub and upload images
docker login --username="$HEROKU_USERNAME" --password="$HEROKU_API_KEY" registry.heroku.com
docker push registry.heroku.com/catalog-microservice/web