using Metaverse.UI.Catalog;
using System;
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
            // We create the representantion of the item
            currentSceneItemRepresentantion = ScriptableObject.CreateInstance<SceneItemRepresentantion>();
            currentSceneItemRepresentantion.SetCatalogItem(catalogItem);
            
            // We spawn the item prefab
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
            DestroyItemPositionerComponent();
        }

        private void ItemPositioned(Vector3 position) 
        {
            DestroyItemPositionerComponent();

            // We set the new position of the object and fire the event because the item is ready
            currentSceneItemRepresentantion.SetPosition(position);
            OnNewItemAdded?.Invoke(currentSceneItemRepresentantion, positionerComponent.gameObject);

            // We remove the references since we are not setting this item anymore
            currentSceneItemRepresentantion = null;
            positionerComponent = null;
        }

        private void DestroyItemPositionerComponent()
        {
            // We just destroy the component if exists
            if (positionerComponent == null)
                return;

            positionerComponent.OnItemPositioned -= ItemPositioned;
            GameObject.Destroy(positionerComponent);
        }
    }
}
