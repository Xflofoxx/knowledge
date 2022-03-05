'use strict';

class SplashScreenScene extends Scene {
    constructor(engine) {
        const splashOptions = {
            assets: [{
                id: "logo",
                type: "image",
                uri: "/assets/lumi.png"
            }],
            options: {
                showForSeconds: 5
            }
        }
        super('splashScreen', engine, splashOptions);

        this._currentSeconds = 0;
        this.targetSceneID = 0;

        this._gradientRadius = this._engine._canvas.back.width;
        this._gradientRadiusIncrement = 1;
    }

    update(secondsPassed) {
        this._currentSeconds += secondsPassed;
        if (this._currentSeconds >= this._config.showForSeconds) {
            // Switches states.
            this._engine.sceneFSM.switchTo(this.targetSceneID);
        }
        // Change radius
        if (this._gradientRadius <= this.backCanvas.width / 6) {
            this._gradientRadiusIncrement = Utils.Effects.easing.easeInQuad(secondsPassed, 10, 1, 30);
        } else if (this._gradientRadius >= this.backCanvas.width) {
            this._gradientRadiusIncrement = -Utils.Effects.easing.easeInOutElastic(secondsPassed, 10, 1, 30);
        }

        this._gradientRadius += this._gradientRadiusIncrement;
    }

    draw(secondsPassed) {
        const canvasHalfWidth = this.backCanvas.width / 2;
        const canvasHalfHeight = this.backCanvas.height / 2;
        const gradient = this._engine._ctx.back.createRadialGradient(
            canvasHalfWidth, canvasHalfHeight, 5,
            canvasHalfWidth, canvasHalfHeight, this._gradientRadius);
        const logo = this._engine.assetsManagers.get('logo');
        this._clearScene();

        gradient.addColorStop(0, 'white');
        gradient.addColorStop(1, 'black');
        this._engine._ctx.back.fillStyle = gradient;
        this._engine._ctx.back.fillRect(0, 0, this.backCanvas.width, this.backCanvas.height);
        this._engine._ctx.back.drawImage(logo, canvasHalfWidth - logo.width / 2, canvasHalfHeight - logo.height / 2);
    }

    drawSceneInfo() {
        return "Playing stage " + this.name + " for " + Utils.Math.roundAccuratly(this._currentSeconds, 2) + " seconds.";
    }
}