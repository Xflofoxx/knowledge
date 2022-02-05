'use strict';

const express = require('express');

class Server {

    constructor(config, logger) {
        this._logger = logger;
        this._config = config;
        this._app = express();
    }

    get config() {
        return this._config;
    }
    get logger() {
        return this._logger;
    }

    /**
     * Apply middlewares
     */
    _applyMiddleware() {
        const MIDDLEWARES = require("./middlewares")(this);

        this._app.use(MIDDLEWARES.helmet);
        this._app.use(MIDDLEWARES.favicon);
        this._app.use(MIDDLEWARES.morgan);
        this._app.use(MIDDLEWARES.compression);
        this._app.use(MIDDLEWARES.static);
    }

    async init() {
        this._applyMiddleware();

        this._app.get('/', (req, res) => {
            res.status(200).sendFile("../client/index.html");
        });

        this._app.use((req, res, next) => {
            res.status(404).send("Sorry can't find that!")
        });

        this._app.use(function(err, req, res, next) {
            console.error(err.stack)
            res.status(500).send('Something broke!')
        });
    }

    async start() {
        return new Promise(resolve => {
            this._app.listen(this._config.port, () => {
                console.log(`Knowledge server listening on port ${this._config.port}`);
                resolve();
            });
        });
    }
}

module.exports = Server;