using System.Collections.Generic;
using UnityEngine;

namespace Metaverse.Scenes
{
    [CreateAssetMenu(menuName = "Metaverse/Scene")]
    public class SceneData : ScriptableObject
    {
        private const string ROOT_GAMEOBJECT_NAME = "ActiveScene";

        private List<SceneItemRepresentantion> sceneRepresentantion = new List<SceneItemRepresentantion>();

        private List<GameObject> activeGameObjects = new List<GameObject>();
        private GameObject rootGameObject;

        public void ResetScene()
        {
            sceneRepresentantion.Clear();
        }

        public void SpawnScene()
        {         
            // This gameobject will hold all the gameobjects related to the scene
            rootGameObject = new GameObject(ROOT_GAMEOBJECT_NAME);

            // We spawn all the items that are associated with the scene
            foreach (SceneItemRepresentantion item in sceneRepresentantion)
            {
                GameObject instantiatedItem = Instantiate(item.GetItemPrefab(), rootGameObject.transform);
                instantiatedItem.transform.position = item.GetPosition();
                activeGameObjects.Add(instantiatedItem);
            }
        }

        public void Dispose()
        {
            Destroy(rootGameObject);
        }

        public void AddItem(SceneItemRepresentantion itemToAdd, GameObject gameObject)
        {
            // We add the item to the scene and set its parenting
            sceneRepresentantion.Add(itemToAdd);
            gameObject.transform.SetParent(rootGameObject.transform);
        }
    }
}