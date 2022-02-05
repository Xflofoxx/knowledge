'use strict';

const express = require('express');
const path = require('path')
const app = express();

app.use(express.static(path.join(__dirname, '../client')));


app.get('/', (req, res) => {
    res.status(200).sendFile("../client/index.html");
});

app.use((req, res, next) => {
    res.status(404).send("Sorry can't find that!")
});

app.use(function (err, req, res, next) {
    console.error(err.stack)
    res.status(500).send('Something broke!')
});

module.exports = app;
