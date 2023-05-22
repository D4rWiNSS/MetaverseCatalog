using Metaverse.UI.Catalog;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Metaverse.UI
{
    public class BuildSceneUIView : MonoBehaviour
    {
        public event Action OnNextButtonPress;
        public event Action OnPreviousButtonPress;

        [SerializeField] private CatalogView catalogGridView;

        [SerializeField] private Button nextSceneButton;
        [SerializeField] private Button previusSceneButton;

        private void Awake()
        {
            // We setup the view
            nextSceneButton.onClick.AddListener(() => OnNextButtonPress?.Invoke());
            previusSceneButton.onClick.AddListener(() => OnPreviousButtonPress?.Invoke());
        }

        public void Initialize(CatalogController catalogController) 
        {
            // We inject the views into the controllers
            catalogController.Initialize(catalogGridView);
        }

        public void Dispose()
        {
            nextSceneButton.onClick.RemoveAllListeners();
            previusSceneButton.onClick.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}
