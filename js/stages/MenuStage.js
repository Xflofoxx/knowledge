/**
 * Created by Dario on 12/06/2015.
 */

var MenuStage = function MenuStage(AM, IO) {
    Stage.call(this, "menu", {
        assets: [
            {
                id: "panel",
                path: window.location + '/../assets/uipack-rpg/PNG/',
                file: "panel_brown.png",
                mime: "image/png"
            },
            {
                id: "panelInset",
                path: window.location + '/../assets/uipack-rpg/PNG/',
                file: "panelInset_beige.png",
                mime: "image/png"
            },
            {
                id: "btn",
                path: window.location + '/../assets/uipack-rpg/PNG/',
                file: "buttonLong_beige.png",
                mime: "image/png"
            },
            {
                id: "btnDown",
                path: window.location + '/../assets/uipack-rpg/PNG/',
                file: "buttonLong_beige_pressed.png",
                mime: "image/png"
            },
        ],
        title: "Knowledge",
        subTitle: " a game on mankind",
        buttons: [
            {
                action: "new",
                title: "New game",
                hover: false,
                pressed: false,
                disabled: false
            },
            {
                action: "continue",
                title: "Continue",
                hover: false,
                pressed: false,
                disabled: true
            },
            {
                action: "load",
                title: "Load saved game",
                hover: false,
                pressed: false,
                disabled: false
            },
            {
                action: "settings",
                title: "Settings",
                hover: false,
                pressed: false,
                disabled: false
            }
        ]
    }, AM, IO);
    this.LOG_TAG = "MenuStage:";
};
utils.inherit(MenuStage, Stage);

MenuStage.prototype.render = function render(ctx, forceRedraw) {
    var panelSize = {
        w: 300,
        h: 100 + this.config.buttons.length * 40
    };
    var buttonSize = {
        w: panelSize.w - 60,
        h: 40
    };
    var btn;
    //clear background context
    ctx.fillStyle = "#000000";
    ctx.fillRect(0, 0, ctx.canvas.width, ctx.canvas.height);
    panelSize.x = (ctx.canvas.width - panelSize.w) / 2;
    panelSize.y = (ctx.canvas.height - panelSize.h) / 2;
    buttonSize.x = panelSize.x + 30;
    buttonSize.y = panelSize.y + 30;
    //paneltitle and subtitle
    panelSize.title = this.config.title;
    panelSize.subTitle = this.config.subTitle;
    ui.drawPanel(ctx, panelSize, true, true);
    for (var i = 0; i < this.config.buttons.length; i++) {
        btn = this.config.buttons[i];
        btn.disabledAsset = this.AM.bundle.menu.btn;
        btn.pressedAsset = this.AM.bundle.menu.btnDown;
        btn.hoverAsset = this.AM.bundle.menu.btn;
        btn.defaultAsset = this.AM.bundle.menu.btn;
        btn.size = {
            x: buttonSize.x,
            y: buttonSize.y + (buttonSize.h + 2) * (i + 1),
            w: buttonSize.w,
            h: buttonSize.h
        };
        ui.drawButton(ctx,btn);
        //if (btn.disabled) {
        //    ctx.save();
        //    ctx.globalAlpha = 0.4;
        //    ctx.drawImage(this.AM.bundle.menu.btn,
        //        btn.size.x, btn.size.y, btn.size.w, btn.size.h);
        //    ctx.fillText(btn.title,
        //        btn.size.x + btn.size.w / 2,
        //        btn.size.y + btn.size.h / 2 - 2,
        //        btn.size.w);
        //    ctx.restore();
        //}
        //else if (btn.pressed) {
        //    ctx.drawImage(this.AM.bundle.menu.btnDown,
        //        btn.size.x, btn.size.y, btn.size.w, btn.size.h);
        //    ctx.fillText(btn.title,
        //        btn.size.x + btn.size.w / 2,
        //        btn.size.y + btn.size.h / 2 - 2,
        //        btn.size.w);
        //}
        //else if (btn.hover) {
        //    ctx.drawImage(this.AM.bundle.menu.btn,
        //        btn.size.x, btn.size.y, btn.size.w, btn.size.h);
        //    ctx.fillText(btn.title,
        //        btn.size.x + btn.size.w / 2,
        //        btn.size.y + btn.size.h / 2 - 2,
        //        btn.size.w);
        //}
        //else {
        //    ctx.save();
        //    ctx.globalAlpha = 0.8;
        //    ctx.drawImage(this.AM.bundle.menu.btn,
        //        btn.size.x, btn.size.y, btn.size.w, btn.size.h);
        //    ctx.fillText(btn.title,
        //        btn.size.x + btn.size.w / 2,
        //        btn.size.y + btn.size.h / 2 - 2,
        //        btn.size.w);
        //    ctx.restore();
        //}
    }

    return ctx;
};
//MenuStage.prototype.manage = function manage(event, data, suppressEmit) {
//    var pEvent = event,
//        pData = data,
//        pSuppress = suppressEmit;
//    switch (event) {
//        case "btnpressed":
//            switch (data.action) {
//                case "setting":
//                    //this.drawSettings();
//                    break;
//            }
//            break;
//    }
//    //call the parent method that will emit the event
//    Stage.prototype.manage.call(this, pEvent, pData, pSuppress);
//};

//<editor-fold desc="# IO Handlers">
MenuStage.prototype.mouseDownHandler = function (status) {
    var self = this;
    var btn, b, hit;
    for (b = 0; b < self.config.buttons.length; b++) {
        btn = self.config.buttons[b];
        if (btn.size) {
            if (btn.size.x < status.mouse.mousePos.x
                && btn.size.x + btn.size.w > status.mouse.mousePos.x
                && btn.size.y < status.mouse.mousePos.y
                && btn.size.y + btn.size.h > status.mouse.mousePos.y
                && status.mouse.mouseButtonStatus[0]
                && !btn.disabled
            ) {
                hit = true;
                btn.pressed = true;
                self.manage("btnpressed", btn);
            } else {
                btn.pressed = false;
            }
        } else {
            btn.pressed = false;
        }
    }
    if (hit) {
        document.body.style.cursor = "pointer";
    } else {
        document.body.style.cursor = "default";
    }
};
MenuStage.prototype.mouseUpHandler = function (status) {
    var btn, b;
    for (b = 0; b < this.config.buttons.length; b++) {
        btn = this.config.buttons[b];
        btn.pressed = false
    }
};
MenuStage.prototype.mouseMoveHandler = function (status) {
    var self = this;
    var btn, b, hit;
    for (b = 0; b < self.config.buttons.length; b++) {
        btn = self.config.buttons[b];
        if (btn.size) {
            if (btn.size.x < status.mouse.mousePos.x
                && btn.size.x + btn.size.w > status.mouse.mousePos.x
                && btn.size.y < status.mouse.mousePos.y
                && btn.size.y + btn.size.h > status.mouse.mousePos.y
                && !btn.disabled
            ) {
                hit = true;
                btn.hover = true;
            } else {
                btn.hover = false;
            }
        } else {
            btn.hover = false;
        }
    }
    if (hit) {
        document.body.style.cursor = "pointer";
    } else {
        document.body.style.cursor = "default";
    }
};
//</editor-fold>