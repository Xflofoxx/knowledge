'use strict';
/**
 * @author Dario Olivini
 * @copyright 2016 Dario Olivini. All rights reserved.
 * See LICENSE file in root directory for full license.
 */
let express = require("express");
let exphbs  = require("express-handlebars");
let path = require("path");
let router=require("./routes");
let oneDay = 86400000;

const EXPRESS_MIDDLEWARE = {
    BODY_PARSER: require('body-parser').urlencoded({limit: '50mb', extended: true}),
    JSON_PARSER: require('body-parser').json({limit: '50mb'}),
    //COOKIE : require('cookie'),
    COOKIE_PARSER: require('cookie-parser')('supercalifragilistichespiralidoso'),
    COMPRESSION: require('compression')({
        threshold: 512,
        level: -1 //zlib.Z_BEST_SPEED
    }),
    METHOD_OVERRIDE: require('method-override')(),
    SESSION: require('express-session')({
        secret: 'supercalifragilistichespiralidoso',
        key: 'knowledge',
        resave: false,
        saveUninitialized: false,
        cookie: {httpOnly: true, secure: true, signed: true}
    }),
    STATIC: express.static(__dirname + '/client/public', {
        maxAge: 30 * oneDay
    }),
    TEMPLATE_ENGINE: exphbs({
        extname: '.hbs',
        defaultLayout: 'main',
        layoutsDir : __dirname + "/views/layouts"
    })
};

class Server {
    constructor() {
        this.app = express();
    };

    init() {
        return new Promise((resolve, reject)=> {
            this.app.engine('.hbs', EXPRESS_MIDDLEWARE.TEMPLATE_ENGINE);
            this.app.set('views', __dirname + '/views');
            this.app.set('view engine', '.hbs');
            this.app.use(EXPRESS_MIDDLEWARE.COMPRESSION);
            this.app.use(EXPRESS_MIDDLEWARE.JSON_PARSER);
            this.app.use(EXPRESS_MIDDLEWARE.BODY_PARSER);
            this.app.use(EXPRESS_MIDDLEWARE.METHOD_OVERRIDE);
            this.app.use('/public',EXPRESS_MIDDLEWARE.STATIC);
            //this.app.use(EXPRESS_MIDDLEWARE.FAVICON);
            this.app.use(EXPRESS_MIDDLEWARE.COOKIE_PARSER);
            this.app.use(EXPRESS_MIDDLEWARE.SESSION);

            router(this, this.app);
            
            resolve();
        });
    };

    start() {
        this.app.listen(process.env.PORT, ()=> {
            console.log(`Knowledge server started at http://localhost:${process.env.PORT}`);
        });
    };
}

module.exports = Server;

