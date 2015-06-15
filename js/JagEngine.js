'use strict';
/**
 * Created by Dario on 05/06/2015.
 */
var JagEngine = function JagEngine(canvas){
    this.LOG_TAG = "JagEngine:";

    this.rAF = 0; // reference for RequestAnimationFrame
    this.ttu = 0; // TimeToUpdate - time (in ms) till next update
    this.time = 0; // Last update time

    this.initialized = false; // Flag to store init state
    this.running = false; // Flag to store runloop state

    this.ctx = canvas.getContext('2d');
    this._bgcanvas = document.createElement('canvas');
    this.bctx = this._bgcanvas.getContext('2d');
    this.currentStage = null;
    this.IO = null;
    this.forceRedraw=false;
};
JagEngine.prototype.init = function init(inputManager){
    this.IO = inputManager;
    //resize canvas create background canvas and get context
    this.ctx.canvas.width = window.innerWidth;
    this.ctx.canvas.height = window.innerHeight;
    this.bctx.canvas.width =  this.ctx.canvas.width;
    this.bctx.canvas.height = this.ctx.canvas.height;

    //attach event for resize
    window.addEventListener('resize', this.onResizeHandler.bind(this), false);

    this.initialized= true;
    this.clear();
    // console.log(this.LOG_TAG + " init!");
};
JagEngine.prototype.start = function start(){
    if(this.initialized){
        this.rAF = requestAnimationFrame(this.frame.bind(this));
    }
    this.running = true;
    // console.log(this.LOG_TAG + " start!");
};
JagEngine.prototype.stop = function stop(){
    if (this.running && this.rAF) {
        cancelAnimationFrame(this.rAF);
    }
    this.rAF = 0;
    this.running = false;
    // console.log(this.LOG_TAG + " stop!");
};
JagEngine.prototype.loadStage = function loadStage(stage){
    var self = this;
    if(this.currentStage){
        this.currentStage.active = false;
        this.IO.removeAll();
    }
    this.stop();
    this.currentStage = stage;
    this.currentStage.init(function(err){
        self.currentStage.active = true;
        self.start();
    });
};

JagEngine.prototype.frame= function frame(time){
    var dt = time - this.time;
    // store last update time
    this.time = time;
    // decrease update timer
    this.ttu -= dt;
    if (this.ttu < 0) {
        // update and render the stage
        this.ttu = this.update(dt);
        this.render();
    }
    // request next frame update
    this.rAF = requestAnimationFrame(this.frame.bind(this));
};
JagEngine.prototype.update = function update(time){
    // console.log(this.LOG_TAG + " update!");
    if(this.currentStage){
        return this.currentStage.update(time);
    }else{
        return 17;
    }
};
JagEngine.prototype.render = function render(){
    if(this.currentStage){
        this.bctx = this.currentStage.render(this.bctx, this.forceRedraw);
        this.ctx.drawImage(this._bgcanvas, 0, 0);
        this.forceRedraw = false;
    }
    //attach other engine rendering
    this.IO.drawInputInfos(this.ctx);
    //this.testUI();
};
JagEngine.prototype.clear = function clear(){
    // console.log(this.LOG_TAG + " clear!");
    this.ctx.fillStyle = "#000000";
    this.ctx.fillRect(0,0,this.ctx.canvas.width, this.ctx.canvas.height);
};

JagEngine.prototype.testUI = function testUI(){
    var w = new ui.window.empty("Mouse infos", (this.ctx.canvas.width-100)/2,
        (this.ctx.canvas.height-60)/2, 400,200);
    w.render(this.ctx);
};

JagEngine.prototype.onResizeHandler = function onResizeHandler(event){
    // console.log(this.LOG_TAG + " onResizeHandler!");
    this.ctx.canvas.width = window.innerWidth;
    this.ctx.canvas.height = window.innerHeight;
    this.bctx.canvas.width =  this.ctx.canvas.width;
    this.bctx.canvas.height = this.ctx.canvas.height;
    this.clear();
    this.forceRedraw = true;
};

