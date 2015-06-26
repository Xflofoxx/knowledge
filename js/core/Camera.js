var Camera = function Camera(startX, startY, width, height) {
    //top-left cell iso coordinates
    this.srow = 0;
    this.scol = 0;

    //camera width
    this.w = width;
    this.h = height;

    //world sizes - random starting value
    this.worldW = 1000;
    this.worldH = 1000;
    //maybe the viewport will be smaller than the entire canvas
    this.viewport = {};
    this.zoom = 1;
};

Camera.prototype.update = function update(ctx, world) {
    this.w = ctx.canvas.width;
    this.h = ctx.canvas.height;
    this.worldW = world.w;
    this.worldH = world.h;
    this.viewport = {
        rows: Math.floor(this.h * 2 / (world.tileHeight  * this.zoom)),
        cols: Math.floor(this.w / (world.tileWidth* this.zoom))
    };
    console.log("Camera update",this.zoom, this.viewport, world.tileWidth, world.tileHeight)
};

Camera.prototype.render = function render(ctx, forceRedraw, world) {
    var row, col, r, c, tile;
    ctx.save();
    ctx.lineWidth = 1;
    ctx.strokeStyle = "whitesmoke";
    ctx.fillStyle = "whitesmoke";
    ctx.font = "italic 8px sans";
    ctx.textAlign = "center";
    ctx.baseline = "middle";
    for (row = 0; row < this.viewport.rows+2; row++) {
        r = this.srow + row;
        if (r >= world.h) {
            r = world.h - 1;
        }
        if (world.tiles[r]) {
            for (col = this.viewport.cols; col >= 0; col--) {
                c = this.scol + col;
                if (c >= world.w) {
                    c -= world.w;
                } else if (c < 0) {
                    c = this.world.w + c;
                }
                tile = world.tiles[r][c];
                if (tile) {
                    tile.x = (r % 2) * tile.w * this.zoom / 2 + (col - 1 / 2) * tile.w * this.zoom;
                    tile.y = (row - 1) / 2 * tile.h* this.zoom;
                    tile.render(ctx, forceRedraw,  this.zoom);
                }
            }
        }
    }
    //now render the scrolling line!
    ctx.strokeStyle = "goldenrod";
    ctx.font = "bold 28px Verdana";
    ctx.strokeText(this.srow + ":" + this.scol, ctx.canvas.width / 2, 10);
    ctx.restore();
};

Camera.prototype.selectTile = function selectTile(world, mouse){
    var r,c;
    for (r = this.srow; r < this.viewport.rows+2; r++) {
        for (c = this.scol; c < this.viewport.cols; c++) {
            if(world.tiles[r][c] && mouse.mousePos.x > world.tiles[r][c].x && 
                mouse.mousePos.x < world.tiles[r][c].x + world.tiles[r][c].w && 
 mouse.mousePos.y > world.tiles[r][c].y && 
                mouse.mousePos.y < world.tiles[r][c].y + world.tiles[r][c].h){
                    return world.tiles[r][c];
                }
        }
    }
    return null;                    
};

Camera.prototype.moveRight = function moveRight(max) {
    this.scol += 1;
    if (this.scol === max) {
        this.scol = 0;
    }
};
Camera.prototype.moveLeft = function moveLeft(max) {
    this.scol -= 1;
    if (this.scol < 0) {
        this.scol += max;
    }
};
Camera.prototype.moveUp = function moveUp(max) {
    if (this.srow > 0) {
        this.srow -= 1;
    }
};
Camera.prototype.moveDown = function moveDown(max) {
    if (this.srow < max - this.viewport.rows-2) {
        this.srow += 1;
    }
};
Camera.prototype.changeZoom = function changeZoom(amount){
    //zoom range from -2 to 2
    if(this.zoom <= -2){
        this.zoom = -2;
    } else if(this.zoom >= 2) {
        this.zoom = 2;
    }else{
        this.zoom += amount/2;
        console.log("Current zoom",this.zoom, amount);
    }
};
