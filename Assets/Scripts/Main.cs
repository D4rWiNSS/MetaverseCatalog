using Metaverse.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metaverse
{
    public class Main : MonoBehaviour
    {
        private BuildSceneUIController buildSceneUIController;

        void Awake()
        {
            buildSceneUIController = new BuildSceneUIController();
        }

        private void OnDestroy()
        {
            buildSceneUIController.Dispose();
        }
    }
}
