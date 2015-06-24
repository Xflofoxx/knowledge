'use strict';
/**
 * Created by Dario on 05/06/2015.
 */
var JagEngine = function JagEngine(canvas){
    var bgCanvas = document.createElement('canvas');

    this.LOG_TAG = "JagEngine:";
    this.rAF = 0; // reference for RequestAnimationFrame
    this.ttu = 0; // TimeToUpdate - time (in ms) till next update
    this.time = 0; // Last update time

    this.initialized = false; // Flag to store init state
    this.running = false; // Flag to store runloop state

    this.ctx = canvas.getContext('2d');
    this.bctx = bgCanvas.getContext('2d');
    this.currentStage = null;
    this.IO = null;
    this.forceRedraw=false;
    this.zoom = 1;
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
        //add listeners
        //this.IO.on('wheel',this.onMouseWheelHandler.bind(this));

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
        if(self.currentStage.needCamera){
            self.currentStage.Camera = new Camera(0, 0, self.bctx.canvas.width, self.bctx.canvas.height);
        }
        self.clear();
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
        return this.currentStage.update(this.bctx, time);
    }else{
        return 17;
    }
};
JagEngine.prototype.render = function render(){
    if(this.currentStage){
        this.bctx = this.currentStage.render(this.bctx, this.forceRedraw);
        this.clear("ctx");
        this.ctx.drawImage(this.bctx.canvas, 0, 0);
        this.forceRedraw = false;
    }
    //attach other engine rendering
    this.IO.drawInputInfos(this.ctx);
    //this.testUI();
};
JagEngine.prototype.clear = function clear(what){
    switch(what){
        case "ctx":
            this.ctx.fillStyle = "#000000";
            this.ctx.fillRect(0,0,this.ctx.canvas.width, this.ctx.canvas.height);
            break;
        case "bctx":
            this.bctx.fillStyle = "#000000";
            this.bctx.fillRect(0, 0, this.bctx.canvas.width, this.bctx.canvas.height);
            break;
        default:
            this.bctx.fillStyle = "#000000";
            this.bctx.fillRect(0, 0, this.bctx.canvas.width, this.bctx.canvas.height);
            this.ctx.fillStyle = "#000000";
            this.ctx.fillRect(0,0,this.ctx.canvas.width, this.ctx.canvas.height);
    }
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