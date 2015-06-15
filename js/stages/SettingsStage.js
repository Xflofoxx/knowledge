/**
 * Created by Dario on 12/06/2015.
 */

var SettingsStage = function SettingsStage(AM, IO, current) {
    Stage.call(this, "settings", {
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
        title: "Settings",
        current: current,
        buttons: [
            {
                action: "save",
                title: "Save",
                hover: false,
                pressed: false,
                disabled: false
            },
            {
                action: "back",
                title: "Back",
                hover: false,
                pressed: false,
                disabled: false
            }
        ]
    }, AM, IO);
    this.LOG_TAG = "SettingsStage:";
};
utils.inherit(SettingsStage, Stage);

SettingsStage.prototype.render = function render(ctx, forceRedraw) {
    var panelSize = {
        w: ctx.canvas.width * 60 / 100,
        h: ctx.canvas.height * 80 / 100
    };
    var buttonSize = {
        w: 100,
        h: 40
    };
    var btn;
    //clear background context
    ctx.fillStyle = "#000000";
    ctx.fillRect(0, 0, ctx.canvas.width, ctx.canvas.height);
    panelSize.x = (ctx.canvas.width - panelSize.w) / 2;
    panelSize.y = (ctx.canvas.height - panelSize.h) / 2;
    buttonSize.x = panelSize.x;
    buttonSize.y = panelSize.y + panelSize.h - ui.styles.panel.padding - buttonSize.h - ui.styles.buttons.margin.bottom;
    panelSize.title = this.config.title;
    ui.drawPanel(ctx, panelSize, true, true);
    //draw buttons on the right
    for (var i = 0; i < this.config.buttons.length; i++) {
        btn = this.config.buttons[i];
        btn.disabledAsset = this.AM.bundle.settings.btn;
        btn.pressedAsset = this.AM.bundle.settings.btnDown;
        btn.hoverAsset = this.AM.bundle.settings.btn;
        btn.defaultAsset = this.AM.bundle.settings.btn;
        btn.size = {
            x: panelSize.x + panelSize.w - ui.styles.panel.padding - (buttonSize.w + ui.styles.buttons.margin.left + ui.styles.buttons.margin.right) * (i + 1),
            y: buttonSize.y,
            w: buttonSize.w,
            h: buttonSize.h
        };
        ui.drawButton(ctx, btn);
    }

    return ctx;
};

SettingsStage.prototype.manage = function manage(event, data, suppressEmit) {
    var pEvent = event,
        pData = data,
        pSuppress = suppressEmit;
    switch (event) {
        case "btnpressed":
            switch (data.action) {
                case "save":
                    //this.drawSettings();
                    break;
                case "back":
                    break;
            }
            break;
    }
    //call the parent method that will emit the event
    Stage.prototype.manage.call(this, pEvent, pData, pSuppress);
};

//<editor-fold desc="# IO Handlers">
SettingsStage.prototype.mouseDownHandler = function (status) {
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
SettingsStage.prototype.mouseUpHandler = function (status) {
    var btn, b;
    for (b = 0; b < this.config.buttons.length; b++) {
        btn = this.config.buttons[b];
        btn.pressed = false
    }
};
SettingsStage.prototype.mouseMoveHandler = function (status) {
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