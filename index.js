'use strict';
require("dotenv").config();

const {
    SERVER_PORT,
    MORGAN_LOG_FORMAT
} = process.env;

const Server = require("./server/Server");

const Knowledge = new Server({
    port: SERVER_PORT,
    morganLogFormat: MORGAN_LOG_FORMAT
});

Knowledge.init()
    .then(Knowledge.start())
    .catch(err => console.error(err.message));