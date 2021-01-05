/**
 * U is a lightweight DOM helper.
 *
 * Created by Dario on 04/03/2017.
 */
var U = (function (win) {
    "use strict";
    var _logLevel = 0; // 0 = none, 1 = error, 2 = warn, 3 = info, 4 = debug
    var _doc = win.document;

    /**
     * window.requestAnimFrame Shiver
     */
    win.requestAnimFrame = (function () {
        return win.requestAnimationFrame ||
            win.webkitRequestAnimationFrame ||
            win.mozRequestAnimationFrame ||
            win.oRequestAnimationFrame ||
            win.msRequestAnimationFrame ||
            function (callback) {
                win.setTimeout(callback, 1000 / 60);
            };
    })();

    return {
        /**
         * Document.querySelector helper
         * @param {string} selector
         * @returns {Element}
         */
        query: function query(selector) {
            if(selector.charAt(0) === "#") {
                return _doc.getElementById(selector.substr(1));
            }
            return _doc.querySelector(selector);
        },
        /**
         * Adds a new element of type 'tagName' with attributes 'options' to the target (or the body).
         * It's a wrapper for createElement and appendChild.
         * @see https://developers.google.com/web/fundamentals/getting-started/primers/customelements#extendhtml
         *
         * @param {string} tagName - A valid HTML5 tag element name
         * @param {Object} options - The createElement options
         * @param {Element} target
         * @return {Element} the new Html element
         */
        add: function add(tagName, options, target) {
            var e = _doc.createElement(tagName);

            for (var key in options) {
                // skip loop if the property is from prototype
                if (!options.hasOwnProperty(key)) continue;
                e.setAttribute(key, options[key]);
            }

            if (target) {
                target.appendChild(e);
            } else {
                _doc.body.appendChild(e);
            }
            return e;
        },
        /**
         * Logger library
         */
        setLogLevel: function setLogLevel(level) {
            _logLevel = level;
        },
        log: function log() {
            var level = arguments[0];
            var args = Array.prototype.slice.call(arguments, 1);
            if (level <= _logLevel) {
                switch (level) {
                    case 1:
                        console.error.apply(this, args);
                        break;
                    case 2:
                        console.warn.apply(this, args);
                        break;
                    case 3:
                        console.info.apply(this, args);
                        break;
                    default:
                        console.log.apply(this, args);
                }
            }
        },
        LOG_LEVELS: {
            NONE: 0,
            ERROR: 1,
            WARN: 2,
            INFO: 3,
            DEBUG: 4
        },
        resizeCanvas: function resizeCanvas(canvas) {
            // Lookup the size the browser is displaying the canvas.
            var displayWidth = canvas.clientWidth;
            var displayHeight = canvas.clientHeight;

            // Check if the canvas is not the same size.
            if (canvas.width != displayWidth ||
                canvas.height != displayHeight) {

                // Make the canvas the same size
                canvas.width = displayWidth;
                canvas.height = displayHeight;
            }
        },
    }

})(window);

