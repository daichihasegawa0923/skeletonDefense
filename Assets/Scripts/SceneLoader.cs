using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Diamond.SkeletonDefense.UI;

namespace Diamond.SkeletonDefense
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private Fade _fade;

        [SerializeField]
        private ActiveCanvasSwitcher _activeCanvasSwitcher;

        private void Awake()
        {
            _fade.gameObject.SetActive(true);
        }

        // Start is called before the first frame update
        void Start()
        {
            _fade.FadeOut(1.0f);
        }

        public void SceneLoad(string sceneName)
        {
            _fade.FadeIn(1.0f, () => { SceneManager.LoadScene(sceneName); });
            if (_activeCanvasSwitcher)
                _activeCanvasSwitcher.Active(string.Empty);
        }
    }
}