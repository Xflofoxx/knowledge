'use strict';

class GameStatus {
    constructor() {
        this.isRunning = false;
        this.isGameOver = false;
    }
}

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
        this._playTime = 0.000;
        this._oldTimeStamp = 0;
        this._fps = 0;


        this.assetsManagers = new Map();
        this.sceneFSM = new SceneFSM();
        this._sceneIDs = {
            splash: 0,
            game: 0
        }
        this.gameStatus = new GameStatus();
        this.IM = new InputManager(this);
    }

    _resizeCanvas(canvas) {
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;
    }

    _draw(secondsPassed) {

        // Draw current scene
        this.sceneFSM.draw(secondsPassed);

        if (this._config.debug) {
            // Draw FPS
            this._drawFps(secondsPassed);
        }
        this._drawIM(secondsPassed);
    }
    _drawFps(secondsPassed) {
        // Calculate fps
        this._fps = Math.round(1 / secondsPassed);

        this._ctx.back.save();
        this._ctx.back.font = FONT_SIZES[MEDIA].XSmall + "px " + FONT_FAMILY;

        // Draw number to the screen
        this._ctx.back.textAlign = "left";
        this._ctx.back.fillStyle = 'black';
        this._ctx.back.fillText("FPS: " + this._fps, 10, FONT_SIZES[MEDIA].XSmall);
        this._ctx.back.fillText("secondsPassed: " + secondsPassed, 10, 2 * FONT_SIZES[MEDIA].XSmall);
        if (this.gameStatus.isRunning) {
            this._ctx.back.fillText("Play time: " + Utils.Math.roundAccuratly(this._playTime, 3), 10, this._canvas.back.height - 5);
        } else {
            this._ctx.back.fillText("Game is not running.", 10, this._canvas.back.height - 5);
        }
        this._ctx.back.textAlign = "right";
        this._ctx.back.fillText(this.sceneFSM.currScene.drawSceneInfo(), this._canvas.back.width - 5, this._canvas.back.height - 5);
        this._ctx.back.restore();
    }

    _drawIM() {
        if (this._config.debug) {
            this.IM.drawInfos();
        }
        this.IM.draw();
    }

    _update(secondsPassed) {
        this._playTime += secondsPassed;

        // Update current scene
        this.sceneFSM.update(secondsPassed);
    }

    _lateUpdate(secondsPassed) {
        // Update current scene
        this.sceneFSM.lateUpdate(secondsPassed);
    }

    propagateClick(button) {
        this.sceneFSM.propagateClick(button);
    }

    async init() {
        console.log("Init engine");

        this._canvas.main = document.getElementById(this._config.ids.main);
        this._canvas.back = document.getElementById(this._config.ids.back);

        this._resizeCanvas(this._canvas.main);
        this._resizeCanvas(this._canvas.back);

        if (this._canvas.main.width < 480) {
            MEDIA = "phone";
        } else if (this._canvas.main.width < 768) {
            MEDIA = "tablet";
        } else {
            MEDIA = "desktop";
        }


        // attach windows.resize event
        window.addEventListener("resize", () => {
            this._resizeCanvas(this._canvas.main);
            this._resizeCanvas(this._canvas.back);

            if (this._canvas.main.width < 480) {
                MEDIA = "phone";
            } else if (this._canvas.main.width < 768) {
                MEDIA = "tablet";
            } else {
                MEDIA = "desktop";
            }

            // set the medium font
            this._ctx.main.font = FONT_SIZES[MEDIA].Medium + "px " + FONT_FAMILY;
            this._ctx.back.font = FONT_SIZES[MEDIA].Medium + "px " + FONT_FAMILY;

        }, true);

        this._ctx.main = this._canvas.main.getContext('2d');
        this._ctx.back = this._canvas.back.getContext('2d');

        // set the medium font
        this._ctx.main.font = FONT_SIZES[MEDIA].Medium + "px " + FONT_FAMILY;
        this._ctx.back.font = FONT_SIZES[MEDIA].Medium + "px " + FONT_FAMILY;

        this.IM.init(this._canvas.main);

        // Craetes all the scenes
        const splashScreenScene = new SplashScreenScene(this);
        const gameScene = new GameScene(this);

        this._sceneIDs.splash = await this.sceneFSM.add(splashScreenScene);
        this._sceneIDs.game = await this.sceneFSM.add(gameScene);

        splashScreenScene.targetSceneID = this._sceneIDs.game;

    }

    start() {
        console.log("Game loop started");

        this.sceneFSM.switchTo(this._sceneIDs.splash);
        // Start The proper game loop
        this.gameLoop(this._oldTimeStamp);
    }

    gameLoop(timeStamp) {
        // Calculate the number of seconds passed since the last frame
        let secondsPassed = (timeStamp - this._oldTimeStamp) / 1000;

        // Move forward in time with a maximum amount
        secondsPassed = Math.min(secondsPassed, 0.1);
        this._oldTimeStamp = timeStamp;

        // Perform game update
        this._update(secondsPassed);

        // Perform the drawing operation
        this._draw(secondsPassed);

        this._lateUpdate(secondsPassed);
        // The loop function has reached it's end. Keep requesting new frames        
        window.requestAnimationFrame(this.gameLoop.bind(this));
    }
}


window.GameEngine = Engine;