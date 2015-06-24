var Stage = function Stage(name, config, AM, IO) {
    this.LOG_TAG = "Stage:";
    EventEmitter.call(this);
    this.name = name;
    //this.assets = config.assets || [];
    this.config = config;
    //assetsManager
    this.AM = AM;
    this.resourceMap = false;
    //IOManager
    this.IO = IO;
    //Camera for zoom, pan and rotate
    this.needCamera = false;
    this.Camera = undefined;
    //objects
    this.objects = [];
    this.paused = false;
};
utils.inherit(Stage, EventEmitter);

Stage.prototype.init = function init(next) {
    var self = this;
    document.body.style.cursor = "default";
    if (!self.resourceMap) {
        console.log(this.LOG_TAG + " loadAssets");
        self.AM.load(self.name, self.config.assets, function (err, map) {
            self.resourceMap = map;
            self.registerObjects();
            next(err);
        });
    } else {
        console.log(this.LOG_TAG + " stage already initialized");
        next(null);
    }
};
Stage.prototype.pause = function pause(paused) {
    this.paused = paused;
};

Stage.prototype.registerObjects = function registerObjects() {
    var e, event;
    //bind all the events from InputManager
    for (e = 0; e < this.IO.managedEvents.length; e++) {
        event = this.IO.managedEvents[e];
        this.IO.on(event.toLowerCase(), this[event + "Handler"].bind(this));
    }
};
Stage.prototype.update = function update(ctx,time) {
    console.log(this.LOG_TAG + " update");
    if (this.active) {
        return 16;
    } else {
        return 1000;
    }
};
Stage.prototype.render = function render(ctx, forceRedraw) {
    console.log(this.LOG_TAG + " render", forceRedraw);
    //ctx.drawImage(this.AM.bundle[this.name]["tree.jpg"],0,0,ctx.canvas.width, ctx.canvas.height);
    return ctx;
};
Stage.prototype.manage = function manage(event, data, suppressEmit) {
    var processedData = data;
    if (!suppressEmit) {
        this.emit(event, processedData);
    }
    console.log(this.LOG_TAG + " handle");
};

//<editor-fold desc="# IO Handlers">
Stage.prototype.keyUpHandler = function (event) {
    console.log(this.LOG_TAG + " keyUpHandler");
};
Stage.prototype.keyDownHandler = function (event) {
    console.log(this.LOG_TAG + " keyDownHandler");
};
Stage.prototype.mouseUpHandler = function (event) {
    console.log(this.LOG_TAG + " mouseUpHandler");
};
Stage.prototype.mouseDownHandler = function (event) {
    console.log(this.LOG_TAG + " mouseDownHandler");
};
Stage.prototype.wheelHandler = function (event) {
    console.log(this.LOG_TAG + " mouseWheelHandler");
};
Stage.prototype.mouseMoveHandler = function (event) {
    console.log(this.LOG_TAG + " mouseMoveHandler");
};
Stage.prototype.contextMenuHandler = function (event) {
    console.log(this.LOG_TAG + " contextMenuHandler");
};
//</editor-fold>