using Metaverse.UI.Catalog;
using UnityEngine;

namespace Metaverse.Scenes
{
    public class SceneController
    {
        private const string SCENE_PATH_DATA = "Scenes";

        private SceneData[] scenes;
        private SceneItemController sceneItemSpawner;

        private SceneData activeScene;
        private int currentIndex = 0;

        public SceneController()
        {
            // We load all the scenes
            scenes = Resources.LoadAll<SceneData>(SCENE_PATH_DATA);

            // Create and configure the spawner
            sceneItemSpawner = new SceneItemController();
            sceneItemSpawner.OnNewItemAdded += NewItemAddedToScene;

            // If we have more than 1 scene, we load it 
            if (scenes.Length > 0 )
                LoadScene(scenes[0]);
        }

        public void Dispose()
        {
            sceneItemSpawner.OnNewItemAdded -= NewItemAddedToScene;
            sceneItemSpawner.Dispose();

            if (activeScene != null)
                activeScene.Dispose();
        }

        public void LoadNextScene()
        {
            if (currentIndex + 1 >= scenes.Length)
                return;

            currentIndex++;
            LoadScene(scenes[currentIndex]);
        }

        public void LoadPreviousScene()
        {
            if (currentIndex <= 0)
                return;

            currentIndex--;
            LoadScene(scenes[currentIndex]);
        }

        public void SpawnItem(CatalogItemData itemToSpawn)
        {
            sceneItemSpawner.SpawnItem(itemToSpawn);
        }

        private void NewItemAddedToScene(SceneItemRepresentantion sceneItem, GameObject itemGameObject)
        {
            activeScene.AddItem(sceneItem,itemGameObject);
        }

        private void LoadScene(SceneData sceneData)
        {
            // if we have an active scene, we dispose it
            if(activeScene != null)
                activeScene.Dispose();

            sceneItemSpawner.Reset();
            sceneData.SpawnScene();
            activeScene = sceneData;        
        }       
    }
}
