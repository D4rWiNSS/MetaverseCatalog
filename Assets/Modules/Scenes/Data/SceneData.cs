using System.Collections.Generic;
using UnityEngine;

namespace Metaverse.Scenes
{
    [CreateAssetMenu(menuName = "Metaverse/Scene")]
    public class SceneData : ScriptableObject
    {
        private const string ROOT_GAMEOBJECT_NAME = "ActiveScene";

        [SerializeField] private List<SceneItemRepresentantion> sceneRepresentantion = new List<SceneItemRepresentantion>();

        private List<GameObject> activeGameObjects = new List<GameObject>();
        private GameObject rootGameObject;

        public void SpawnScene()
        {
            rootGameObject = new GameObject(ROOT_GAMEOBJECT_NAME);

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
            sceneRepresentantion.Add(itemToAdd);
            gameObject.transform.SetParent(rootGameObject.transform);
        }
    }
}