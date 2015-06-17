var InputManager = function InputManager(game) {
    this.LOG_TAG = "InputManager:";
    EventEmitter.call(this);
    this.keyStatus = {};
    this.mouseButtonStatus = [];
    this.isDragging = false;
    this.mouseWheelDir = 0;
    // Represents mouse position inside the viewport (clientX/clientY)
    this.mousePos = {
        x: undefined,
        y: undefined,
        screenX: undefined,
        screenY: undefined,
        scrollX:0,
        scrollY: 0,
        deltaX:0,
        deltaY: 0,
        scrollDirX:undefined,
        scrollDirY:undefined
    };

    //this is the list of the managed events
    this.managedEvents = [
        'keyUp','keyDown','mouseUp','mouseDown','mouseMove','mouseWheel','contextMenu'
    ];

    // Attach listeners for game and ambient
    document.addEventListener('keyup', this.keyUp.bind(this), false);
    document.addEventListener('keydown', this.keyDown.bind(this), false);
    game.canvas.addEventListener('mousedown', this.mouseDown.bind(this), false);
    game.canvas.addEventListener("mouseup", this.mouseUp.bind(this), false);
    game.canvas.addEventListener("mousemove", this.mouseMove.bind(this), false);
    game.canvas.addEventListener("wheel", this.mouseWheel.bind(this), false);
    game.canvas.addEventListener("contextmenu",this.contextMenu.bind(this), true);
};

utils.inherit(InputManager, EventEmitter);

