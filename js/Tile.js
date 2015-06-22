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

var Tile = function Tile(r, c, x, y, w, h, img) {
    this.r = r;
    this.c = c;
    this.x = x;
    this.y = y;
    this.w = w;
    this.h = h;
    this.img = img;

    this.type = undefined;

    this.elevation = undefined;
    this.moisture = undefined;
    this.temperature = undefined;
    this.rainfall = undefined;
};

Tile.prototype.update = function update() {

};

Tile.prototype.render = function render(ctx, forceRedraw) {
    // ctx.drawImage(this.img,
    //     this.x,
    //     this.y);

    ctx.fillStyle = DISPLAY_COLORS[this.type];
    ctx.beginPath();
    ctx.moveTo(this.x, this.y + this.h / 2);
    ctx.lineTo(this.x + this.w / 2, this.y);
    ctx.lineTo(this.x + this.w, this.y + this.h / 2);
    ctx.lineTo(this.x + this.w / 2, this.y + this.h);
    ctx.lineTo(this.x, this.y + this.h / 2);
    ctx.fill();

    ctx.fillStyle = "whitesmoke";
    ctx.fillText(this.r + "," + this.c, this.x + this.w / 2, this.y + this.h / 2);
    ctx.fillText(this.elevation, this.x + this.w / 2, this.y + 8 +this.h / 2);
};

Tile.prototype.getBiome = function getBiome(hNoise,mNoise,tNoise,rfNoise) {
    var maxHeight = 100;
    this.setElevation(hNoise, maxHeight);

    //identify the type of the tile
    if(this.elevation < -maxHeight * 0.2){
        this.type = "OCEAN";
    }
    else if(this.elevation < maxHeight * 0.1){
        this.type = "COAST";
    }
    else if(this.elevation < maxHeight * 0.25){
        this.type = "BEACH";
    }
    else if(this.elevation < maxHeight * 0.75){
        this.type = "GRASSLAND";
    }
    else if(this.elevation < maxHeight * 0.9){
        this.type = "ROCK";
    }
    else {
        this.type = "SNOW";
    }

    this.setMoisture(mNoise);
    this.setTemperature(tNoise);
    this.setRainfall(rfNoise);

    //now identify the correct biomass


};

Tile.prototype.setElevation = function setElevation(noise, maxHeight) {
    var val= noise.simplex2(this.c / maxHeight, this.r / maxHeight);
    this.elevation = Math.floor(val * maxHeight);
};

Tile.prototype.setMoisture = function setMoisture(noise) {
    this.moisture = noise.simplex2(this.c / 100, this.r / 100);
};

Tile.prototype.setTemperature = function setTemperature(noise, equator) {
    this.temperature = noise.simplex2(this.c / 100, this.r / 100);
};

Tile.prototype.setRainfall = function setRainfall(noise) {
    this.rainfall = noise.simplex2(this.c / 100, this.r / 100);
};
