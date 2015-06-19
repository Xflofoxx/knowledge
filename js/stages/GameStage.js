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
        scroll: {
            startX: 0,
            startY: 0,
            x:0,
            y:0,
            dirX:undefined,
            dirY:undefined
        }
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
            this.world.tiles[row][col] = new Tile(row, col, 0, 0, this.world.tileWidth, this.world.tileHeight,
                this.AM.bundle.game[this.config.assets[Math.floor(Math.random() * this.config.assets.length)].id]);
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
    var row, col, tileRow, tile, sliceD, viewport, x, y, w, h, s,dc,dr, sprite,r,c;
    //var rowCount, colCount;   
    ctx.lineWidth = 1;
    //calculates the viewport
    //TODO: change the last index with the tile num
    viewport = {
        rows:  Math.floor(ctx.canvas.height / this.world.tileHeight*2),
        cols: Math.floor(ctx.canvas.width / this.world.tileWidth)
    };

    dc = Math.floor(this.world.scroll.x / this.world.tileWidth);
    dr = Math.floor(this.world.scroll.y / this.world.tileHeight);
    this.world.scol += dc;
    this.world.srow = dr;

    //centerX = ctx.canvas.width / 2 - this.world.tileWidth / 2;
    //centerY = 0; //ctx.canvas.height / 2;

    //if(!this.world.rendered || forceRedraw) {
    ctx.fillStyle = "#000000";
    ctx.fillRect(0, 0, ctx.canvas.width, ctx.canvas.height);
    ctx.font = "italic 8px sans";
    ctx.textAlign = "center";
    ctx.baseline = "middle";
    for (col = viewport.cols ; col >=0; col--) {
        c = this.world.scol + col;
        if(c < 0){
            c = this.world.w + c;
            this.world.scol += this.world.w;
        }else if(c === this.world.w){
            c = 0;            
            this.world.scol = c;
        }else if(c > this.world.w){
            c = c - this.world.w;                 
            this.world.scol -= this.world.w;
        }
        for (row = 0; row < viewport.rows+1; row++) {
            tile = this.world.tiles[row][c];
            tile.x = (row%2) * this.world.tileWidth/2 + col * this.world.tileWidth - this.world.tileHeight;
            tile.y = row * this.world.tileHeight/2 - this.world.tileHeight;
            tile.render(ctx);
        }
    }


        //for (col = 0; col < viewport.cols; col++) {
    //    c = col - this.world.scol;
    //    if(c < 0){
    //        c = 0;
    //    }
    //    for (row = 0; row < viewport.rows; row++) {
    //        r = row - this.world.srow;
    //        if(r<0){
    //            r = 0;
    //        }
    //        tile = this.world.tiles[r][c];
    //        tile.x = centerX + (col - row) * this.world.tileHeight;
    //        tile.y = centerY + (row + col) * this.world.tileHeight / 2;
    //        tile.render(ctx);
    //    }
    //
    //}
    //this.world.rendered = true;
    //}

    //now render the scrolling line!
    ctx.strokeStyle= "#ffffff";
    ctx.beginPath();
    ctx.moveTo(this.world.scroll.startX, this.world.scroll.startY);
    ctx.lineTo(this.world.scroll.startX + this.world.scroll.x, this.world.scroll.startY - this.world.scroll.y);
    ctx.stroke();

    return ctx;
};

//<editor-fold desc="# IO Handlers">

GameStage.prototype.mouseMoveHandler = function (status) {
    var self = this;
    var btn, b, hit;
    if(status.mouse.isDragging){
        this.world.scroll= {
            startX: status.mouse.mousePos.scrollX,
            startY: status.mouse.mousePos.scrollY,
            x: status.mouse.mousePos.deltaX,
            y: status.mouse.mousePos.deltaY,
            dirX: status.mouse.mousePos.scrollDirX,
            dirY:status.mouse.mousePos.scrollDirY
        }
    }else {
        this.world.scroll= {
            startX: 0,
            startY: 0,
            x: 0,
            y: 0,
            dirX: 0,
            dirY: 0
        }
    }
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
//GameStage.prototype.mouseDownHandler = function (status) {
//    var self = this;
//    var gridOffsetY = this.world.h;
//    var gridOffsetX = this.world.w;
//
//    gridOffsetX += ()
//};

//</editor-fold>