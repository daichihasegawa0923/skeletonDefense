using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.SkeletonDefense.UI
{
    /// <summary>
    /// Active canvas switcher
    /// </summary>
    public class ActiveCanvasSwitcher : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _canvases = new List<GameObject>();
        // Start is called before the first frame update
        void Start()
        {
        }

        public void Active(string canvasName)
        {
            _canvases.ForEach(canv => canv.SetActive(canv.name == canvasName));
        }

        public void Active(GameObject gameObject)
        {
            _canvases.ForEach(canv => canv.SetActive(canv.Equals(gameObject)));
        }
    }
}