'use strict';

class GameScene extends Scene {
    constructor(engine) {
        const gameOptions = {
            assets: [],
            options: {

            }
        }
        super('gameScene', engine, gameOptions);
    }

    get gameStatus() {
        return this._engine.gameStatus;
    }

    onActivate() {
        const startX = 50;
        const startY = 50;
        const width = 200;
        const height = 150;
        const colors = ["#404b69", "#04322e", "#00818a", "#333333", "#aaaaaa"];
        const fontColors = ["#c0ffc2", "#00818a", "#f8c43a", "#ffffff", "#000000"];

        console.log("Scene.onActivate(): name = " + this.name);

        if (this._engine._config.debug) {
            console.log("Create HUD demo objects...");
            for (let w = 0; w < 5; w++) {
                const bgColor = colors[w];
                const fontColor = fontColors[w];
                const x = startX + w * (width + 50);

                this._gameObjects.push(new HWindow(this.mainCanvasCtx, x, startY, 0, 0, {
                    height,
                    width,
                    bgColor,
                    fontColor,
                    border: 2,
                    dropSwadow: w % 2 === 0
                }));
            }
        }
    }

    update(secondsPassed) {
        if (this.gameStatus.isRunning) {
            // Loop over all game objects
            for (let i = 0; i < this._gameObjects.length; i++) {
                this._gameObjects[i].update(this._engine.IM, secondsPassed);
            }
        } else if (this.gameStatus.isGameOver) {

        } else if (this._engine._config.debug) {
            // Loop over all game objects
            for (let i = 0; i < this._gameObjects.length; i++) {
                this._gameObjects[i].update(this._engine.IM, secondsPassed);
            }
        } else {}
    }

    lateUpdate(secondsPassed) {
        // Loop over all game objects
        for (let i = 0; i < this._gameObjects.length; i++) {
            this._gameObjects[i].lateUpdate(this._engine.IM, secondsPassed);
        }
    }

    draw(secondsPassed) {
        this._clearScene();
        this._engine._ctx.back.font = FONT_SIZES[MEDIA].XSmall + " " + FONT_FAMILY;
        this._engine._ctx.back.textAlign = "center";
        this._engine._ctx.back.fillStyle = 'black';

        if (this.gameStatus.isRunning) {
            // Loop over all game objects
            for (let i = 0; i < this._gameObjects.length; i++) {
                this._gameObjects[i].draw(secondsPassed);
            }
        } else if (this.gameStatus.isGameOver) {
            this._engine._ctx.back.fillText("GAME OVER ", this.backCanvas.width / 2, this.backCanvas.height / 2);
        } else if (this._engine._config.debug) {
            // draw demo elements
            this._engine._ctx.back.fillText("DEMO HUD mode ", this.backCanvas.width / 2, 15);

            // Loop over all game objects
            for (let i = 0; i < this._gameObjects.length; i++) {
                this._gameObjects[i].draw(secondsPassed);
            }
        } else {
            this._engine._ctx.back.fillText("SHOW MENU ", this.backCanvas.width / 2, this.backCanvas.height / 2);
        }
    }
}