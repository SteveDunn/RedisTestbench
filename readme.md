
`docker exec -it myredis bash`

then run `docker-cli`

.. and some test commands, e.g.

```bash
lpush newusers goku
lrange newusers 0 0
```
