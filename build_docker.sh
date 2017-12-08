GITSHA1=${GITSHA1:=$(git rev-parse HEAD)}
DOCKER_IMAGE_VERSION=${BUILD_NUMBER}.${GITSHA1}

docker build -t goldytech/lf-tenant2:$DOCKER_IMAGE_VERSION .

