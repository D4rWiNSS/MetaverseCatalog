using Metaverse.UI.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Suduck
{
    public class BuildSceneUIView : MonoBehaviour
    {
        [SerializeField] private CatalogView catalogGridView;

        public void Initialize(CatalogController catalogController) 
        {
            catalogController.Initialize(catalogGridView);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
