using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Diamond.SkeletonDefense.ReleasedNewCharacterScene
{
    public class ReleasedNewCharacterSceneManager : MonoBehaviour
    {
        public ReleaseNewCharacterInfo _info;
        public Button _next;
        public SceneLoader ceneLoader;
        public Transform _position;

        private void Start()
        {
            _info = ReleaseNewCharacterInfo.GetReleaseNewCharacterInfo();
        }
    }
}