//<editor-fold desc="# IO Handlers">
InputManager.prototype.keyUp = function (event) {
    event.preventDefault();
    this.keyStatus[event.keyCode] = false;
    //console.log(this.LOG_TAG+" keyUp", event.keyCode, this.keyStatus);
    this.emit("keyup", this.getStatus());
};
InputManager.prototype.keyDown = function (event) {
    event.preventDefault();
    this.keyStatus[event.keyCode] = true;
    //console.log(this.LOG_TAG+" keyDown", event.keyCode, this.keyStatus);
    this.emit("keydown", this.getStatus());
};
InputManager.prototype.mouseUp = function (event) {
    event.preventDefault();
    this.mouseButtonStatus[event.button] = false;
    this.isDragging = false;
    this.mousePos.scrollX = 0;
    this.mousePos.scrollY= 0;
    this.registerMousePosition(event);
    //console.log(this.LOG_TAG+" mouseUp", event.button, this.mouseButtonStatus, this.isDragging, this.mousePos);
    this.emit("mouseup", this.getStatus());
};
InputManager.prototype.mouseDown = function (event) {
    event.preventDefault();
    this.mouseButtonStatus[event.button] = true;
    this.isDragging = true;
    this.registerMousePosition(event);
    //fix starting point!
    this.mousePos.scrollX = this.mousePos.x;
    this.mousePos.scrollY = this.mousePos.y;
    //console.log(this.LOG_TAG+" mouseDown", event.button, this.mouseButtonStatus, this.isDragging, this.mousePos);
    this.emit("mousedown", this.getStatus());
};
InputManager.prototype.mouseWheel = function (evt) {
    if (!evt) evt = event;
    var direction = (evt.detail < 0 || evt.wheelDelta > 0) ? 1 : -1;
    this.isDragging = false;
    this.mousePos.scrollX = 0;
    this.mousePos.scrollY= 0;
    // Use the value as you will
    this.mouseWheelDir += direction;
    this.emit("wheel", this.getStatus());
};
//TODO: provide more X/Y values (page, client...)
InputManager.prototype.mouseMove = function (event) {
    event.preventDefault();
    this.registerMousePosition(event);
    //check to make sure there is data to compare against
    if (this.isDragging) {
        //get the change from last position to this position
        var deltaX = this.mousePos.x - this.mousePos.scrollX,
            deltaY = this.mousePos.scrollY - this.mousePos.y;
        this.mousePos.deltaX = deltaX;
        this.mousePos.deltaY = deltaY;
        //check which direction had the highest amplitude and then figure out direction by checking if the value is greater or less than zero
        if (deltaX < 0) {
            //left
            this.mousePos.scrollDirX = "left";
        } else if (deltaX > 0) {
            //right
            this.mousePos.scrollDirX = "right";
        }
        if (deltaY > 0) {
            //up
            this.mousePos.scrollDirY = "up";
        } else if (deltaY < 0) {
            //down
            this.mousePos.scrollDirY = "down";
        }
    }
    //console.log(this.LOG_TAG+" mouseMove", event.button, this.mouseButtonStatus, this.isDragging, this.mousePos);
    this.emit("mousemove", this.getStatus());
};
InputManager.prototype.contextMenu = function (event) {
    event.preventDefault();
    console.log("Where's my context?", event);
    this.emit("contextmenu", this.getStatus());
};
//</editor-fold>
//<editor-fold desc="# Tools and enums">
// This function calculates the correct position of the mouse inside the viewport.
// Obviously this is a hell of a cross-browser fucking shit, but it works fine.
InputManager.prototype.registerMousePosition = function (event) {
    var viewportX, viewportY;
    this.mousePos.screenX = event.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
    this.mousePos.screenY = event.clientY + document.body.scrollTop + document.documentElement.scrollTop;
    var obj = event.currentTarget;
    viewportX = 0;
    viewportY = 0;
    viewportX += obj.offsetLeft;
    viewportY += obj.offsetTop;
    while (obj = obj.offsetParent) {
        viewportX += obj.offsetLeft;
        viewportY += obj.offsetTop;
    }
    this.mousePos.x = this.mousePos.screenX - viewportX;
    this.mousePos.y = this.mousePos.screenY - viewportY;
};
InputManager.prototype.drawInputInfos = function drawInputInfos(ctx) {
    var infos = [
        "Mouse input:",
        "x: " + this.mousePos.x + " y: " + this.mousePos.y,
        "sX: " + this.mousePos.screenX + " sY: " + this.mousePos.screenY,
        "Button pressed: " + this.mouseButtonStatus.join(","),
        "Dragging: " + this.isDragging,
        "Wheel: " + this.mouseWheelDir,
        "Keyboard status",
    ], i;
    ctx.fillStyle = "rgba(0,0,0,0.5)";
    ctx.fillRect(0, 0, 180, 10 * (infos.length + 2));
    ctx.fillStyle = "#ffffff";
    ctx.textAlign = 'left';

    for (i = 0; i < infos.length; i++) {
        ctx.fillText(infos[i], 10, 10 + 10 * (i + 1));
    }

    //draw keyboard active inputs
    var k, keyPressed = [];
    var self = this;
    for (k in self.keyStatus) {
        if (self.keyStatus.hasOwnProperty(k) && self.keyStatus[k]) {
            keyPressed.push(self.keyEventToString(k));
        }
    }
    for (i = 0; i < keyPressed.length; i++) {
        ctx.fillStyle = "#ffffff";
        ctx.fillText(keyPressed[i], 10, 10 * (infos.length + 1) + 10 * (i+1));
        ctx.fillStyle = "rgba(0,0,0,0.5)";
        ctx.fillRect(0, 10 * (infos.length + 1) + 10 * (i+1), 180, 10);
    }

};
InputManager.prototype.getStatus = function getStatus() {
    var status = {
        keyboard: this.keyStatus,
        mouse: {
            mouseButtonStatus: this.mouseButtonStatus,
            isDragging: this.isDragging,
            mouseWheelDir: this.mouseWheelDir,
            mousePos: this.mousePos
        }
    }
    return status;
};

