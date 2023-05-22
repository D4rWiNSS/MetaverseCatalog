using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metaverse.Catalog
{
    [CreateAssetMenu(menuName = "Metaverse/Catalog Item")]
    public class CatalogItemData : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private Sprite thumbnail;
        [SerializeField] private GameObject prefab;
    }
}
