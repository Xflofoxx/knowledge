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

    update(secondsPassed) {

    }

    draw(secondsPassed) {
        this._clearScene();
        if (this.gameStatus.isRunning) {
            // Loop over all game objects
            for (let i = 0; i < this._gameObjects.length; i++) {
                this._gameObjects[i].draw(secondsPassed);
            }
        } else if (this.gameStatus.isGameOver) {
            this._engine._ctx.back.textAlign = "center";
            this._engine._ctx.back.font = '12px Arial';
            this._engine._ctx.back.fillStyle = 'black';
            this._engine._ctx.back.fillText("GAME OVER ", this.backCanvas.width / 2, this.backCanvas.height / 2);
        } else {
            this._engine._ctx.back.textAlign = "center";
            this._engine._ctx.back.font = '12px Arial';
            this._engine._ctx.back.fillStyle = 'black';
            this._engine._ctx.back.fillText("SHOW MENU ", this.backCanvas.width / 2, this.backCanvas.height / 2);
        }
    }
}