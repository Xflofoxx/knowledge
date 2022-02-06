'use strict';

/**
 * Setup the requestAnimationFrame polyfill
 */
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

window.onload = async function() {
    try {
        game = new window.GameEngine({
            ids: {
                main: "mainCanvas",
                back: "backCanvas",
                hud: "hud"
            },
            debug: true
        });

        await game.init()
        await game.start();
    } catch (error) {
        console.error(error);
    }
};