'use strict';
/**
 * @author Dario Olivini
 * @copyright 2016 Dario Olivini. All rights reserved.
 * See LICENSE file in root directory for full license.
 */
let express = require("express");
let exphbs  = require("express-handlebars");
let path = require("path");

class Server {
    constructor() {
        this.app = express();
    };

    init() {
        return new Promise((resolve, reject)=> {
            this.app.engine('.hbs', exphbs({
                extname: '.hbs',
                defaultLayout: 'main',
                layoutsDir : __dirname + "/views/layouts"
            }));
            this.app.set('views', __dirname + '/views');
            this.app.set('view engine', '.hbs');

            this.app.use('/public', express.static(__dirname + '/client/public'));

            this.app.get('/', function (req, res) {
                res.render('home',{
                    bodyClasses:"home",
                    title: "Knowledge - a game on mankind",
                    handler: "home.js"
                });
            });
            
            resolve();
        });
    };

    start() {
        this.app.listen(3000, ()=> {
            console.log("Knowledge server started at http://localhost:3000");
        });
    };
}

module.exports = Server;

