using Metaverse.UI.Catalog;
using Suduck;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Metaverse.Scenes
{
    public class SceneItemController
    {
        public event Action<SceneItemRepresentantion, GameObject> OnNewItemAdded;

        SceneItemRepresentantion currentSceneItemRepresentantion;
        ItemPositioner positionerComponent;

        public void SpawnItem(CatalogItemData catalogItem)
        {
            currentSceneItemRepresentantion = ScriptableObject.CreateInstance<SceneItemRepresentantion>();
            currentSceneItemRepresentantion.SetCatalogItem(catalogItem);
            
            var activeItem = GameObject.Instantiate(catalogItem.GetPrefab());
            positionerComponent = activeItem.AddComponent<ItemPositioner>();
            positionerComponent.OnItemPositioned += ItemPositioned;
        }

        public void Dispose()
        {
            Reset();
        }

        public void Reset()
        {
            if (positionerComponent == null)
                return;

            positionerComponent.OnItemPositioned -= ItemPositioned;
            GameObject.Destroy(positionerComponent.gameObject);           
        }

        private void ItemPositioned(Vector3 position) 
        {
            positionerComponent.OnItemPositioned -= ItemPositioned;
            GameObject.Destroy(positionerComponent);

            currentSceneItemRepresentantion.SetPosition(position);
            OnNewItemAdded?.Invoke(currentSceneItemRepresentantion, positionerComponent.gameObject);

            currentSceneItemRepresentantion = null;
            positionerComponent = null;
        }
    }
}
