docker network create -d bridge redisnet
docker run -d -p 6379:6379 --name myredis --network redisnet redis
