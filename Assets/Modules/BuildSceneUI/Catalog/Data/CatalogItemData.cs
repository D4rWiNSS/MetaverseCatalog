using System.Linq;
using UnityEngine;

namespace Metaverse.UI.Catalog
{
    [CreateAssetMenu(menuName = "Metaverse/Catalog Item")]
    public class CatalogItemData : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private Sprite thumbnail;
        [SerializeField] private GameObject prefab;

        /// <summary>
        /// Get the thumbnail of the item
        /// </summary>
        /// <returns></returns>
        public Sprite GetThumbnail() => thumbnail;

        /// <summary>
        /// Get the title of the item
        /// </summary>
        /// <returns></returns>
        public string GetTitle() => title;

        /// <summary>
        /// Get the prefab that this item represents
        /// </summary>
        /// <returns></returns>
        public GameObject GetPrefab() => prefab;

        // Override the OnValidate method to perform validation when the ScriptableObject is modified in the editor.
        private void OnValidate()
        {
            ValidateUniqueTitle();
        }

        // Validate if the title is unique among other instances of the ScriptableObject.
        private void ValidateUniqueTitle()
        {
            // Get all instances of the ScriptableObject in the project.
            CatalogItemData[] scriptableObjects = Resources.FindObjectsOfTypeAll<CatalogItemData>();

            foreach (CatalogItemData so in scriptableObjects)
            {
                // Skip validating against the current instance.
                if (so == this)
                    continue;

                // Check if the name is already used by another instance.
                if (so.title == title)
                {
                    Debug.LogError("Duplicate name detected! Please choose a unique name.");
                    title = GenerateUniqueTitle();
                    break;
                }
            }
        }

        // Generate a unique name by appending a number to the original name.
        private string GenerateUniqueTitle()
        {
            string baseName = title;
            int counter = 1;

            while (true)
            {
                string newName = baseName + counter.ToString();
                bool nameExists = Resources.FindObjectsOfTypeAll<CatalogItemData>()
                    .Any(so => so.title == newName);

                if (!nameExists)
                    return newName;

                counter++;
            }
        }
    }
}
