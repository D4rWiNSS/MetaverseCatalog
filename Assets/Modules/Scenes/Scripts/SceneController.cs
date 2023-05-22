using Metaverse.UI.Catalog;
using System.Collections;
using System.Collections.Generic;
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
            scenes = Resources.LoadAll<SceneData>(SCENE_PATH_DATA);
            sceneItemSpawner = new SceneItemController();
            sceneItemSpawner.OnNewItemAdded += NewItemAddedToScene;
            if (scenes.Length > 0 )
                LoadScene(scenes[0]);
        }

        public void Dispose()
        {
            sceneItemSpawner.OnNewItemAdded -= NewItemAddedToScene;
            sceneItemSpawner.Dispose();
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
            if(activeScene != null)
                activeScene.Dispose();

            sceneItemSpawner.Reset();
            sceneData.SpawnScene();
            activeScene = sceneData;        
        }       
    }
}
