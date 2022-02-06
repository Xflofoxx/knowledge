'use strict';

class Engine {
    constructor(config) {
        this._config = config;

        this._canvas = {
            main: null,
            back: null
        };
        this._ctx = {
            main: null,
            back: null
        };

        this._hud = null;
        this._playTime = 0.000;
        this._oldTimeStamp = 0;
        this._fps = 0;
        this.scenes = [];
        this.currentScene = null;
    }

    _resizeCanvas(canvas) {
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;
    }

    _draw(secondsPassed) {

        // Draw currnt scene
        this.currentScene.draw(secondsPassed);

        if (this._config.debug) {
            // Draw FPS
            this._drawFps(secondsPassed);
        }
    }
    _drawFps(secondsPassed) {
        // Calculate fps
        this._fps = Math.round(1 / secondsPassed);

        // Draw number to the screen
        this._ctx.back.font = '12px Arial';
        this._ctx.back.fillStyle = 'black';
        this._ctx.back.fillText("FPS: " + this._fps, 10, 17);
        this._ctx.back.fillText("Play time: " + Utils.Math.roundAccuratly(this._playTime, 3), 10, this._canvas.back.height - 5);
    }

    _update(secondsPassed) {
        this._playTime += secondsPassed;

        // Update currnt scene
        this.currentScene.update(secondsPassed);
    }

    async init() {
        console.log("Init engine");

        this._canvas.main = document.getElementById(this._config.ids.main);
        this._canvas.back = document.getElementById(this._config.ids.back);

        this._resizeCanvas(this._canvas.main);
        this._resizeCanvas(this._canvas.back);

        this._ctx.main = this._canvas.main.getContext('2d');
        this._ctx.back = this._canvas.back.getContext('2d');
        this._hud = document.getElementById(this._config.ids.hud);

        console.log("Fetch assets manifest");
        let assets = await fetch("/assets/gameAssets.json")
        assets = await assets.json();

        // TODO load scenes
        const splashScene = new Scene("splash", this);

        this.scenes.push(await splashScene.loadScene());

        console.log(assets);
    }

    async start() {
        console.log("Game loop started");

        // Select first scene
        this.currentScene = this.scenes.find(s => s.name === "splash");

        // Start The proper game loop
        this.gameLoop(this._oldTimeStamp);
    }

    gameLoop(timeStamp) {
        // Calculate the number of seconds passed since the last frame
        let secondsPassed = (timeStamp - this._oldTimeStamp) / 1000;

        // Move forward in time with a maximum amount
        secondsPassed = Math.min(secondsPassed, 0.1);
        this._oldTimeStamp = timeStamp;

        this._update(secondsPassed);

        // Perform the drawing operation
        this._draw(secondsPassed);

        // The loop function has reached it's end. Keep requesting new frames        
        window.requestAnimationFrame(this.gameLoop.bind(this));
    }
}


window.GameEngine = Engine;