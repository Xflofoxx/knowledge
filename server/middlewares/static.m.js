'use strict';

const express = require("express");
const path = require('path');

module.exports = () => {
    return express.static(path.join(__dirname, '../../client'))
}