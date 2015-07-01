/**
 * Created by dario.olivini on 15/06/2015.
 */

var DISPLAY_COLORS = {
    OCEAN: '#0094ff',
    COAST: '#82caff',
    BEACH: '#ffe98d',
    LAKE: '#2f9ceb',
    RIVER: '#369eea',
    SOURCE: '#00f',
    MARSH: '#2ac6d3',
    ICE: '#b3deff',
    ROCK: '#535353',
    LAVA: '#e22222',
    SNOW: '#f8f8f8',

    TUNDRA: '#ddddbb',
    BARE: '#bbbbbb',
    SCORCHED: '#999999',
    TAIGA: '#ccd4bb',
    SHRUBLAND: '#c4ccbb',
    TEMPERATE_DESERT: '#e4e8ca',
    TEMPERATE_RAIN_FOREST: '#a4c4a8',
    TEMPERATE_DECIDUOUS_FOREST: '#b4c9a9',
    GRASSLAND: '#c4d4aa',
    TROPICAL_RAIN_FOREST: '#9cbba9',
    TROPICAL_SEASONAL_FOREST: '#a9cca4',
    SUBTROPICAL_DESERT: '#e9ddc7'
};

var Tile = function Tile(r, c, x, y, w, h) {
    this.r = r;
    this.c = c;
    this.x = x;
    this.y = y;
    this.w = w;
    this.h = h;
    this.img = undefined;

    this.type = undefined;

    this.elevation = undefined;
    this.moisture = undefined;
    this.temperature = undefined;
    this.rainfall = undefined;
};

Tile.prototype.update = function update() {

};

Tile.prototype.render = function render(ctx, forceRedraw, zoom) {    
    
    var x0 = this.x + this.w * zoom / 2,
        y0 = this.y + this.h * zoom/ 2,
        x1 = this.x + this.w* zoom,
        y1 = this.y + this.h* zoom;
    
    if(this.img){
        ctx.drawImage(this.img,this.x,this.y,this.w* zoom,this.h* zoom);
    }else{        
        ctx.beginPath();
        
        ctx.moveTo(this.x, y0);
        ctx.lineTo(x0, this.y);
        ctx.lineTo(x1, y0);
        ctx.lineTo(x0, y1);
        ctx.lineTo(this.x, y0);
        ctx.fillStyle = DISPLAY_COLORS[this.type];
        ctx.fill();
        ctx.stroke();
    }
};
Tile.prototype.renderSelected = function renderSelected(ctx, zoom) {
    var x0 = this.x + this.w *zoom / 2,
        y0 = this.y + this.h *zoom/ 2,
        x1 = this.x + this.w*zoom,
        y1 = this.y + this.h*zoom;
    //ctx.save();
    ctx.strokeStyle = "goldenrod";
    ctx.fillStyle = "rgba(100,100,0,0.2)";
    ctx.strokeWidth = 2;
    ctx.beginPath();
    ctx.moveTo(this.x, y0);
    ctx.lineTo(x0, this.y);
    ctx.lineTo(x1, y0);
    ctx.lineTo(x0, y1);
    ctx.lineTo(this.x, y0);
    ctx.fill();
    ctx.stroke();
    //ctx.restore();
};
