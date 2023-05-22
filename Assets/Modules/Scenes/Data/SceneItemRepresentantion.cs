using Metaverse.UI.Catalog;
using System;
using UnityEngine;

namespace Metaverse.Scenes
{
    [Serializable]
    public class SceneItemRepresentantion : ScriptableObject
    {
        private Vector3 position;
        private CatalogItemData itemsData;

        public void SetCatalogItem(CatalogItemData itemsData) => this.itemsData = itemsData;
        
        public void SetPosition(Vector3 postion) => this.position = postion;

        public Vector3 GetPosition() => position;

        public CatalogItemData GetData() => itemsData;

        public GameObject GetItemPrefab() => itemsData.GetPrefab();
    }
}
