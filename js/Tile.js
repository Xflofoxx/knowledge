/**
 * Created by dario.olivini on 15/06/2015.
 */

var DISPLAY_COLORS = {
    OCEAN: '#82caff',
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

var Tile = function Tile(r, c, x, y, w, h, img) {
    this.r = r;
    this.c = c;
    this.x = x;
    this.y = y;
    this.w = w;
    this.h = h;
    this.img = img;

    this.elevation = undefined;
    this.umidity = undefined;
    this.temperature = undefined;
    this.rainfall = undefined;
};

Tile.prototype.update = function update() {

};

Tile.prototype.render = function render(ctx, forceRedraw) {
    // ctx.drawImage(this.img,
    //     this.x,
    //     this.y);
    if (this.elevation < 0) {
        ctx.fillStyle = DISPLAY_COLORS.OCEAN;
    } else if (this.elevation < 20) {
        ctx.fillStyle = DISPLAY_COLORS.BEACH;
    } else if (this.elevation < 80) {
        ctx.fillStyle = DISPLAY_COLORS.GRASSLAND;
    } else if (this.elevation < 85) {
        ctx.fillStyle = DISPLAY_COLORS.ROCK;
    } else {
        ctx.fillStyle = DISPLAY_COLORS.SNOW;
    }
    ctx.beginPath();
    ctx.moveTo(this.x, this.y + this.h / 2);
    ctx.lineTo(this.x + this.w / 2, this.y);
    ctx.lineTo(this.x + this.w, this.y + this.h / 2);
    ctx.lineTo(this.x + this.w / 2, this.y + this.h);
    ctx.lineTo(this.x, this.y + this.h / 2);
    ctx.fill();
    //ctx.stroke();
    ctx.fillStyle = "whitesmoke";
    ctx.fillText(this.r + "," + this.c, this.x + this.w / 2, this.y + this.h / 2);
    ctx.fillText(this.elevation, this.x + this.w / 2, this.y + 8 +this.h / 2);
};