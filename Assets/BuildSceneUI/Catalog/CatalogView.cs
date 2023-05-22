using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Metaverse.UI.Catalog
{
    public class CatalogView : MonoBehaviour
    {
        public event Action<CatalogItemData> OnCatalogItemSpawn;

        [SerializeField] private int itemsPerPage = 5;

        [Header("Prefab References")]
        [SerializeField] private CatalogGridView gridView;

        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;

        [SerializeField] private TextMeshProUGUI paginationText;

        private int currentIndex = 0;
        private int maxIndex = 0;

        private CatalogItemData[] catalogItems;

        public void Initialize()
        {
            gridView.Initialize(itemsPerPage);
            gridView.OnItemSelected += CatalogItemSelected;

            previousButton.onClick.AddListener(PreviousPage);
            nextButton.onClick.AddListener(NextPage);
        }

        public void Dispose()
        {
            gridView.OnItemSelected -= CatalogItemSelected;

            previousButton?.onClick.RemoveAllListeners();
            nextButton?.onClick.RemoveAllListeners();

            Destroy(gameObject);
        }

        public void SetItems(CatalogItemData[] catalogItems)
        {
            this.catalogItems = catalogItems;
            currentIndex = 0;
            maxIndex = catalogItems.Length / itemsPerPage;
            SetIndex(currentIndex);
            gridView.Show();
        }

        private void NextPage()
        {
            if (currentIndex + 1 > maxIndex)
                return;

            currentIndex++;
            SetIndex(currentIndex);
        }

        private void PreviousPage() 
        {
            if (currentIndex <= 0)
                return;

            currentIndex--;
            SetIndex(currentIndex);
        }

        private void SetIndex(int index)
        {
            nextButton.interactable = index == maxIndex ? false : true;
            previousButton.interactable = index == 0 ? false : true;

            List<CatalogItemData> itemsToShow = new List<CatalogItemData>();
            int initialIndex = index * itemsPerPage;

            for (int i = initialIndex; i < initialIndex+itemsPerPage; i++)
            {
                if (catalogItems.Length >= i)
                    break;

                CatalogItemData catalogItemData = catalogItems[index];
                itemsToShow.Add(catalogItemData);
            }

            gridView.SetItems(itemsToShow);

            SetPaginationText();
        }

        private void SetPaginationText()
        {
            paginationText.text = currentIndex + "/" + maxIndex;
        }

        private void CatalogItemSelected(CatalogItemData item)
        {
            OnCatalogItemSpawn?.Invoke(item);
        }
    }
}
