using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Metaverse.UI.Catalog
{
    public class CatalogGridView : MonoBehaviour
    {
        public event Action<CatalogItemData> OnItemSelected;

        [SerializeField] private CatalogItemAdapter catalogItemAdapterPrefab;

        [Header("Prefab References")]
        [SerializeField] private Transform contentParentTransform;

        private List<CatalogItemAdapter> adapters = new List<CatalogItemAdapter>();

        public void Initialize(int itemsPerPage)
        {
            PrePopulateItems(itemsPerPage);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetItems(List<CatalogItemData> catalogItems)
        {
            int counter = 0;
            foreach (CatalogItemAdapter adapter in adapters)
            {
                if(counter >= catalogItems.Count)
                {
                    adapter.Hide();
                    continue;
                }

                adapter.SetData(catalogItems[counter]);
                adapter.Show();
                counter++;
            }
        }

        public void Dispose()
        {
            foreach(CatalogItemAdapter adapter in adapters)
            {
                adapter.OnItemSelected -= CatalogItemSelected;
                adapter.Dispose();
            }
        }

        private void PrePopulateItems(int amountOfItemsToPopulate)
        {
            for(int i = 0; i <= amountOfItemsToPopulate; i++)
            {
                var adapter = Instantiate(catalogItemAdapterPrefab, contentParentTransform);
                adapter.OnItemSelected += CatalogItemSelected;
                adapters.Add(adapter);
            }
        }

        private void CatalogItemSelected(CatalogItemData item)
        {
            OnItemSelected?.Invoke(item);
        }
    }
}
