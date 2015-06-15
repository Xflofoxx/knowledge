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
        people : []
    }, AM, IO);
    this.LOG_TAG = "GameStage:";

    this.world = {
        rendered: false,
        tiles: [],
        tileWidth: 64,
        tileHeight: 32,
        w: 20,
        h: 20,
        row: 0,
        col: 0
    };
};
utils.inherit(GameStage, Stage);
GameStage.prototype.registerObjects = function registerObjects() {
    var row, col;
    Stage.prototype.registerObjects.call(this);
    //prepare the world

    for (row = 0; row < this.world.h; row++) {
        this.world.tiles[row] = this.world.tiles[row] || [];
        for (col = 0; col < this.world.w; col++) {
            //this.world.tiles[row][col] = new Tile(row, col, 0,0, this.world.tileWidth, this.world.tileHeight,
            //    this.AM.bundle.game[this.config.assets[Math.floor(Math.random() * this.config.assets.length)].id]);
            this.world.tiles[row][col] = new Tile(row, col, 0,0, this.world.tileWidth, this.world.tileHeight,
                this.AM.bundle.game[this.config.assets[utils.tileMap[row][col]].id]);
        }
    }

    this.config.people.push({
        name: "firstMan",
        sex: "m",
        r: 0,
        c: 0
    });
};
GameStage.prototype.update = function update(time) {
    //console.log(this.LOG_TAG + " update");
    return 16;
};
GameStage.prototype.render = function render(ctx, forceRedraw) {
    var row, col, tileRow,tile, sliceD, viewport, x, y, w, h, centerX,centerY, s, sprite;
    ctx.strokeColor = "#f0f0f0";
    ctx.lineWidth = 1;
    //calculates the viewport
    //TODO: change the last index with the tile num
    viewport = {
        rows: 19,
        cols: 19
    };


    centerX = (ctx.canvas.width-(viewport.cols/2 * (this.world.tileWidth+ this.world.tileWidth)))/2;
    centerY = ctx.canvas.height/2;

    if(!this.world.rendered || forceRedraw) {
        ctx.fillStyle="#000000";
        ctx.fillRect(0,0,ctx.canvas.width,ctx.canvas.height);
        ctx.font = "normal 8px Verdana";
        ctx.textAlign = "center";
        ctx.baseline = "middle";
        for (row = 0; row < viewport.rows; row++) {
            x = centerX + (row * this.world.tileWidth / 2);
            y = centerY + (row * this.world.tileHeight / 2);
            for (col = viewport.cols; col >= 0; col--) {
                tile = this.world.tiles[row][col];
                tile.x = centerX + (col * this.world.tileWidth / 2) + (row * this.world.tileWidth / 2);
                tile.y = centerY + (row * this.world.tileHeight / 2) - (col * this.world.tileHeight / 2);
                tile.render(ctx);
            }

        }
        this.world.rendered = true;
    }

    for(s = 0;s < this.config.people.length; s++){
        sprite = this.config.people[s];
        ctx.fillStyle="goldenrod";
        ctx.strokeStyle="white";
        ctx.beginPath();
        ctx.moveTo(centerX + (sprite.c * this.world.tileWidth / 2) + (sprite.r * this.world.tileWidth / 2), centerY + (sprite.r * this.world.tileHeight / 2) - (sprite.c * this.world.tileHeight / 2));
        ctx.lineTo(100,100);
        //sprite.x = centerX + (sprite.c * this.world.tileWidth / 2) + (sprite.r * this.world.tileWidth / 2);
        //sprite.y = centerY + (sprite.r * this.world.tileHeight / 2) - (sprite.c * this.world.tileHeight / 2);
        //ctx.fillRect(sprite.x, sprite.y, this.world.tileWidth, this.world.tileHeight);
        ctx.stroke();
    }


    return ctx;
};

//<editor-fold desc="# IO Handlers">

GameStage.prototype.mouseMoveHandler = function (status) {
    var self = this;
    var btn, b, hit;
    //for (b = 0; b < self.config.buttons.length; b++) {
    //    btn = self.config.buttons[b];
    //    if (btn.size) {
    //        if (btn.size.x < status.mouse.mousePos.x
    //            && btn.size.x + btn.size.w > status.mouse.mousePos.x
    //            && btn.size.y < status.mouse.mousePos.y
    //            && btn.size.y + btn.size.h > status.mouse.mousePos.y
    //            && !btn.disabled
    //        ) {
    //            hit = true;
    //            btn.hover = true;
    //        } else {
    //            btn.hover = false;
    //        }
    //    } else {
    //        btn.hover = false;
    //    }
    //}
    //if (hit) {
    //    document.body.style.cursor = "pointer";
    //} else {
    document.body.style.cursor = "default";
    //}
};
//</editor-fold>