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

KnowledgeGame.prototype.test2Areduce = function test2Areduce(){
    var tarray=[], rarray=[], r,c;
    for(r=0;r<10;r++){
        tarray[r]= [];
        for(c=0;c<20;c++){
            tarray[r][c]=Math.abs(Math.random() * 256);
        }
    }

    //var rc = 5, cc=2;
    //for(var r=0;r< a.length;r+=rc){
    //    rarray = tarray.reduce(function(pa, a) {
    //        var chunks=[],chunk;
    //        for(var c=0;c< a.length;c+=cc){
    //            chunk = a.slice(c,c+cc);
    //            chunk = chunk.reduce(function(a,b){
    //                return (a+b)/2;
    //            });
    //            chunks.push(chunk);
    //        }
    //        return pa.push(chunks);
    //    });
    //}


    console.log("test",tarray,rarray);
    this.stop();
};