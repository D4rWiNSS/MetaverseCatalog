using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Metaverse.UI.Catalog
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class CatalogItemAdapter : MonoBehaviour
    {
        public event Action<CatalogItemData> OnItemSelected;

        public Image itemThumbnailImage;
        public TextMeshProUGUI itemTitleText;

        private Button adapterButton;
        private CatalogItemData catalogItemData;

        private void Awake()
        {
            adapterButton = GetComponent<Button>();
            adapterButton.onClick.AddListener(Selected);
        }

        public void SetData(CatalogItemData catalogItemData)
        {
            this.catalogItemData = catalogItemData;

            itemThumbnailImage.sprite = catalogItemData.GetThumbnail();
            itemTitleText.text = catalogItemData.GetTitle();
        }

        public void Dispose()
        {
            adapterButton.onClick.RemoveAllListeners();
            Destroy(gameObject);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Selected()
        {
            OnItemSelected?.Invoke(catalogItemData);
        }
    }
}
