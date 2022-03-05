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

// Disable right click menu on page
window.addEventListener(`contextmenu`, (e) => e.preventDefault());

window.onload = async function() {
    try {
        game = new window.GameEngine({
            ids: {
                main: "mainCanvas",
                back: "backCanvas"
            },
            debug: true
        });

        await game.init();
        game.start();
    } catch (error) {
        console.error(error);
    }
};