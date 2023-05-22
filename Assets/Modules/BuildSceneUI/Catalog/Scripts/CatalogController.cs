using System;
using UnityEngine;

namespace Metaverse.UI.Catalog
{
    public class CatalogController
    {
        private const string CATALOG_PATH_DATA = "CatalogItems";

        public event Action<CatalogItemData> OnCatalogItemSpawn;

        private CatalogItemData[] catalogItems;

        private CatalogView catalogView;

        public CatalogController()
        {
            // We load all the items in when the controller is created
            catalogItems = Resources.LoadAll<CatalogItemData>(CATALOG_PATH_DATA);
        }

        public void Initialize(CatalogView catalogView)
        {
            // We prepare the view of the catalog
            this.catalogView = catalogView;
            catalogView.OnCatalogItemSpawn += CatalogItemSelected;

            catalogView.Initialize();
            catalogView.SetItems(catalogItems);
        }

        public void Dispose()
        {
            catalogView.OnCatalogItemSpawn -= CatalogItemSelected;
            catalogView.Dispose();
        }

        private void CatalogItemSelected(CatalogItemData item)
        {
            OnCatalogItemSpawn?.Invoke(item);
        }
    }
}
