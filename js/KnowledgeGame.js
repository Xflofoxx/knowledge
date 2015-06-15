var KnowledgeGame = function KnowledgeGame(canvas) {
    this.LOG_TAG = "EntropiaGame:";
    this.canvas = canvas;
    this.IO = new InputManager(this);
    this.engine = new JagEngine(this.canvas);
    this.assetsManager = new AssetsManager();
    this.stages = {};
    this.currentConfig = {};
};

KnowledgeGame.prototype.init = function init(initComplete) {
    var self = this;
    this.engine.init(this.IO);
    //Initialize the stages
    //this.stages.test = new Stage("test", {
    //    assets: [
    //        {
    //            path: window.location + '/../assets/',
    //            file: "test.json",
    //            mime: "application/json"
    //        },
    //        {
    //            path: window.location + '/../assets/',
    //            file: "tree.jpg",
    //            mime: "image/jpeg"
    //        }
    //    ]
    //}, this.assetsManager);
    this.stages.menu = new MenuStage(this.assetsManager, this.IO);
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

    this.stages.settings = new SettingsStage(this.assetsManager, this.IO, this.currentConfig);
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

    this.stages.game = new GameStage(this.assetsManager, this.IO, this.currentConfig);

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