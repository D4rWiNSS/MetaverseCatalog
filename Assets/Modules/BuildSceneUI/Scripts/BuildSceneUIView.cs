using Metaverse.UI.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Suduck
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
            nextSceneButton.onClick.AddListener(() => OnNextButtonPress?.Invoke());
            previusSceneButton.onClick.AddListener(() => OnPreviousButtonPress?.Invoke());
        }

        public void Initialize(CatalogController catalogController) 
        {
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
