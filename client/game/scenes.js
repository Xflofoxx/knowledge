'use strict';

class SceneFSM {

    constructor() {
        this._scenes = new Map();
        this.currScene = null;

        this._insertedSceneID = 0;
    }

    /**
     * Add Scene object to game
     * @param {Scene} scene - The Scene to add
     * @return {number} - the Scene ID
     */
    async add(scene) {
        this._insertedSceneID++;
        await scene.onCreate();
        this._scenes.set(this._insertedSceneID, scene);
        return this._insertedSceneID;
    }

    /**
     * Removes a Scene from the game
     * @param {number}} sceneID - the Scene ID
     */
    remove(sceneID) {
        const target = this._scenes.get(sceneID);

        if (target) {
            // clear currScene if is the target scene to prevent other updates and drawing calls
            if (this.currScene.name === target.name) {
                this.currScene = null;
            }
            target.onDestroy();
            this._scenes.remove(sceneID);
        }
    }

    /**
     * Switch the current scene to target scene
     * @param {number} sceneID - The target scene id 
     */
    switchTo(sceneID) {
        const target = this._scenes.get(sceneID);

        if (target) {
            if (this.currScene) {
                this.currScene.onDeactivate();
            }
            this.currScene = target;
            this.currScene.onActivate();
        }
    }

    draw(secondsPassed) {
        if (this.currScene) {
            this.currScene.draw(secondsPassed);
        }
    }

    update(secondsPassed) {
        if (this.currScene) {
            this.currScene.update(secondsPassed);
        }
    }

    lateUpdate(secondsPassed) {
        if (this.currScene) {
            this.currScene.lateUpdate(secondsPassed);
        }
    }

}


class Scene {

    constructor(name, engine, config = { options: {}, assets: [] }) {
        this.name = name;
        this._engine = engine;
        this._config = config.options;
        this._assets = config.assets;

        this._gameObjects = [];
    }

    get backCanvas() {
        return this._engine._canvas.back;
    }
    get mainCanvas() {
        return this._engine._canvas.main;
    }

    _clearScene() {
        // Clear the entire canvas
        this._engine._ctx.main.clearRect(0, 0, this.mainCanvas.width, this.mainCanvas.height);
        // Clear the entire canvas
        this._engine._ctx.back.clearRect(0, 0, this.backCanvas.width, this.backCanvas.height);
    }

    async _loadScene() {
        let asset;
        console.log("Loading scene " + this.name);

        for (const a of this._assets) {
            switch (a.type) {
                case "image":
                    asset = await Utils.Assets.fetchImage(a.uri);
                    this._engine.assetsManagers.set(a.id, asset);
                    break;
            }
        }

        return this;
    }

    async onCreate() {
        console.log("Scene.onCreate(): name = " + this.name);
        await this._loadScene();
    }
    onDestroy() {
        console.log("Scene.onDestroy(): name = " + this.name);
    }
    onActivate() {
        console.log("Scene.onActivate(): name = " + this.name);
    }
    onDeactivate() {
        console.log("Scene.onDeactivate(): name = " + this.name);
    }

    draw(secondsPassed) {
        this._clearScene();

        // Loop over all game objects
        for (let i = 0; i < this._gameObjects.length; i++) {
            this._gameObjects[i].draw(secondsPassed);
        }
    }

    update(secondsPassed) {
        // Loop over all game objects
        for (let i = 0; i < this._gameObjects.length; i++) {
            this._gameObjects[i].update(secondsPassed);
        }
    }

    lateUpdate(secondsPassed) {

    }

    drawSceneInfo() {
        return "Playing stage " + this.name;
    }
}