'use strict';

class Scene {

    constructor(name, engine) {
        this.name = name;
        this._engine = engine;

        this._gameObjects = [];

        this._gradientRadius = this._engine._canvas.back.width;
        this._gradientRadiusIncrement = 1;
    }

    _clearScene() {
        // Clear the entire canvas
        this._engine._ctx.main.clearRect(0, 0, this._engine._canvas.main.width, this._engine._canvas.main.height);
        // Clear the entire canvas
        this._engine._ctx.back.clearRect(0, 0, this._engine._canvas.back.width, this._engine._canvas.back.height);
        var gradient = this._engine._ctx.back.createRadialGradient(
            this._engine._canvas.back.width / 2,
            this._engine._canvas.back.height / 2,
            5,
            this._engine._canvas.back.width / 2,
            this._engine._canvas.back.height / 2,
            this._gradientRadius);
        gradient.addColorStop(0, 'white');
        gradient.addColorStop(1, 'black');
        this._engine._ctx.back.fillStyle = gradient;
        this._engine._ctx.back.fillRect(0, 0, this._engine._canvas.back.width, this._engine._canvas.back.height);
    }

    async loadScene() {
        console.log("Loading scene " + this.name);
        return this;
    }

    draw(secondsPassed) {
        this._clearScene();

        // Loop over all game objects
        for (let i = 0; i < this._gameObjects.length; i++) {
            this._gameObjects[i].draw(secondsPassed);
        }
    }

    update(secondsPassed) {
        // Change radius
        if (this._gradientRadius <= this._engine._canvas.back.width / 6) {
            this._gradientRadiusIncrement = Utils.Effects.easing.easeInOutElastic(secondsPassed, 10, 1, 30);
        } else if (this._gradientRadius >= this._engine._canvas.back.width) {
            this._gradientRadiusIncrement = -Utils.Effects.easing.easeInOutElastic(secondsPassed, 10, 1, 30);
        }

        this._gradientRadius += this._gradientRadiusIncrement;

        // Loop over all game objects
        for (let i = 0; i < this._gameObjects.length; i++) {
            this._gameObjects[i].update(secondsPassed);
        }
    }
}