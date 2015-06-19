/**
 * Created by dario.olivini on 15/06/2015.
 */
var Tile = function Tile(r, c, x,y, w, h, img){
    this.r = r;
    this.c = c;
    this.x = x;
    this.y = y;
    this.w = w;
    this.h=h;
    this.img = img;
};

Tile.prototype.update = function update(){

};

Tile.prototype.render = function render(ctx, forceRedraw){
    ctx.drawImage(this.img,
        this.x,
        this.y);
    ctx.fillStyle = "whitesmoke";
    ctx.textAlign = "center";
    ctx.textBaseline = "top";
    //ctx.beginPath();
    //ctx.moveTo(this.x, this.y+this.h/2);
    //ctx.lineTo(this.x + this.w/2, this.y - this.h/2);
    //ctx.lineTo(this.y - this.h/2, this.y+ this.w / 2);
    //ctx.lineTo(this.x + this.w / 2, this.y+ this.w / 2);
    //ctx.stroke();

    ctx.fillText(this.r + "," + this.c, this.x + this.w/2, this.y+this.h);
};