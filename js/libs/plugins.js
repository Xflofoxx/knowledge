'use strict';
// http://paulirish.com/2011/requestanimationframe-for-smart-animating/
// http://my.opera.com/emoller/blog/2011/12/20/requestanimationframe-for-smart-er-animating
// requestAnimationFrame polyfill by Erik Möller. fixes from Paul Irish and Tino Zijdel
// MIT license
(function (window, rAF, cAF) {
    var lastTime = 0, vendors = ['ms', 'moz', 'webkit', 'o'], x;

    for (x = 0; x < vendors.length && !window[rAF]; ++x) {
        window[rAF] = window[vendors[x] + 'RequestAnimationFrame'];
        window[cAF] = window[vendors[x] + 'CancelAnimationFrame']
        || window[vendors[x] + 'CancelRequestAnimationFrame'];
    }

    if (!window[rAF]) {
        window[rAF] = function (callback) {
            var currTime = new Date().getTime(),
                timeToCall = Math.max(0, 16 - (currTime - lastTime)),
                id = window.setTimeout(function () {
                    callback(currTime + timeToCall);
                }, timeToCall);

            lastTime = currTime + timeToCall;

            return id;
        };
    }

    if (!window[cAF]) {
        window[cAF] = function (id) {
            window.clearTimeout(id);
        };
    }

}(this, 'requestAnimationFrame', 'cancelAnimationFrame'));

// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/filter
// Array.prototype.filter() polyfill
// "Any copyright is dedicated to the Public Domain. http://creativecommons.org/publicdomain/zero/1.0/"
if (!Array.prototype.filter) {
    Array.prototype.filter = function (fun/*, thisArg*/) {
        'use strict';

        if (this === void 0 || this === null) {
            throw new TypeError();
        }

        var t = Object(this);
        var len = t.length >>> 0;
        if (typeof fun !== 'function') {
            throw new TypeError();
        }

        var res = [];
        var thisArg = arguments.length >= 2 ? arguments[1] : void 0;
        for (var i = 0; i < len; i++) {
            if (i in t) {
                var val = t[i];

                // NOTE: Technically this should Object.defineProperty at
                //       the next index, as push can be affected by
                //       properties on Object.prototype and Array.prototype.
                //       But that method's new, and collisions should be
                //       rare, so use the more-compatible alternative.
                if (fun.call(thisArg, val, i, t)) {
                    res.push(val);
                }
            }
        }

        return res;
    };
}

var utils = {
    inherit: function inheritPrototype(childObject, parentObject) {
        // As discussed above, we use the Crockford’s method to copy the properties and methods from the parentObject onto the childObject​
        // So the copyOfParent object now has everything the parentObject has ​
        var copyOfParent = Object.create(parentObject.prototype);
        // Then we set the constructor of this new object to point to the childObject.​
        // Why do we manually set the copyOfParent constructor here, see the explanation immediately following this code block.​
        copyOfParent.constructor = childObject;
        // Then we set the childObject prototype to copyOfParent, so that the childObject can in turn inherit everything from copyOfParent (from parentObject)​
        childObject.prototype = copyOfParent;
    },
    makeid: function makeid(length) {
        var text = "";
        length = length + 0 || 5;
        var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        for (var i = 0; i < length; i++)
            text += possible.charAt(Math.floor(Math.random() * possible.length));

        return text;
    },
    getXHR : function getXHR(url, progress, next){
        var xhr = new XMLHttpRequest();
        xhr.onprogress = progress;
        xhr.open('GET', url, true);
        xhr.onload = next;
        xhr.send();
    },
    randomIntFromInterval : function randomIntFromInterval(min,max) {
        return Math.floor(Math.random()*(max-min+1)+min);
    }
};