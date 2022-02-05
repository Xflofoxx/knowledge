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
    }

    _resizeCanvas(canvas) {
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;
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

    }

    async start() {
        console.log("Started engine");
    }
}


window.GameEngine = Engine;

window.requestAnimFrame =
    window.requestAnimationFrame ||
    window.webkitRequestAnimationFrame ||
    window.mozRequestAnimationFrame ||
    window.oRequestAnimationFrame ||
    window.msRequestAnimationFrame ||
    function(callback) {
        window.setTimeout(callback, 1000 / 60);
    };

let game;

window.onload = function() {
    game = new window.GameEngine({
        ids: {
            main: "mainCanvas",
            back: "backCanvas",
            hud: "hud"
        }
    });

    game.init()
        .then(game.start())
        .catch(error => {
            console.error(error);
        });
};