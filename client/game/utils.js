'use strict';

const Utils = {
    Math: {
        /**
         * Accurate round 
         * @param {number} number - The number to round 
         * @param {integer} decimalPlaces - The number of decimal places 
         * @returns 
         */
        roundAccuratly: (number, decimalPlaces) => Number(Math.round(number + "e" + decimalPlaces) + "e-" + decimalPlaces),
        /**
         * Generates random number between min and max
         * @param {number} min 
         * @param {number} max 
         * @return {number} 
         */
        getRandomNumber: (min, max) => {
            return Math.random() * (max - min) + min;
        },
        /**
         * Generates random integer number between min and max
         * @param {number} min 
         * @param {number} max 
         * @return {number} 
         */
        getRandomInt(minInt, maxInt) {
            return Math.floor(Math.random() * (maxInt - minInt + 1)) + minInt;
        },
        /**
         * Generates random boolean value
         * @return {boolean} 
         */
        getRandomBoolean() {
            return !!Utils.Math.getRandomInt(0, 1);
        }
    },
    Effects: {
        /**
         * Easing s
         * Each  accepts arguments for the parameters t, b, c and d. The following applies to the parameters of all s:
         * So in short b, c and d are mostly used as static settings for the easing and t needs to get updated frequently to make the animation progress.
         * The equations for these s come from Robert Panner.
         * @see https://spicyyoghurt.com/tools/easing-s
         */
        easing: {
            /*
             * Linear motion
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeLinear: (t, b, c, d) => {
                return c * t / d + b;
            },
            /**
             * Quadratic easing in
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInQuad: (t, b, c, d) => {
                return c * (t /= d) * t + b;
            },
            /**
             * Quadratic easing out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeOutQuad: (t, b, c, d) => {
                return -c * (t /= d) * (t - 2) + b;
            },
            /**
             * Quadratic easing in and out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInOutQuad: (t, b, c, d) => {
                if ((t /= d / 2) < 1) return c / 2 * t * t + b;
                return -c / 2 * ((--t) * (t - 2) - 1) + b;
            },
            /**
             * Sinusoidal easing in
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInSine: (t, b, c, d) => {
                return -c * Math.cos(t / d * (Math.PI / 2)) + c + b;
            },
            /**
             * Sinusoidal easing out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeOutSine: (t, b, c, d) => {
                return c * Math.sin(t / d * (Math.PI / 2)) + b;
            },
            /**
             * Sinusoidal easing in and out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInOutSine: (t, b, c, d) => {
                return -c / 2 * (Math.cos(Math.PI * t / d) - 1) + b;
            },
            /**
             * Exponential easing in
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInExpo: (t, b, c, d) => {
                return (t == 0) ? b : c * Math.pow(2, 10 * (t / d - 1)) + b;
            },
            /**
             * Exponential easing out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeOutExpo: (t, b, c, d) => {
                return (t == d) ? b + c : c * (-Math.pow(2, -10 * t / d) + 1) + b;
            },
            /**
             * Exponential easing in and out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInOutExpo: (t, b, c, d) => {
                if (t == 0) return b;
                if (t == d) return b + c;
                if ((t /= d / 2) < 1) return c / 2 * Math.pow(2, 10 * (t - 1)) + b;
                return c / 2 * (-Math.pow(2, -10 * --t) + 2) + b;
            },
            /**
             * Circular easing in
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInCirc: (t, b, c, d) => {
                return -c * (Math.sqrt(1 - (t /= d) * t) - 1) + b;
            },
            /**
             * Circular easing out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeOutCirc: (t, b, c, d) => {
                return c * Math.sqrt(1 - (t = t / d - 1) * t) + b;
            },
            /**
             * Circular easing in and out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInOutCirc: (t, b, c, d) => {
                if ((t /= d / 2) < 1) return -c / 2 * (Math.sqrt(1 - t * t) - 1) + b;
                return c / 2 * (Math.sqrt(1 - (t -= 2) * t) + 1) + b;
            },
            /**
             * Cubic easing in
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInCubic: (t, b, c, d) => {
                return c * (t /= d) * t * t + b;
            },
            /**
             * Cubic easing out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeOutCubic: (t, b, c, d) => {
                return c * ((t = t / d - 1) * t * t + 1) + b;
            },
            /**
             * Cubic easing in and out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInOutCubic: (t, b, c, d) => {
                if ((t /= d / 2) < 1) return c / 2 * t * t * t + b;
                return c / 2 * ((t -= 2) * t * t + 2) + b;
            },
            /**
             * Quartic easing in
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInQuart: (t, b, c, d) => {
                return c * (t /= d) * t * t * t + b;
            },
            /**
             * Quartic easing out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeOutQuart: (t, b, c, d) => {
                return -c * ((t = t / d - 1) * t * t * t - 1) + b;
            },

            /**
             * Quartic easing in and out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInOutQuart: (t, b, c, d) => {
                if ((t /= d / 2) < 1) return c / 2 * t * t * t * t + b;
                return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
            },

            /**
             * Quintic easing in
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInQuint: (t, b, c, d) => {
                return c * (t /= d) * t * t * t * t + b;
            },

            /**
             * Quintic easing out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeOutQuint: (t, b, c, d) => {
                return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
            },

            /**
             * Quintic easing in and out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInOutQuint: (t, b, c, d) => {
                if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b;
                return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
            },

            /**
             * Elastic easing in
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInElastic: (t, b, c, d) => {
                var s = 1.70158;
                var p = 0;
                var a = c;
                if (t == 0) return b;
                if ((t /= d) == 1) return b + c;
                if (!p) p = d * .3;
                if (a < Math.abs(c)) {
                    a = c;
                    var s = p / 4;
                } else var s = p / (2 * Math.PI) * Math.asin(c / a);
                return -(a * Math.pow(2, 10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p)) + b;
            },

            /**
             * Elastic easing out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeOutElastic: (t, b, c, d) => {
                var s = 1.70158;
                var p = 0;
                var a = c;
                if (t == 0) return b;
                if ((t /= d) == 1) return b + c;
                if (!p) p = d * .3;
                if (a < Math.abs(c)) {
                    a = c;
                    var s = p / 4;
                } else var s = p / (2 * Math.PI) * Math.asin(c / a);
                return a * Math.pow(2, -10 * t) * Math.sin((t * d - s) * (2 * Math.PI) / p) + c + b;
            },

            /**
             * Elastic easing in and out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInOutElastic: (t, b, c, d) => {
                var s = 1.70158;
                var p = 0;
                var a = c;
                if (t == 0) return b;
                if ((t /= d / 2) == 2) return b + c;
                if (!p) p = d * (.3 * 1.5);
                if (a < Math.abs(c)) {
                    a = c;
                    var s = p / 4;
                } else var s = p / (2 * Math.PI) * Math.asin(c / a);
                if (t < 1) return -.5 * (a * Math.pow(2, 10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p)) + b;
                return a * Math.pow(2, -10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p) * .5 + c + b;
            },

            /**
             * Back easing in
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInBack: (t, b, c, d) => {
                if (s == undefined) s = 1.70158;
                return c * (t /= d) * t * ((s + 1) * t - s) + b;
            },

            /**
             * Back easing out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeOutBack: (t, b, c, d) => {
                if (s == undefined) s = 1.70158;
                return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
            },

            /**
             * Back easing in and out
             * @param {number} t - Time - Amount of time that has passed since the beginning of the animation. 
             *                     Usually starts at 0 and is slowly increased using a game loop or other update .
             * @param {number} b - Beginning value - The starting point of the animation. Usually it's a static value, you can start at 0 for example 
             * @param {number} c - Change in value - The amount of change needed to go from starting point to end point. It's also usually a static value.
             * @param {number} d - Duration - Amount of time the animation will take. Usually a static value aswell
             * @return {number}
             */
            easeInOutBack: (t, b, c, d) => {
                if (s == undefined) s = 1.70158;
                if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
                return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;
            }

        }
    }
}