/**
 * Created by Dario on 11/06/2015.
 */
var AssetsManager = function AssetsManager() {
    this.LOG_TAG = "AssetsManager:";
    this.bundle = {};
};

AssetsManager.prototype.load = function load(stage, assets, next) {
    var self = this;
    var completeRequests = 0;
    //save assets for mapping
    self.bundle[stage] = self.bundle[stage] || {};

    function createAssetHandler(as) {
        return function () {
            var res;
            switch (as.mime) {
                case "application/json":
                    if (this.readyState === 4 && this.status == 200) {
                        completeRequests -= 1;
                        res = JSON.parse(xhr.responseText);
                        self.bundle[stage][as.id || as.file] = res;
                    } else {
                        return;
                    }
                    break;
                case "image/jpeg":
                case "image/png":
                    completeRequests -= 1;
                    //do nothing...
                    break;
            }
            //map[as.file] = as.file;
            if (completeRequests === 0) {
                console.log(self.LOG_TAG + " complete!", self.bundle);
                next(null);
            }
        }
    }

    console.log(this.LOG_TAG + " load", assets);
    for (var a = 0; a < assets.length; a++) {
        //assets[a].id = utils.makeid(10);
        switch (assets[a].mime) {
            case "application/json":
                var xhr = new XMLHttpRequest();
                completeRequests += 1;
                xhr.onprogress = function (e) {
                    if (e.lengthComputable) {
                        var percent = (e.loaded / e.total) * 100;
                        console.log(self.LOG_TAG + " progress:", percent);
                    }
                };
                xhr.open('GET', assets[a].path + assets[a].file, true);
                xhr.onreadystatechange = createAssetHandler(assets[a]);
                xhr.send(null);
                break;
            case "image/jpeg":
            case "image/png":
                completeRequests += 1;
                self.bundle[stage][assets[a].id || assets[a].file] = new Image();
                self.bundle[stage][assets[a].id || assets[a].file].onload = createAssetHandler(assets[a]);
                self.bundle[stage][assets[a].id || assets[a].file].src = assets[a].path + assets[a].file;
                break;
        }

    }
};
