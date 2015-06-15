'use strict';
// This class manages the UI elements
var ui = {
    styles: {
        panel: {
            title: {
                font: "bold 28px Verdana",
                color: "#333333",
                strokeWidth: 2,
                strokeColor: "rgb(107, 83, 57)",
                light: "#777777",
                align: "center",
                baseline: "middle"
            },
            subTitle: {
                font: "bold 14px Verdana",
                color: "#e0e0e0",
                strokeWidth: 1,
                strokeColor: "#777777",
                align: "center",
                baseline: "middle"
            },
            radius: 10,
            padding: 10,
            bgColor: "rgb(151, 113, 74)",
            strokeWidth: 5,
            strokeColor: "rgb(107, 83, 57)"
        },
        buttons: {
            color: "#f0f0f0",
            font: "normal 14px Verdana",
            margin: {
                right: 0,
                left: 5,
                top: 0,
                bottom: 0
            }
        }
    },
    /**
      * Draws a rounded rectangle using the current state of the canvas.
      * If you omit the last three params, it will draw a rectangle
      * outline with a 5 pixel border radius
      * @param {CanvasRenderingContext2D} ctx
      * @param {Number} x The top left x coordinate
      * @param {Number} y The top left y coordinate
      * @param {Number} width The width of the rectangle
      * @param {Number} height The height of the rectangle
      * @param {Number} radius The corner radius. Defaults to 5;
      * @param {Boolean} fill Whether to fill the rectangle. Defaults to false.
      * @param {Boolean} stroke Whether to stroke the rectangle. Defaults to true.
      */
    roundRect: function roundRect(ctx, x, y, width, height, radius, fill, stroke) {
        if (typeof stroke == "undefined") {
            stroke = true;
        }
        if (typeof radius === "undefined") {
            radius = 5;
        }
        ctx.beginPath();
        ctx.moveTo(x + radius, y);
        ctx.lineTo(x + width - radius, y);
        ctx.quadraticCurveTo(x + width, y, x + width, y + radius);
        ctx.lineTo(x + width, y + height - radius);
        ctx.quadraticCurveTo(x + width, y + height, x + width - radius, y + height);
        ctx.lineTo(x + radius, y + height);
        ctx.quadraticCurveTo(x, y + height, x, y + height - radius);
        ctx.lineTo(x, y + radius);
        ctx.quadraticCurveTo(x, y, x + radius, y);
        ctx.closePath();
        if (stroke) {
            ctx.stroke();
        }
        if (fill) {
            ctx.fill();
        }
    },
    drawPanel: function drawPanel(ctx, panelObj, fill, stroke) {
        var subTitleDelta = 28; // title font size
        if (fill) {
            ctx.fillStyle = this.styles.panel.bgColor;
        }
        if (stroke) {
            ctx.lineWidth = this.styles.panel.strokeWidth;
            ctx.strokeStyle = this.styles.panel.strokeColor;
        }
        this.roundRect(ctx, panelObj.x, panelObj.y, panelObj.w, panelObj.h, this.styles.panel.radius, fill, stroke);
        if (panelObj.title) {
            ctx.fillStyle = this.styles.panel.title.color;
            ctx.font = this.styles.panel.title.font;
            ctx.textAlign = this.styles.panel.title.align;
            ctx.textBaseline = this.styles.panel.title.baseline;
            ctx.lineWidth = this.styles.panel.title.strokeWidth;
            ctx.strokeStyle = this.styles.panel.title.strokeColor;
            ctx.strokeText(panelObj.title,
                panelObj.x + 1 + this.styles.panel.padding + (panelObj.w - this.styles.panel.padding * 2) / 2,
                panelObj.y + 1 + this.styles.panel.padding * 2,
                panelObj.w - this.styles.panel.padding * 2);
            ctx.strokeStyle = this.styles.panel.title.light;
            ctx.strokeText(panelObj.title,
                panelObj.x - 1 + this.styles.panel.padding + (panelObj.w - this.styles.panel.padding * 2) / 2,
                panelObj.y - 1 + this.styles.panel.padding * 2,
                panelObj.w - this.styles.panel.padding * 2);
            ctx.fillText(panelObj.title,
                panelObj.x + this.styles.panel.padding + (panelObj.w - this.styles.panel.padding * 2) / 2,
                panelObj.y + this.styles.panel.padding * 2,
                panelObj.w - this.styles.panel.padding * 2);

        }
        if (panelObj.subTitle) {
            ctx.fillStyle = this.styles.panel.subTitle.color;
            ctx.font = this.styles.panel.subTitle.font;
            ctx.textAlign = this.styles.panel.subTitle.align;
            ctx.textBaseline = this.styles.panel.subTitle.baseline;
            ctx.lineWidth = this.styles.panel.subTitle.strokeWidth;
            ctx.strokeStyle = this.styles.panel.subTitle.strokeColor;
            ctx.strokeText(panelObj.subTitle,
                panelObj.x + 1 + this.styles.panel.padding + (panelObj.w - this.styles.panel.padding * 2) / 2,
                panelObj.y + 1 + subTitleDelta + this.styles.panel.padding * 2,
                panelObj.w - this.styles.panel.padding * 2);
            ctx.fillText(panelObj.subTitle,
                panelObj.x + this.styles.panel.padding + (panelObj.w - this.styles.panel.padding * 2) / 2,
                panelObj.y + subTitleDelta + this.styles.panel.padding * 2,
                panelObj.w - this.styles.panel.padding * 2);
        }
    },
    drawButton: function drawButton(ctx, btnObj){
        ctx.fillStyle = this.styles.buttons.color;
        ctx.font = this.styles.buttons.font;
        if (btnObj.disabled) {
            ctx.save();
            ctx.globalAlpha = 0.4;
            ctx.drawImage(btnObj.disabledAsset,
                btnObj.size.x, btnObj.size.y, btnObj.size.w, btnObj.size.h);
            ctx.fillText(btnObj.title,
                btnObj.size.x + btnObj.size.w / 2,
                btnObj.size.y + btnObj.size.h / 2 - 2,
                btnObj.size.w);
            ctx.restore();
        }
        else if (btnObj.pressed) {
            ctx.drawImage(btnObj.pressedAsset,
                btnObj.size.x, btnObj.size.y, btnObj.size.w, btnObj.size.h);
            ctx.fillText(btnObj.title,
                btnObj.size.x + btnObj.size.w / 2,
                btnObj.size.y + btnObj.size.h / 2 - 2,
                btnObj.size.w);
        }
        else if (btnObj.hover) {
            ctx.drawImage(btnObj.hoverAsset,
                btnObj.size.x, btnObj.size.y, btnObj.size.w, btnObj.size.h);
            ctx.fillText(btnObj.title,
                btnObj.size.x + btnObj.size.w / 2,
                btnObj.size.y + btnObj.size.h / 2 - 2,
                btnObj.size.w);
        }
        else {
            ctx.save();
            ctx.globalAlpha = 0.8;
            ctx.drawImage(btnObj.defaultAsset,
                btnObj.size.x, btnObj.size.y, btnObj.size.w, btnObj.size.h);
            ctx.fillText(btnObj.title,
                btnObj.size.x + btnObj.size.w / 2,
                btnObj.size.y + btnObj.size.h / 2 - 2,
                btnObj.size.w);
            ctx.restore();
        }
    }
};