InputManager.prototype.mouseEventEnum = {
    MOUSE_BUTTON_LEFT: 0,
    MOUSE_BUTTON_MIDDLE: 1,
    MOUSE_BUTTON_RIGHT: 2
};
InputManager.prototype.keyEventEnum = {
    DOM_VK_CANCEL: 3,
    DOM_VK_HELP: 6,
    DOM_VK_BACK_SPACE: 8,
    DOM_VK_TAB: 9,
    DOM_VK_CLEAR: 12,
    DOM_VK_RETURN: 13,
    DOM_VK_ENTER: 14,
    DOM_VK_SHIFT: 16,
    DOM_VK_CONTROL: 17,
    DOM_VK_ALT: 18,
    DOM_VK_PAUSE: 19,
    DOM_VK_CAPS_LOCK: 20,
    DOM_VK_ESCAPE: 27,
    DOM_VK_SPACE: 32,
    DOM_VK_PAGE_UP: 33,
    DOM_VK_PAGE_DOWN: 34,
    DOM_VK_END: 35,
    DOM_VK_HOME: 36,
    DOM_VK_LEFT: 37,
    DOM_VK_UP: 38,
    DOM_VK_RIGHT: 39,
    DOM_VK_DOWN: 40,
    DOM_VK_PRINTSCREEN: 44,
    DOM_VK_INSERT: 45,
    DOM_VK_DELETE: 46,
    DOM_VK_0: 48,
    DOM_VK_1: 49,
    DOM_VK_2: 50,
    DOM_VK_3: 51,
    DOM_VK_4: 52,
    DOM_VK_5: 53,
    DOM_VK_6: 54,
    DOM_VK_7: 55,
    DOM_VK_8: 56,
    DOM_VK_9: 57,
    DOM_VK_SEMICOLON: 59,
    DOM_VK_EQUALS: 61,
    DOM_VK_A: 65,
    DOM_VK_B: 66,
    DOM_VK_C: 67,
    DOM_VK_D: 68,
    DOM_VK_E: 69,
    DOM_VK_F: 70,
    DOM_VK_G: 71,
    DOM_VK_H: 72,
    DOM_VK_I: 73,
    DOM_VK_J: 74,
    DOM_VK_K: 75,
    DOM_VK_L: 76,
    DOM_VK_M: 77,
    DOM_VK_N: 78,
    DOM_VK_O: 79,
    DOM_VK_P: 80,
    DOM_VK_Q: 81,
    DOM_VK_R: 82,
    DOM_VK_S: 83,
    DOM_VK_T: 84,
    DOM_VK_U: 85,
    DOM_VK_V: 86,
    DOM_VK_W: 87,
    DOM_VK_X: 88,
    DOM_VK_Y: 89,
    DOM_VK_Z: 90,
    DOM_VK_CONTEXT_MENU: 93,
    DOM_VK_NUMPAD0: 96,
    DOM_VK_NUMPAD1: 97,
    DOM_VK_NUMPAD2: 98,
    DOM_VK_NUMPAD3: 99,
    DOM_VK_NUMPAD4: 100,
    DOM_VK_NUMPAD5: 101,
    DOM_VK_NUMPAD6: 102,
    DOM_VK_NUMPAD7: 103,
    DOM_VK_NUMPAD8: 104,
    DOM_VK_NUMPAD9: 105,
    DOM_VK_MULTIPLY: 106,
    DOM_VK_ADD: 107,
    DOM_VK_SEPARATOR: 108,
    DOM_VK_SUBTRACT: 109,
    DOM_VK_DECIMAL: 110,
    DOM_VK_DIVIDE: 111,
    DOM_VK_F1: 112,
    DOM_VK_F2: 113,
    DOM_VK_F3: 114,
    DOM_VK_F4: 115,
    DOM_VK_F5: 116,
    DOM_VK_F6: 117,
    DOM_VK_F7: 118,
    DOM_VK_F8: 119,
    DOM_VK_F9: 120,
    DOM_VK_F10: 121,
    DOM_VK_F11: 122,
    DOM_VK_F12: 123,
    DOM_VK_F13: 124,
    DOM_VK_F14: 125,
    DOM_VK_F15: 126,
    DOM_VK_F16: 127,
    DOM_VK_F17: 128,
    DOM_VK_F18: 129,
    DOM_VK_F19: 130,
    DOM_VK_F20: 131,
    DOM_VK_F21: 132,
    DOM_VK_F22: 133,
    DOM_VK_F23: 134,
    DOM_VK_F24: 135,
    DOM_VK_NUM_LOCK: 144,
    DOM_VK_SCROLL_LOCK: 145,
    DOM_VK_COMMA: 188,
    DOM_VK_PERIOD: 190,
    DOM_VK_SLASH: 191,
    DOM_VK_BACK_QUOTE: 192,
    DOM_VK_OPEN_BRACKET: 219,
    DOM_VK_BACK_SLASH: 220,
    DOM_VK_CLOSE_BRACKET: 221,
    DOM_VK_QUOTE: 222,
    DOM_VK_META: 224
};
InputManager.prototype.keyEventToString = function keyEventToString(keyEvent) {
    var keyStrings = {
        3: "CANCEL",
        6: "HELP",
        8: "BACK_SPACE",
        9: "TAB",
        12: "CLEAR",
        13: "RETURN",
        14: "ENTER",
        16: "SHIFT",
        17: "CONTROL",
        18: "ALT",
        19: "PAUSE",
        20: "CAPS_LOCK",
        27: "ESCAPE",
        32: "SPACE",
        33: "PAGE_UP",
        34: "PAGE_DOWN",
        35: "END",
        36: "HOME",
        37: "LEFT",
        38: "UP",
        39: "RIGHT",
        40: "DOWN",
        44: "PRINTSCREEN",
        45: "INSERT",
        46: "DELETE",
        48: "0",
        49: "1",
        50: "2",
        51: "3",
        52: "4",
        53: "5",
        54: "6",
        55: "7",
        56: "8",
        57: "9",
        59: "SEMICOLON",
        61: "EQUALS",
        65: "A",
        66: "B",
        67: "C",
        68: "D",
        69: "E",
        70: "F",
        71: "G",
        72: "H",
        73: "I",
        74: "J",
        75: "K",
        76: "L",
        77: "M",
        78: "N",
        79: "O",
        80: "P",
        81: "Q",
        82: "R",
        83: "S",
        84: "T",
        85: "U",
        86: "V",
        87: "W",
        88: "X",
        89: "Y",
        90: "Z",
        93: "CONTEXT_MENU",
        96: "NUMPAD0",
        97: "NUMPAD1",
        98: "NUMPAD2",
        99: "NUMPAD3",
        100: "NUMPAD4",
        101: "NUMPAD5",
        102: "NUMPAD6",
        103: "NUMPAD7",
        104: "NUMPAD8",
        105: "NUMPAD9",
        106: "MULTIPLY",
        107: "ADD",
        108: "SEPARATOR",
        109: "SUBTRACT",
        110: "DECIMAL",
        111: "DIVIDE",
        112: "F1",
        113: "F2",
        114: "F3",
        115: "F4",
        116: "F5",
        117: "F6",
        118: "F7",
        119: "F8",
        120: "F9",
        121: "F10",
        122: "F11",
        123: "F12",
        124: "F13",
        125: "F14",
        126: "F15",
        127: "F16",
        128: "F17",
        129: "F18",
        130: "F19",
        131: "F20",
        132: "F21",
        133: "F22",
        134: "F23",
        135: "F24",
        144: "NUM_LOCK",
        145: "SCROLL_LOCK",
        188: "COMMA",
        190: "PERIOD",
        191: "SLASH",
        192: "BACK_QUOTE",
        219: "OPEN_BRACKET",
        220: "BACK_SLASH",
        221: "CLOSE_BRACKET",
        222: "QUOTE",
        224: "META"
    };
    return keyStrings[keyEvent] || "undefined";
};
//</editor-fold>

