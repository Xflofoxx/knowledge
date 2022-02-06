'use strict';

class GameObject {
    constructor(context, x, y, vx, vy) {
        this._context = context;
        this._x = x;
        this._y = y;
        this._vx = vx;
        this._vy = vy;

        this._isColliding = false;
    }

    get isColliding() {
        return this._isColliding;
    }
}

class Square extends GameObject {
    constructor(context, x, y, vx, vy) {
        super(context, x, y, vx, vy);

        // Set default width and height
        this.width = 50;
        this.height = 50;
    }

    draw() {
        // Draw a simple square
        this.context.fillStyle = this.isColliding ? '#ff8080' : '#0099b0';
        this.context.fillRect(this.x, this.y, this.width, this.height);
    }

    update(secondsPassed) {
        // Move with set velocity
        this.x += this.vx * secondsPassed;
        this.y += this.vy * secondsPassed;
    }
}