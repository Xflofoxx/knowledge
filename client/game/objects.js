'use strict';

class GameObject {
    constructor(context, x, y, vx, vy) {
        this.context = context;
        this.x = x;
        this.y = y;
        this.vx = vx;
        this.vy = vy;

        this.isColliding = false;
        this.hasMouseOver = false;
    }

    onClick(button) {
        console.log("Clicked " + button + " on " + this.constructor.name);
    }
}