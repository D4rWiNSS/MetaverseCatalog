using Metaverse.Scenes;
using Metaverse.UI;
using Metaverse.UI.Catalog;
using UnityEngine;

namespace Metaverse
{
    public class Core : MonoBehaviour
    {
        private BuildSceneUIController buildSceneUIController;
        private SceneController sceneController;

        void Awake()
        {
            // We create the UI controller and it will handle all the the UI on his own
            buildSceneUIController = new BuildSceneUIController();
            buildSceneUIController.OnCatalogItemSpawn += CatalogItemSpawn;
            buildSceneUIController.OnLoadNextScene += LoadNextScene;
            buildSceneUIController.OnLoadPreviousScene += LoadPreviousScene;

            // We create the scene controller
            sceneController = new SceneController();
        }

        public void Dispose()
        {
            // We dipose the UI 
            buildSceneUIController.OnCatalogItemSpawn -= CatalogItemSpawn;
            buildSceneUIController.OnLoadNextScene -= LoadNextScene;
            buildSceneUIController.OnLoadPreviousScene -= LoadPreviousScene;
            buildSceneUIController.Dispose();

            // We dispose the scene controller
            sceneController.Dispose();
        }

        private void CatalogItemSpawn(CatalogItemData item) => sceneController.SpawnItem(item);
        
        private void LoadNextScene() => sceneController.LoadNextScene();

        private void LoadPreviousScene() => sceneController.LoadPreviousScene();
    }
}
