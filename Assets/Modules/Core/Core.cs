using Metaverse.Scenes;
using Metaverse.UI;
using Metaverse.UI.Catalog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metaverse
{
    public class Core : MonoBehaviour
    {
        private BuildSceneUIController buildSceneUIController;
        private SceneController sceneController;

        void Awake()
        {
            buildSceneUIController = new BuildSceneUIController();
            buildSceneUIController.OnCatalogItemSpawn += CatalogItemSpawn;
            buildSceneUIController.OnLoadNextScene += LoadNextScene;
            buildSceneUIController.OnLoadPreviousScene += LoadPreviousScene;
            sceneController = new SceneController();
        }

        public void Dispose()
        {
            buildSceneUIController.OnCatalogItemSpawn -= CatalogItemSpawn;
            buildSceneUIController.OnLoadNextScene -= LoadNextScene;
            buildSceneUIController.OnLoadPreviousScene -= LoadPreviousScene;
        }

        private void CatalogItemSpawn(CatalogItemData item) => sceneController.SpawnItem(item);
        
        private void LoadNextScene() => sceneController.LoadNextScene();

        private void LoadPreviousScene() => sceneController.LoadPreviousScene();
    }
}
