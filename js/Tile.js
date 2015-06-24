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
    var x0 = this.x + this.w / 2,
        y0 = this.y + this.h / 2,
        x1 = this.x + this.w,
        y1 = this.y + this.h;

    ctx.fillStyle = DISPLAY_COLORS[this.type];
    ctx.beginPath();
    ctx.moveTo(this.x, y0);
    ctx.lineTo(x0, this.y);
    ctx.lineTo(x1, y0);
    ctx.lineTo(x0, y1);
    ctx.lineTo(this.x, y0);
    ctx.fill();
    ctx.stroke();

    ctx.fillStyle = "whitesmoke";
    ctx.fillText(this.r + "," + this.c, x0, y0, this.w);
    ctx.fillText(this.elevation, x0, y0 + 8, this.w);
    ctx.fillText(this.type, x0, y0 + 16);
};
Tile.prototype.renderSelected = function renderSelected(ctx) {
    // ctx.drawImage(this.img,
    //     this.x,
    //     this.y);
    var x0 = this.x + this.w / 2,
        y0 = this.y + this.h / 2,
        x1 = this.x + this.w,
        y1 = this.y + this.h;
    ctx.save();
    ctx.strokeStyle = "goldenrod";
    ctx.strokeWidth= 2;
    ctx.beginPath();
    ctx.moveTo(this.x, y0);
    ctx.lineTo(x0, this.y);
    ctx.lineTo(x1, y0);
    ctx.lineTo(x0, y1);
    ctx.lineTo(this.x, y0);
    ctx.stroke();
    ctx.restore();
};


Tile.prototype.getBiome = function getBiome(hNoise, mNoise, tNoise, rfNoise) {
    var maxHeight = 100;
    this.setElevation(hNoise, maxHeight);
    this.setMoisture(mNoise);
    this.setTemperature(tNoise);
    this.setRainfall(rfNoise);


    if (this.ocean) {
        this.type = 'OCEAN';
    } else if (this.water) {
        if (this.getRealElevation(this) < 0.05) this.type = 'MARSH';
        if (this.getRealElevation(this) > 0.4) this.type = 'ICE';
        this.type = 'LAKE';
    } else if (this.beach) {
        this.type = 'BEACH';
    } else if (this.elevation > maxHeight *0.4) {
        if (this.moisture > 0.50) this.type = 'SNOW';
        else if (this.moisture > 0.33) this.type = 'TUNDRA';
        else if (this.moisture > 0.16) this.type = 'BARE';
        else this.type = 'SCORCHED';
    } else if (this.elevation > maxHeight *0.3) {
        if (this.moisture > 0.66) this.type = 'TAIGA';
        else if (this.moisture > 0.33) this.type = 'SHRUBLAND';
        else this.type = 'TEMPERATE_DESERT';
    } else if (this.elevation > maxHeight *0.15) {
        if (this.moisture > 0.83) this.type = 'TEMPERATE_RAIN_FOREST';
        else if (this.moisture > 0.50) this.type = 'TEMPERATE_DECIDUOUS_FOREST';
        else if (this.moisture > 0.16) this.type = 'GRASSLAND';
        else this.type = 'TEMPERATE_DESERT';
    } else {
        if (this.moisture > maxHeight *0.66) this.type = 'TROPICAL_RAIN_FOREST';
        else if (this.moisture > 0.33) this.type = 'TROPICAL_SEASONAL_FOREST';
        else if (this.moisture > 0.16) this.type = 'GRASSLAND';
        else this.type = 'SUBTROPICAL_DESERT';
    }


    //identify the type of the tile
    if(this.elevation < -maxHeight * 0.2){
        this.type = "OCEAN";
        this.moisture = 1;
    }
    else if(this.elevation < maxHeight * 0.1){
        this.type = "COAST";
    }
    else if(this.elevation < maxHeight * 0.25){
        this.type = "BEACH";
    }
    else if(this.elevation < maxHeight * 0.75){
        if (this.moisture > 0.66) this.type =  'TROPICAL_RAIN_FOREST';
        else if (this.moisture > 0.33) this.type =  'TROPICAL_SEASONAL_FOREST';
        else if (this.moisture > 0.16) this.type =  'GRASSLAND';
        else this.type =  'SUBTROPICAL_DESERT';
    }
    else if(this.elevation < maxHeight * 0.9){
        this.type = "ROCK";
    }
    else {
        this.type = "ICE";
    }
    //
    ////now identify the correct biomass
    //if (this.elevation > 0.4) {
    //    if (this.moisture > 0.50) this.type =  'SNOW';
    //    else if (this.moisture > 0.33) this.type =  'TUNDRA';
    //    else if (this.moisture > 0.16) this.type =  'BARE';
    //    else this.type =  'SCORCHED';
    //} else if (this.elevation > 0.3) {
    //    if (this.moisture > 0.66) this.type =  'TAIGA';
    //    else if (this.moisture > 0.33) this.type =  'SHRUBLAND';
    //    else this.type =  'TEMPERATE_DESERT';
    //} else if (this.elevation > 0.15) {
    //    if (this.moisture > 0.83) this.type =  'TEMPERATE_RAIN_FOREST';
    //    else if (this.moisture > 0.50) this.type =  'TEMPERATE_DECIDUOUS_FOREST';
    //    else if (this.moisture > 0.16) this.type =  'GRASSLAND';
    //    else this.type =  'TEMPERATE_DESERT';
    //} else {
    //    if (this.moisture > 0.66) this.type =  'TROPICAL_RAIN_FOREST';
    //    else if (this.moisture > 0.33) this.type =  'TROPICAL_SEASONAL_FOREST';
    //    else if (this.moisture > 0.16) this.type =  'GRASSLAND';
    //    else this.type =  'SUBTROPICAL_DESERT';
    //}


};

Tile.prototype.setElevation = function setElevation(noise, maxHeight) {
    var val = noise.simplex2(this.c / 100, this.r / 100);
    this.elevation = Math.floor(val * maxHeight);
};

Tile.prototype.setMoisture = function setMoisture(noise) {
    this.moisture = this.moisture || noise.simplex2(this.c / 100, this.r / 100);
};

Tile.prototype.setTemperature = function setTemperature(noise, equator) {
    this.temperature = noise.simplex2(this.c / 100, this.r / 100);
};

Tile.prototype.setRainfall = function setRainfall(noise) {
    this.rainfall = noise.simplex2(this.c / 100, this.r / 100);
};
