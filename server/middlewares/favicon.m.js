'use strict';
const favicon = require('serve-favicon');
const path = require('path');

module.exports = () => {
    return favicon(path.join(__dirname, '../../client/assets', 'favicon.ico'));
}