using Metaverse.UI.Catalog;
using Suduck;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metaverse.UI
{
    public class BuildSceneUIController
    {
        private const string VIEW_PATH = "BuildSceneUI";

        public event Action<CatalogItemData> OnCatalogItemSpawn;

        private CatalogController catalogController;

        private BuildSceneUIView view;

        public BuildSceneUIController()
        {
            view = GameObject.Instantiate(Resources.Load<GameObject>(VIEW_PATH)).GetComponent<BuildSceneUIView>();
            CreateControllers();
            ConfigureControllers();

            view.Initialize(catalogController);
        }

        public void Dispose()
        {
            catalogController.OnCatalogItemSpawn -= CatalogItemSelected;
            catalogController.Dispose();
        }

        private void CreateControllers()
        {
            catalogController = new CatalogController();
        }

        private void ConfigureControllers()
        {
            ConfigureCatalogController();
        }

        private void ConfigureCatalogController()
        {
            catalogController.OnCatalogItemSpawn += CatalogItemSelected;
        }

        private void CatalogItemSelected(CatalogItemData item)
        {
            OnCatalogItemSpawn?.Invoke(item);
        }
    }
}
