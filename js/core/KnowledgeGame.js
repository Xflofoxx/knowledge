var KnowledgeGame = function KnowledgeGame(canvas, AM) {
    this.LOG_TAG = "KnowledgeGame:";
    this.canvas = canvas;
    this.IO = new InputManager(this);
    this.engine = new JagEngine(this.canvas);
    this.AM = AM;
    this.stages = {};
    this.currentConfig = {};
};

KnowledgeGame.prototype.init = function init(initComplete) {
    var self = this;
    this.engine.init(this.IO);

    this.stages.menu = new MenuStage(this);
    this.stages.menu.on("btnpressed", function (btn) {
        console.log(self.LOG_TAG + " btnpressed!", btn.action, btn);
        switch (btn.action) {
            case "new":
                self.engine.loadStage(self.stages.game);
                break;
            case "continue":
                break;
            case "load":
                break;
            case "settings":
                self.engine.loadStage(self.stages.settings);
                break;
        }
    });

    this.stages.settings = new SettingsStage(this);
    this.stages.settings.on("btnpressed", function (btn) {
        console.log(self.LOG_TAG + " btnpressed!", btn.action, btn);
        switch (btn.action) {
            case "back":
                self.engine.loadStage(self.stages.menu);
                break;
            case "save":
                break;

        }
    });

    this.stages.game = new MainStage(this);

    console.log(this.LOG_TAG + " init complete!");
    initComplete(null);
};

KnowledgeGame.prototype.start = function start() {
    console.log(this.LOG_TAG + " start!");
    this.engine.start();
    this.engine.loadStage(this.stages.menu);
};
KnowledgeGame.prototype.stop = function stop() {
    console.log(this.LOG_TAG + " stop!");
    this.engine.stop();
};