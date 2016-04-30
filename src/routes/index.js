'use strict';
/**
 * @author Dario Olivini
 * @copyright 2016 Dario Olivini. All rights reserved.
 * See LICENSE file in root directory for full license.
 */

module.exports = function (server, app){

    app.get('/credits', function (req, res) {
        res.render('credits',{
            bodyClasses:"credits",
            title: "Knowledge - a game on mankind",
            handler: false
        });
    });

    app.get('/load', function (req, res) {
        res.render('load',{
            bodyClasses:"load",
            title: "Knowledge - a game on mankind",
            handler: "load.js"
        });
    });

    app.get('/settings', function (req, res) {
        res.render('settings',{
            bodyClasses:"settings",
            title: "Knowledge - a game on mankind",
            handler: "settings.js",
            settings: {}
        });
    });

    app.post('/settings',function (req, res) {
       var data = req.body;
        console.log(data);
        res.render('settings',{
            bodyClasses:"settings",
            title: "Knowledge - a game on mankind",
            handler: "settings.js",
            message: "Correctly saved!",
            error: false
        });
    });

    app.get('/new', function (req, res) {
        res.render('new_game',{
            bodyClasses:"new",
            title: "Knowledge - a game on mankind",
            handler: "new.js"
        });
    });

    app.get('/', function (req, res) {
        res.render('home',{
            bodyClasses:"home",
            title: "Knowledge - a game on mankind",
            handler: "home.js"
        });
    });
    
};