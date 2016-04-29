'use strict';
let Server = require("./src");
let knowledge = new Server();

knowledge.init()
    .then(()=>{
        knowledge.start();
    });