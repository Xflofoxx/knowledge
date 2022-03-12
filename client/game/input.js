'use strict';

class InputManager {
    constructor(engine) {
        this._engine = engine;
        this.canvas = null;

        this.keyStates = new Array(256);
        this.mouseX = 0;
        this.mouseY = 0;
        this.mouseDirection = {
            N: 0,
            NE: 0,
            E: 0,
            SE: 0,
            S: 0,
            SW: 0,
            W: 0,
            NW: 0
        }

        this.mouseDown = new Array(4);
        this.mouseDragging = {
            startX: 0,
            startY: 0,
            dX: 0,
            dY: 0
        };
        this.mouseIsDragging = false;
    }


    init(canvas) {
        this.canvas = canvas;
        this.canvas.focus();
        this.canvas.addEventListener('mousemove', e => this.onMouseMove(e), false);
        this.canvas.addEventListener('mousedown', e => this.onMouseDown(e), false);
        this.canvas.addEventListener('mouseup', e => this.onMouseUp(e), false);
        this.canvas.addEventListener('keydown', e => this.onKeyDown(e), false);
        this.canvas.addEventListener('keyup', e => this.onKeyUp(e), false);
    }

    setCursor(cursor) {
        if (this.canvas.style.cursor !== cursor) {
            this.canvas.style.cursor = cursor;
            console.log("this.canvas.style.cursor " + cursor + " " + this.canvas.id);
        }
    }

    onKeyDown(e) {
        this.keyStates[e.keyCode] = e.key;
        if (this.keyStates[e.keyCode] === " ") {
            this.keyStates[e.keyCode] = "<space>";
        }
    }
    onKeyUp(e) {
        this.keyStates[e.keyCode] = false;
    }
    onMouseMove(e) {
        const rect = this.canvas.getBoundingClientRect();
        const prevX = this.mouseX;
        const prevY = this.mouseY;

        this.mouseX = e.clientX - rect.left;
        this.mouseY = e.clientY - rect.top;


        if (prevX < this.mouseX) {
            this.mouseDirection.W = false;
            this.mouseDirection.E = true;
        } else {
            this.mouseDirection.W = true;
            this.mouseDirection.E = false;
        }
        if (prevY < this.mouseY) {
            this.mouseDirection.S = true;
            this.mouseDirection.N = false;
        } else {
            this.mouseDirection.S = false;
            this.mouseDirection.N = true;
        }

        this.mouseDirection.NE = this.mouseDirection.N && this.mouseDirection.E;
        this.mouseDirection.SE = this.mouseDirection.S && this.mouseDirection.E;
        this.mouseDirection.NW = this.mouseDirection.N && this.mouseDirection.W;
        this.mouseDirection.SW = this.mouseDirection.S && this.mouseDirection.W;

        if (this.mouseDown[0] !== false) {
            if (!this.mouseIsDragging) {
                this.mouseIsDragging = true;
                this.mouseDragging = {
                    startX: this.mouseX,
                    startY: this.mouseY
                };
            }
            this.mouseDragging.dX = this.mouseX - this.mouseDragging.startX;
            this.mouseDragging.dY = this.mouseY - this.mouseDragging.startY;
        } else {
            this.mouseIsDragging = false;
            this.mouseDragging = {
                startX: 0,
                startY: 0,
                dX: 0,
                dY: 0
            };
        }
    }
    onMouseDown(e) {
        this.mouseDown[e.button] = e.button;
    }
    onMouseUp(e) {
        if (this.mouseDown[e.button] !== false) {
            this._engine.propagateClick(e.button);
        }
        this.mouseDown[e.button] = false;
    }

    drawInfos() {
        // Draw number to the screen
        this._engine._ctx.main.save();
        this._engine._ctx.main.textAlign = "right";
        this._engine._ctx.main.font = FONT_SIZES[MEDIA].XSmall + "px " + FONT_FAMILY;
        this._engine._ctx.main.fillStyle = 'black';
        this._engine._ctx.main.fillText("Keys: " + this.keyStates.filter(k => k !== false).join(", "),
            this.canvas.width - 5, FONT_SIZES[MEDIA].XSmall);
        this._engine._ctx.main.fillText("x: " + this.mouseX + " - y:" + this.mouseY + " - mouseDown: " + this.mouseDown.filter(k => k !== false).join(", ") + " ",
            this.canvas.width - 5, 2 * FONT_SIZES[MEDIA].XSmall);
        this._engine._ctx.main.fillText("Direction: " + JSON.stringify(this.mouseDirection), this.canvas.width - 5, 3 * FONT_SIZES[MEDIA].XSmall);
        if (this.mouseIsDragging) {
            this._engine._ctx.main.fillText("Mouse is dragging", this.canvas.width - 5, 4 * FONT_SIZES[MEDIA].XSmall);
            this._engine._ctx.main.fillText("x: " + this.mouseDragging.startX + " - y: " + this.mouseDragging.startY,
                this.canvas.width - 5, 5 * FONT_SIZES[MEDIA].XSmall);
            this._engine._ctx.main.fillText("dx: " + this.mouseDragging.dX + " - dy: " + this.mouseDragging.dY,
                this.canvas.width - 5, 6 * FONT_SIZES[MEDIA].XSmall);
        } else {
            this._engine._ctx.main.fillText("Mouse is not dragging", this.canvas.width - 5, 4 * FONT_SIZES[MEDIA].XSmall);
        }
        this._engine._ctx.main.restore();
    }

    draw(secondsPassed) {
        if (this.mouseIsDragging) {
            this._engine._ctx.main.save();
            this._engine._ctx.main.beginPath();
            this._engine._ctx.main.setLineDash([2, 2]);
            this._engine._ctx.main.strokeStyle = '#cccccc';
            this._engine._ctx.main.lineWidth = 2;
            this._engine._ctx.main.rect(this.mouseDragging.startX, this.mouseDragging.startY, this.mouseDragging.dX, this.mouseDragging.dY);
            this._engine._ctx.main.stroke();
            this._engine._ctx.main.restore();
        }
    }
}