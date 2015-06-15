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
        settings: settings
    }, AM, IO);
    this.LOG_TAG = "GameStage:";

    this.world = {
        rendered: false,
        tiles: [],
        tileWidth: 64,
        tileHeight: 32,
        w: 200,
        h: 200,
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
            //this.world.tiles[row][col]= {
            //r: row,
            //c: col,
            //x: this.world.tileWidth * (col),
            //y: this.world.tileHeight * (row),
            //w: this.world.tileWidth,
            //h: this.world.tileHeight,
            //z: 0,
            //type: tileTypes[Math.floor(Math.random()*tileTypes.length)]
            //};
            this.world.tiles[row][col] = this.config.assets[Math.floor(Math.random() * this.config.assets.length)].id;
        }
    }
};
GameStage.prototype.update = function update(time) {
    //console.log(this.LOG_TAG + " update");
    return 16;
};
GameStage.prototype.render = function render(ctx, forceRedraw) {
    var row, col, tileRow,tile, sliceD, viewport, x, y, w, h, centerX,centerY;
    ctx.strokeColor = "#f0f0f0";
    ctx.lineWidth = 1;
    //calculates the viewport
    viewport = {
        rows: 15,
        cols: 15
    };
    w = this.world.tileWidth;
    h = this.world.tileHeight;

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
                x = centerX + (col * this.world.tileWidth / 2) + (row * this.world.tileWidth / 2);
                y = centerY + (row * this.world.tileHeight / 2) - (col * this.world.tileHeight / 2);

                ctx.drawImage(this.AM.bundle.game[tile],
                    x,
                    y);
                ctx.fillStyle = "#f0f0f0";
                ctx.fillText(row + "," + col, x + w / 2, y + h);
            }

        }
        this.world.rendered = true;
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