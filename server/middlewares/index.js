'use strict';

module.exports = server => ({
    static: require("./static.m")(),
    compression: require("./compression.m")(),
    helmet: require("./helmet.m")(),
    morgan: require("./morgan.m")(server),
    favicon: require("./favicon.m")(),
});