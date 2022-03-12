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

let MEDIA = "desktop";
const FONT_FAMILY = "Verdana";
const FONT_AWESOME = {
    LIGHT: "FAlight300",
    REGULAR: "FARegular400",
    BOLD: "FASolid900",
}
const FONT_SIZES = {
    phone: {
        XSmall: 6,
        Small: 8,
        Medium: 10,
        Large: 12,
        XLarge: 14
    },
    tablet: {
        XSmall: 10,
        Small: 12,
        Medium: 16,
        Large: 24,
        XLarge: 30
    },
    desktop: {
        XSmall: 10,
        Small: 13,
        Medium: 16,
        Large: 18,
        XLarge: 24
    },
}

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