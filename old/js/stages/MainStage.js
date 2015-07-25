/**
 * Created by Dario on 14/06/2015.
 */
/**
 * Created by Dario on 12/06/2015.
 */

var MainStage = function MainStage(game) {

    var assetsArray = [
        './assets/custom/tiles/dirt.png',
        './assets/custom/tiles/grass.png',
        './assets/custom/tiles/ocean.png',
        './assets/custom/tiles/snow.png'
    ];
    Stage.call(this, "game", {
        assets: assetsArray,
        settings: game.currentConfig,
        people: []
    }, game);
    this.LOG_TAG = "GameStage:";

    this.world = {
        tiles: [],
        selectedTile: undefined,
        tileWidth: 128,
        tileHeight: 64,
        w: 400,
        h: 300
    };

    this.mouse = undefined;
    this.needCamera = true;

    this.world.miniMap = undefined;
};
utils.inherit(MainStage, Stage);
MainStage.prototype.registerObjects = function registerObjects() {

    Stage.prototype.registerObjects.call(this);
    //prepare the world
    this.world.tiles = this.generateWorld(this.world.w, this.world.h, this.world.tileWidth, this.world.tileHeight);
    this.config.people.push({
        name: "firstMan",
        sex: "m"
    });
    this.config.people.push({
        name: "firstWoman",
        sex: "f"
    });
};
MainStage.prototype.update = function update(ctx, time) {
    this.Camera.update(ctx, this.world);
    return 16;
};
MainStage.prototype.render = function render(ctx, forceRedraw) {
    this.Camera.render(ctx, forceRedraw, this.world);
    if (this.world.selectedTile) {
        this.world.selectedTile.renderSelected(ctx, this.Camera.zoom);
    }
    return ctx;
};

MainStage.prototype.generateWorld = function generateWorld(w, h, tw, th) {
    var row, col, tile;
    var world = [];
    var miniC = document.createElement('canvas');
    var miniCtx = miniC.getContext('2d');

    var heightNoise = new NoiseGenerator(Math.random()),
        moistureNoise = new NoiseGenerator(Math.random()),
        rainfallNoise = new NoiseGenerator(Math.random());

    miniC.width = w - 1;
    miniC.height = h - 1;
    miniCtx.globalAlpha = 0.8;
    for (row = 0; row < h; row++) {
        world[row] = world[row] || [];
        for (col = 0; col < w; col++) {
            tile = new Tile(row, col, 0, 0, tw, th);
            this.setBiome(tile, heightNoise, moistureNoise, rainfallNoise);
            world[row][col] = tile;
            //prepare minimap
            miniCtx.fillStyle = DISPLAY_COLORS[tile.type];
            miniCtx.fillRect(col, row, 1, 1);
        }
    }

    this.world.miniMap = new Image();
    this.world.miniMap.src = miniC.toDataURL('image/png', 1);
    return world;
};
MainStage.prototype.setBiome = function setBiome(tile, hNoise, mNoise, rNoise) {
    var maxHeight = 100;
    this.setElevation(tile, hNoise, maxHeight);
    this.setTemperature(tile, this.world.h / 2);
    this.setRainfall(tile, rNoise);
    this.setMoisture(tile, mNoise);

    if (!tile.type) {

        //identify the type of the tile
        if (tile.elevation < -0.10) {
            tile.type = "OCEAN";
            tile.img = this.AM.bundle['./assets/custom/tiles/ocean.png'];
            tile.moisture = 1;
        } else if (tile.elevation < 0.2) {
            tile.type = "COAST";
            tile.moisture = 0.1 * utils.randomIntFromInterval(7, 10);
        } else if (tile.elevation < 0.25) {
            tile.type = "BEACH";
        } else if (tile.elevation < 0.4) {
            if (tile.moisture > 0.66) tile.type = 'TAIGA';
            else if (tile.moisture > 0.33) tile.type = 'SHRUBLAND';
            else tile.type = 'TEMPERATE_DESERT';
        } else if (tile.elevation < 0.5) {
            if (tile.moisture > 0.83) tile.type = 'TEMPERATE_RAIN_FOREST';
            else if (tile.moisture > 0.50) tile.type = 'TEMPERATE_DECIDUOUS_FOREST';
            else if (tile.moisture > 0.16) tile.type = 'GRASSLAND';
            else tile.type = 'TEMPERATE_DESERT';
        } else if (tile.elevation < 0.75) {
            if (tile.moisture > 0.66) tile.type = 'TROPICAL_RAIN_FOREST';
            else if (tile.moisture > 0.33) tile.type = 'TROPICAL_SEASONAL_FOREST';
            else if (tile.moisture > 0.16) {
                tile.type = "GRASSLAND";
                tile.img = this.AM.bundle['./assets/custom/tiles/grass.png'];
            }
            else tile.type = 'SUBTROPICAL_DESERT';
        } else if (tile.elevation < 0.80) {
            if (tile.moisture > 0.50) tile.type = 'SNOW';
            else if (tile.moisture > 0.33) tile.type = 'TUNDRA';
            else if (tile.moisture > 0.16) tile.type = 'BARE';
            else this.type = 'SCORCHED';
        } else if (tile.elevation < 0.89) {
            tile.type = "ROCK";
        } else {
            tile.type = "SNOW";
            tile.img = this.AM.bundle['./assets/custom/tiles/snow.png'];
        }
    }

}
;
MainStage.prototype.setElevation = function setElevation(tile, noise, maxHeight) {
    var val = noise.simplex2(tile.c / maxHeight, tile.r / maxHeight);
    tile.elevation = +val.toFixed(2);
};
MainStage.prototype.setMoisture = function setMoisture(tile, noise) {
    var val = noise.simplex2(tile.c / 100, tile.r / 100);
    tile.moisture = Math.abs(val * tile.rainfall / tile.temperature).toFixed(2);
};
MainStage.prototype.setTemperature = function setTemperature(tile, equator) {
    var distanceFromEq = Math.abs(equator - tile.r);
    tile.temperature = (60 - 0.7 * distanceFromEq - 10 * Math.abs(tile.elevation)).toFixed(2);
};
MainStage.prototype.setRainfall = function setRainfall(tile, noise) {
    var val = noise.simplex2(tile.c / 100, tile.r / 100);
    tile.rainfall = Math.ceil(Math.abs((100 * val ) / tile.temperature) * 100);
};

//<editor-fold desc="# IO Handlers">

MainStage.prototype.mouseMoveHandler = function (status) {
    var self = this;
    var r, c;
    this.mouse = status.mouse;
    this.world.selectedTile = this.Camera.selectTile(this.world, this.mouse);
    //find the tile hovered
    document.body.style.cursor = "default";
};
MainStage.prototype.keyDownHandler = function (status) {
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
MainStage.prototype.wheelHandler = function wheel(status) {
    var self = this;
    //this.Camera.changeZoom(status.mouse.mouseWheelDir);
};

//</editor-fold>