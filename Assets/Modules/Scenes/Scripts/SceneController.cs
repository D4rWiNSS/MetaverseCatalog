using Metaverse.UI.Catalog;
using UnityEngine;

namespace Metaverse.Scenes
{
    public class SceneController
    {
        private const string SCENE_PATH_DATA = "Scenes";

        private SceneData[] scenes;
        private SceneNewItemController sceneItemSpawner;

        private SceneData activeScene;
        private int currentIndex = 0;

        public SceneController()
        {
            // We load all the scenes from the resources folder for simplicity,
            // But we should load the data from a server and create the instances from the data
            scenes = Resources.LoadAll<SceneData>(SCENE_PATH_DATA);

            // We just reset the data each play session using a little "hack" for the simplicity of the example.
            // I assume that we were get the data of the scene from a server so we can create an instance for each scene and of course this won't be necessary
            foreach (SceneData sceneData in scenes)
                sceneData.ResetScene();

            // Create and configure the spawner
            sceneItemSpawner = new SceneNewItemController();
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

            // We may be at the middle of spawning an item, so we reset the spawner
            sceneItemSpawner.Reset();
            sceneData.SpawnScene();
            activeScene = sceneData;        
        }       
    }
}
