'use strict';

class InputManager {
    constructor(engine) {
        this._engine = engine;

        this.keyStates = new Array(256);
        this.mouseX = 0;
        this.mouseY = 0;

        this.mouseDown = new Array(4);
    }


    init(canvas) {
        canvas.focus();
        canvas.addEventListener('mousemove', e => this.onMouseMove(e), false);
        canvas.addEventListener('mousedown', e => this.onMouseDown(e), false);
        canvas.addEventListener('mouseup', e => this.onMouseUp(e), false);
        canvas.addEventListener('keydown', e => this.onKeyDown(e), false);
        canvas.addEventListener('keyup', e => this.onKeyUp(e), false);
    }

    onKeyDown(e) {
        this.keyStates[e.keyCode] = e.key;
        // console.log("InputManager.onKyDown", e, this.keyStates);
    }
    onKeyUp(e) {
        this.keyStates[e.keyCode] = false;
        // console.log("InputManager.onKeyUp", e, this.keyStates);
    }
    onMouseMove(e) {
        const rect = this._engine._canvas.back.getBoundingClientRect();

        this.mouseX = e.clientX - rect.left;
        this.mouseY = e.clientY - rect.top;
        //console.log("InputManager.onMouseMove", e);
    }
    onMouseDown(e) {
        this.mouseDown[e.button] = e.button;
        //console.log("InputManager.onMouseDown", e.button, this.mouseDown);
    }
    onMouseUp(e) {
        this.mouseDown[e.button] = false;
        //console.log("InputManager.onMouseUp", e.button, this.mouseDown);
    }

    draw() {
        // Draw number to the screen
        this._engine._ctx.back.save();
        this._engine._ctx.back.textAlign = "right";
        this._engine._ctx.back.font = '12px Arial';
        this._engine._ctx.back.fillStyle = 'black';
        this._engine._ctx.back.fillText("Keys: " + this.keyStates.filter(k => k !== false).join(", "),
            this._engine._canvas.back.width - 5, 15);
        this._engine._ctx.back.fillText("x: " + this.mouseX + " - y:" + this.mouseY + " - mouseDown: " + this.mouseDown.filter(k => k !== false).join(", ") + " ",
            this._engine._canvas.back.width - 5, 30);
        this._engine._ctx.back.restore();
    }
}