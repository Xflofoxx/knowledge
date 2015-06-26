/**
 * Created by Dario on 11/06/2015.
 */
var AssetsManager = function AssetsManager() {
    this.LOG_TAG = "AssetsManager:";
    this.bundle = {};
};

AssetsManager.prototype.loadAssets = function loadAssets(assetList, next) {
    var self = this;
    var loadBatch = {
        count: 0,
        total: assetList.length,
        cb: next
    };
    var type, assetName, img, fileref;

    if (assetList.length > 0) {
        for (var a = 0; a < assetList.length; a++) {
            assetName = assetList[a];
            if (self.bundle[assetName] == null) {
                type = self.getAssetTypeFromExtension(assetName);
                switch (type) {
                    case 0:
                        img = new Image();
                        img.onload = function(){
                            self.onLoadedCallback(img, loadBatch);
                        };
                        img.src = assetName;
                        self.bundle[assetName] = img;
                        break;
                    case 1:
                        fileref = document.createElement("script");
                        fileref.setAttribute("type","text/javascript");
                        fileref.addEventListener('load',function(e){
                            self.onLoadedCallback(fileref, loadBatch);
                        },false);
                        fileref.setAttribute("src", assetName);
                        document.getElementsByTagName('head')[0].appendChild(fileref);
                        self.bundle[assetName] = fileref;
                        break;
                }
            } else {
                this.onLoadedCallback(self.bundle[assetList[a]], loadBatch);
            }
        }
    }
    else {
        this.onLoadedCallback(null, loadBatch);
    }
};
/**
 * Manages the queue of assets loading
 * @param asset
 * @param batch
 */
AssetsManager.prototype.onLoadedCallback = function onLoadedCallback(asset, batch) {
    // If the entire batch has been loaded,
    // call the callback.
    batch.count++;
    if (batch.count == batch.total) {
        batch.cb(asset);
    }
};
/**
 * Identify the type of asset to be loaded.
 * @param fileName
 * @returns {number}
 */
AssetsManager.prototype.getAssetTypeFromExtension = function getAssetTypeFromExtension(fileName) {
    if (fileName.indexOf('.jpg') !== -1 ||
        fileName.indexOf('.jpeg') !== -1 ||
        fileName.indexOf('.png') !== -1 ||
        fileName.indexOf('.gif') !== -1 ||
        fileName.indexOf('.wp') !== -1) {
        // It's an image!
        return 0;
    }
    if (fileName.indexOf('.js') !== -1 ||
        fileName.indexOf('.json') !== -1) {
        // It's javascript!
        return 1;
    }
    // Uh Oh
    return -1;
};