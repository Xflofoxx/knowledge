'use strict';

class HUDObject extends GameObject {
    constructor(context, x, y, vx, vy, options) {
        super(context, x, y, vx, vy);

        this.isSelected = false;

        // Set the hud element
        const { opacity, bgColor, height, width, border, dropSwadow, fontColor } = options;

        // Set default width and height
        this.width = width || 50;
        this.height = height || 50;
        this.opacity = opacity || 100;
        this.bgColor = bgColor || "#ffffff";
        this.fontColor = fontColor || "#000000";
        this.borderSize = border || 2;
        this.dropSwadow = dropSwadow !== false;
        this.bgDarker = Utils.Effects.colors.increase_brightness(this.bgColor, -25);
        this.bgLighter = Utils.Effects.colors.increase_brightness(this.bgColor, +35);
    }

    /**
     * @param {InputManager} inputManager 
     * @param {number} secondsPassed 
     */
    update(inputManager, secondsPassed) {

        if (inputManager.mouseX > this.x && inputManager.mouseX < this.x + this.width && inputManager.mouseY > this.y && inputManager.mouseY < this.y + this.height) {
            this.hasMouseOver = true;
        } else {
            this.hasMouseOver = false;
        }
    }

    onClick(button) {
        if (button === 0) {
            this.isSelected = !this.isSelected;
            console.log("Clicked " + button + " on " + this.constructor.name);
        }
    }

}

class HWindow extends HUDObject {
    constructor(context, x, y, vx, vy, options) {
        super(context, x, y, vx, vy, options);

        this.title = options.title || "New HWindow";

        // default line height not based on font size!
        this.lineHeight = FONT_SIZES[MEDIA].Medium;

        this.header = {
            hasMouseOver: false,
            h: 20
        }
        this.body = {
            hcenter: this.x + this.width / 2,
            y: this.y + this.header.h,
            h: this.height - this.header.h
        }
    }

    _drawBorder(hasShadow) {
        this.context.save();
        this.context.lineJoin = 'round';
        this.context.lineWidth = this.header.hasMouseOver ? 3 * this.borderSize : this.borderSize;
        this.context.strokeStyle = this.isSelected ? this.bgLighter : this.bgDarker;
        if (hasShadow) {
            this.context.filter = `drop-shadow(${this.borderSize / 2}px ${this.borderSize / 2}px 1px black)`;
        }
        this.context.strokeRect(this.x, this.y, this.width, this.height);
        this.context.restore();
    }

    _drawBody() {
        this.context.save();
        this.context.fillStyle = this.hasMouseOver || this.isSelected ? this.bgLighter : this.bgColor;
        this.context.fillRect(this.x, this.body.y, this.width, this.body.h);
        this.context.font = FONT_SIZES[MEDIA].Small + "px " + FONT_FAMILY;
        this.context.textAlign = "center";
        this.context.fillStyle = this.fontColor;
        this.context.fillText(this.bgColor, this.body.hcenter, this.body.y + this.lineHeight);
        this.context.fillText(this.bgDarker, this.body.hcenter, this.body.y + 2 * this.lineHeight);
        this.context.fillText(this.bgLighter, this.body.hcenter, this.body.y + 3 * this.lineHeight);
        this.context.fillText("selected: " + this.isSelected, this.body.hcenter, this.body.y + 4 * this.lineHeight);
        this.context.fillText("colliding: " + this.isColliding, this.body.hcenter, this.body.y + 5 * this.lineHeight);
        this.context.fillText("header.hasMouseOver: " + this.header.hasMouseOver, this.body.hcenter, this.body.y + 6 * this.lineHeight);
        this.context.restore();
    }

    _drawHeader() {
        this.context.save();

        this.context.fillStyle = this.bgDarker;
        this.context.fillRect(this.x, this.y, this.width, this.header.h);
        this.context.textAlign = "left";
        this.context.textBaseline = 'middle';
        this.context.fillStyle = this.fontColor;
        this.context.font = FONT_SIZES[MEDIA].Medium + "px " + FONT_AWESOME.LIGHT;
        this.context.fillText("\uf40e", this.x + 2, this.y + this.header.h / 2);
        this.context.textAlign = "right";
        this.context.fillText("\uf410", this.x + this.width - 2, this.y + this.header.h / 2 - 1);
        this.context.textAlign = "left";
        this.context.font = FONT_SIZES[MEDIA].Medium + "px " + FONT_FAMILY;
        this.context.fillText(this.title, this.x + 24, this.y + this.header.h / 2);
        this.context.restore();
    }

    draw() {
        const quartedH = this.height / 4;
        // Draw a simple square
        this._drawBorder(this.dropSwadow);
        this._drawBody();
        this._drawHeader();
    }

    /**
     * @param {InputManager} inputManager 
     * @param {number} secondsPassed 
     */
    update(inputManager, secondsPassed) {
        // call base Object calculation
        super.update(inputManager, secondsPassed);

        if (inputManager.mouseX > this.x && inputManager.mouseX < this.x + this.width && inputManager.mouseY > this.y && inputManager.mouseY < this.y + this.header.h) {
            this.header.hasMouseOver = true;
        } else {
            this.header.hasMouseOver = false;
        }
    }

    /**
     * @param {InputManager} inputManager 
     * @param {number} secondsPassed 
     */
    lateUpdate(inputManager, secondsPassed) {}
}