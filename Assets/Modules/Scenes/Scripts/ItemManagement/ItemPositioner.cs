using System;
using UnityEngine;

namespace Metaverse.Scenes
{
    public class ItemPositioner : MonoBehaviour 
    {
        public event Action<Vector3> OnItemPositioned;

        private LayerMask floorLayer;
        private bool isPlaced = false;

        private void Awake()
        {
            floorLayer = LayerMask.GetMask(Constants.FLOOR_LAYER_NAME);
            // We set the item far away so it only shows when the user put the mouse inside the floor
            transform.position = Vector3.one * 9999;
        }

        void Update()
        {
            if (isPlaced)
                return;

            // A raycast from the mouse to the floor to position the item
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, floorLayer))
            {
                transform.position = hit.point;
            }
            else
            {
                // If the mouse is not over the floow, we return since we don't want to include mouse clicks that are not peformed in the floor ( like UI clicks)
                return;
            }

            // If we detect a mouse left click and the item is in the floor, we position it
            if (Input.GetMouseButtonDown(0))
            {
                isPlaced = true;
                OnItemPositioned?.Invoke(transform.position);             
            }
        }
    }
}
