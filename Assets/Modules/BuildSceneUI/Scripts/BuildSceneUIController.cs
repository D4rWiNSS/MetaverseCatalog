using Metaverse.UI.Catalog;
using System;
using UnityEngine;

namespace Metaverse.UI
{
    public class BuildSceneUIController
    {
        private const string VIEW_PATH = "BuildSceneUI";

        public event Action<CatalogItemData> OnCatalogItemSpawn;

        public event Action OnLoadNextScene;
        public event Action OnLoadPreviousScene;

        private CatalogController catalogController;

        private BuildSceneUIView view;

        public BuildSceneUIController()
        {
            // We create configure all the controllers
            CreateControllers();
            ConfigureControllers();

            // We create and inject the necessaries dependencies for the view
            view = GameObject.Instantiate(Resources.Load<GameObject>(VIEW_PATH)).GetComponent<BuildSceneUIView>();

            view.Initialize(catalogController);
            view.OnNextButtonPress += NextScene;
            view.OnPreviousButtonPress += PreviousScene;
        }

        public void Dispose()
        {
            catalogController.OnCatalogItemSpawn -= CatalogItemSelected;
            catalogController.Dispose();

            view.OnNextButtonPress -= NextScene;
            view.OnPreviousButtonPress -= PreviousScene;
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

        private void CatalogItemSelected(CatalogItemData item) => OnCatalogItemSpawn?.Invoke(item);
        
        private void NextScene() => OnLoadNextScene?.Invoke();

        private void PreviousScene() => OnLoadPreviousScene?.Invoke();
    }
}
