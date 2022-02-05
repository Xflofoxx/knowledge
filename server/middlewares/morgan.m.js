'use strict';

const morgan = require("morgan");

module.exports = server => {
    return morgan(server.config.morganLogFormat);
}