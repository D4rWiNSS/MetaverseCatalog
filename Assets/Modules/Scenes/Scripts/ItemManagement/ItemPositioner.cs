using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Suduck
{
    public class ItemPositioner : MonoBehaviour 
    {
        public event Action<Vector3> OnItemPositioned;

        private LayerMask floorLayer;
        private bool isPlaced = false;

        private void Awake()
        {
            floorLayer = LayerMask.GetMask(Constants.FLOOR_LAYER_NAME);
            transform.position = Vector3.one * 9999;
        }

        void Update()
        {
            if (isPlaced)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, floorLayer))
            {
                transform.position = hit.point;
            }
            else
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                isPlaced = true;
                OnItemPositioned?.Invoke(transform.position);             
            }
        }
    }
}
