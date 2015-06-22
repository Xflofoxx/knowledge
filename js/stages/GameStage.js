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
        rendered: false,
        tiles: [],
        tileWidth: 64,
        tileHeight: 32,
        w: 2000,
        h: 2000,
        srow: 0,
        scol: 0,
        viewport: undefined
    };
    this.mouse = undefined;
};
utils.inherit(GameStage, Stage);
GameStage.prototype.registerObjects = function registerObjects() {

    Stage.prototype.registerObjects.call(this);
    //prepare the world
    this.world.tiles = this.generateWorld(this.world.w, this.world.h, this.world.tileWidth, this.world.tileHeight);
    this.world.heightMap = [];
    this.config.people.push({
        name: "firstMan",
        sex: "m",
        r: 0,
        c: 0
    });
};
GameStage.prototype.update = function update(time) {
    //console.log(this.LOG_TAG + " update");
    if (this.mouse && this.world.viewport) {
        if (this.world.srow >= 0 && this.world.srow <= this.world.h - this.world.viewport.rows + 2) {
            if (this.mouse.mousePos.y < this.world.tileWidth && this.mouse.mousePos.y > 0) {
                this.world.srow -= 1;
                if (this.world.srow < 0) {
                    this.world.srow = 0;
                }
            } else if (this.mouse.mousePos.y > this.world.tileHeight / 2 * (this.world.viewport.rows - 2)) {
                this.world.srow += 1;
                if (this.world.srow >= this.world.h - this.world.viewport.rows + 2) {
                    this.world.srow = this.world.h - this.world.viewport.rows + 1;
                }
            }
        }
        if (this.mouse.mousePos.x < this.world.tileWidth * 2) {
            this.world.scol -= 1;
            if (this.world.scol < 0) {
                this.world.scol += this.world.w;
            }
        } else if (this.mouse.mousePos.x > this.world.tileWidth * (this.world.viewport.cols - 2)) {
            this.world.scol += 1;
            if (this.world.scol === this.world.w) {
                this.world.scol = 0;
            }
        }
    }
    return 16;
};
GameStage.prototype.render = function render(ctx, forceRedraw) {
    var row, col, tileRow, tile, sliceD, viewport, x, y, w, h, s, dc, dr, sprite, r, c;
    //var rowCount, colCount;   
    ctx.lineWidth = 1;
    //calculates the viewport
    //TODO: change the last index with the tile num
    this.world.viewport = {
        rows: Math.floor(ctx.canvas.height / this.world.tileHeight * 2),
        cols: Math.floor(ctx.canvas.width / this.world.tileWidth)
    };

    //if (this.mouse) {
    //    if (this.world.srow >= 0 && this.world.srow <= this.world.h - viewport.rows + 2) {
    //        if (this.mouse.mousePos.y < this.world.tileWidth) {
    //            this.world.srow -= 1;
    //            if (this.world.srow < 0) {
    //                this.world.srow = 0;
    //            }
    //        } else if (this.mouse.mousePos.y > this.world.tileHeight/2 * (viewport.rows - 2)) {
    //            this.world.srow += 1;
    //            if (this.world.srow >= this.world.h - viewport.rows + 2) {
    //                this.world.srow = this.world.h - viewport.rows + 1;
    //            }
    //        }
    //    }
    //    if (this.mouse.mousePos.x < this.world.tileWidth*2) {
    //        this.world.scol -= 1;
    //        if (this.world.scol < 0) {
    //            this.world.scol += this.world.w;
    //        }
    //    } else if (this.mouse.mousePos.x > this.world.tileWidth * (viewport.cols - 2)) {
    //        this.world.scol += 1;
    //        if (this.world.scol === this.world.w) {
    //            this.world.scol = 0;
    //        }
    //    }
    //}

    //if(!this.world.rendered || forceRedraw) {
    ctx.strokeStyle = "whitesmoke";
    ctx.fillStyle = "#000000";
    ctx.fillRect(0, 0, ctx.canvas.width, ctx.canvas.height);
    ctx.fillStyle = "whitesmoke";
    ctx.font = "italic 8px sans";
    ctx.textAlign = "center";
    ctx.baseline = "middle";

    for (row = 0; row < this.world.viewport.rows; row++) {
        r = this.world.srow + row;
        for (col = this.world.viewport.cols; col >= 0; col--) {
            c = this.world.scol + col;
            if (c >= this.world.w) {
                c -= this.world.w;
            } else if (c < 0) {
                c = this.world.w + c;
            }
            r = this.world.srow + row;
            tile = this.world.tiles[r][c];
            tile.x = (row % 2) * this.world.tileWidth / 2 + (col - 1 / 2) * this.world.tileWidth;
            tile.y = (row - 1) / 2 * this.world.tileHeight;
            tile.render(ctx);
        }
    }

    //now render the scrolling line!
    ctx.strokeStyle = "goldenrod";
    ctx.font = "bold 28px Verdana";
    ctx.strokeText(this.world.srow + ":" + this.world.scol, ctx.canvas.width / 2, 10);

    //render heightMap
    //this.world.heightMap

    return ctx;
};

GameStage.prototype.generateWorld = function generateWorld(w, h, tw, th) {
    var row, col;
    var world = [];

    var height, umidity, temperature, rainfall;

    for (row = 0; row < h; row++) {
        world[row] = world[row] || [];
        for (col = 0; col < w; col++) {
            world[row][col] = new Tile(row, col, 0, 0, tw, th,
                this.AM.bundle.game[this.config.assets[Math.floor(Math.random() * this.config.assets.length)].id]);
        }
    }
    this.setTilesHeight(world, 100);
    return world;
};
GameStage.prototype.setTilesHeight = function setTilesHeight(tiles, maxHeight) {
    var row, col, tile, tl = tiles.length, tcl, value;
    noise.seed(Math.random());
    for (row = 0; row < tl; row++) {
        tcl = tiles[row].length;
        for (col = 0; col < tcl; col++) {
            tile = tiles[row][col];
            value = noise.simplex2(col / 100, row / 100);
            tile.elevation = Math.floor(value * maxHeight);
        }
    }
};

//<editor-fold desc="# IO Handlers">

GameStage.prototype.mouseMoveHandler = function (status) {
    var self = this;
    this.mouse = status.mouse;
    document.body.style.cursor = "default";
};
//GameStage.prototype.mouseDownHandler = function (status) {
//    var self = this;
//    var gridOffsetY = this.world.h;
//    var gridOffsetX = this.world.w;
//
//    gridOffsetX += ()
//};

//</editor-fold>