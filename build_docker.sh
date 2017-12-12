#! /bin/bash
GITSHA1=${GITSHA1:=$(git rev-parse HEAD)}
DOCKER_IMAGE_VERSION=${GITSHA1:1:5}
echo $DOCKER_IMAGE_VERSION
docker build -t goldytech/lf-tenant2:$DOCKER_IMAGE_VERSION .

