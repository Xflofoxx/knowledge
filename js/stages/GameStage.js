/**
 * Created by Dario on 14/06/2015.
 */
/**
 * Created by Dario on 12/06/2015.
 */

var GameStage = function GameStage(AM, IO, settings) {

    var assetsArray = [];
    for (var a = 0; a < 24; a++) {
        assetsArray.push({
            id: "gw_" + a,
            path: window.location + '/../assets/tiles/',
            file: "grass_and_water_" + a + ".png",
            mime: "image/png"
        });
    }

    Stage.call(this, "game", {
        assets: assetsArray,
        settings: settings,
        people: []
    }, AM, IO);
    this.LOG_TAG = "GameStage:";

    this.world = {
        tiles: [],
        selectedTile : undefined,
        tileWidth: 64,
        tileHeight: 32,
        w: 1000,
        h: 800
    };

    this.mouse = undefined;
    this.needCamera = true;
};
utils.inherit(GameStage, Stage);
GameStage.prototype.registerObjects = function registerObjects() {

    Stage.prototype.registerObjects.call(this);
    //prepare the world
    this.world.tiles = this.generateWorld(this.world.w, this.world.h, this.world.tileWidth, this.world.tileHeight);
    this.config.people.push({
        name: "firstMan",
        sex: "m",
        r: 0,
        c: 0
    });
};
GameStage.prototype.update = function update(ctx, time) {
    var x, y, r, c;
    //console.log(this.LOG_TAG + " update");
    if (this.mouse) {
        if (this.mouse.mousePos.y < this.world.tileWidth) {
            this.Camera.moveUp(this.world.h);
        } else if (this.mouse.mousePos.y > this.world.tileHeight / 2 * (this.Camera.viewport.rows - 1)) {
            this.Camera.moveDown(this.world.h);
        }
        if (this.mouse.mousePos.x < this.world.tileWidth * 2) {
            this.Camera.moveLeft(this.world.w);
        } else if (this.mouse.mousePos.x > this.world.tileWidth * (this.Camera.viewport.cols - 2)) {
            this.Camera.moveRight(this.world.w);
        }
    }
    this.Camera.update(ctx, this.world);
    return 16;
};
GameStage.prototype.render = function render(ctx, forceRedraw) {
    this.Camera.render(ctx, this.world);
    if(this.world.selectedTile){
        this.world.selectedTile.renderSelected(ctx);
    }
    return ctx;
};

GameStage.prototype.generateWorld = function generateWorld(w, h, tw, th) {
    var row, col, tile;
    var world = [];

    var heightNoise = new NoiseGenerator(Math.random()),
        moistureNoise = new NoiseGenerator(Math.random()),
        temperatureNoise = new NoiseGenerator(Math.random()),
        rainfallNoise = new NoiseGenerator(Math.random());

    for (row = 0; row < h; row++) {
        world[row] = world[row] || [];
        for (col = 0; col < w; col++) {
            tile = new Tile(row, col, 0, 0, tw, th,
                this.AM.bundle.game[this.config.assets[Math.floor(Math.random() * this.config.assets.length)].id]);
            tile.getBiome(heightNoise, moistureNoise, temperatureNoise, rainfallNoise);
            world[row][col] = tile;
        }
    }
    //this.setTilesHeight(world, 100);
    return world;
};

//<editor-fold desc="# IO Handlers">

GameStage.prototype.mouseMoveHandler = function (status) {
    var self = this;
    var r,c;
    this.mouse = status.mouse;
    this.world.selectedTile = this.Camera.selectTile(this.world, this.mouse);
    //find the tile hovered
    document.body.style.cursor = "default";
};
GameStage.prototype.keyDownHandler = function (status) {
    if (status.keyboard[this.IO.keyEventEnum.DOM_VK_RIGHT] || status.keyboard[this.IO.keyEventEnum.DOM_VK_D]) {
        this.Camera.moveRight(this.world.w);
    }
    if (status.keyboard[this.IO.keyEventEnum.DOM_VK_LEFT] || status.keyboard[this.IO.keyEventEnum.DOM_VK_A]) {
        this.Camera.moveLeft(this.world.w);
    }
    if (status.keyboard[this.IO.keyEventEnum.DOM_VK_DOWN] || status.keyboard[this.IO.keyEventEnum.DOM_VK_S]) {
        this.Camera.moveDown(this.world.h);
    }
    if (status.keyboard[this.IO.keyEventEnum.DOM_VK_UP] || status.keyboard[this.IO.keyEventEnum.DOM_VK_W]) {
        this.Camera.moveUp(this.world.h);
    }
};
//GameStage.prototype.mouseDownHandler = function (status) {
//    var self = this;
//    var gridOffsetY = this.world.h;
//    var gridOffsetX = this.world.w;
//
//    gridOffsetX += ()
//};

//</editor-fold>