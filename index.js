'use strict';

const server = require("./server");
const port = "8080";

server.listen(port, () => {
    console.log(`Example app listening on port ${port}`)
});