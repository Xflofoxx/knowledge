var Camera = function Camera(startX, startY, width, height) {
    //top-left cell iso coordinates
    this.srow = 0;
    this.scol = 0;

    //camera width
    this.w = width;
    this.h = height;

    //minimap sizes 4/3
    this.miniMap = {
        w : 200,
        h : 100
    }
    //maybe the viewport will be smaller than the entire canvas
    this.viewport = {};
    this.zoom = 1;

};

Camera.prototype.update = function update(ctx, world) {
    this.w = ctx.canvas.width;
    this.h = ctx.canvas.height;
    //this.worldW = world.w;
    //this.worldH = world.h;
    this.viewport = {
        rows: Math.floor(this.h * 2 / (world.tileHeight  * this.zoom)),
        cols: Math.floor(this.w / (world.tileWidth* this.zoom))
    };
};

Camera.prototype.render = function render(ctx, forceRedraw, world) {
    var row, col, r, c, tile;
    //ctx.save();
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

                    ctx.fillStyle = "whitesmoke";
                    ctx.fillText(r + "," + c, tile.x, tile.y-24, tile.w);
                    ctx.fillText("H:"+tile.elevation, tile.x, tile.y-16, tile.w);
                    ctx.fillText("M:"+tile.moisture, tile.x, tile.y-8, tile.w);
                    ctx.fillText(tile.temperature+"°", tile.x, tile.y, tile.w);
                    ctx.fillText(tile.rainfall, tile.x, tile.y+8, tile.w);
                }
            }
        }
    }
    //now render the scrolling line!
    ctx.strokeStyle = "goldenrod";
    ctx.font = "bold 28px Verdana";
    ctx.strokeText(this.srow + ":" + this.scol, ctx.canvas.width / 2, 10);
    //ctx.restore();
   
    //draw mini map!!
    ctx.drawImage(world.miniMap,0,0, world.miniMap.width,
         world.miniMap.height, 10, ctx.canvas.height - 10 - this.miniMap.h, this.miniMap.w,this.miniMap.h);
    ctx.strokeStyle = "black";
    ctx.strokeRect(10,ctx.canvas.height - 10- this.miniMap.h, this.miniMap.w,this.miniMap.h);
    ctx.lineWidth = 1;
    ctx.strokeStyle = "red";
    ctx.strokeRect(10+(this.scol* this.miniMap.w / world.miniMap.width),
        ctx.canvas.height - 10 - this.miniMap.h+(this.srow *this.miniMap.h/ world.miniMap.height),
        this.viewport.cols * this.miniMap.w / world.miniMap.width,
        (this.viewport.rows+2)*this.miniMap.h/world.miniMap.height);
};

Camera.prototype.selectTile = function selectTile(world, mouse){
    var r,c;
   
     r = Math.ceil(mouse.mousePos.y/world.tileHeight*2)+1;
     c = Math.abs(mouse.mousePos.x /world.tileWidth*2);   
     console.log(c, 
        r,
        world.tiles[this.srow+r][this.scol+c]);
    /*
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
    */
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
