using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Diamond.SkeletonDefense.Util;
using Diamond.SkeletonDefense.UI;
using Diamond.SkeletonDefense.Character;

namespace Diamond.SkeletonDefense.ReleasedNewCharacterScene
{
    public class ReleasedNewCharacterSceneManager : MonoBehaviour
    {
        public ReleaseNewCharacterInfo _info;
        public Button _next;
        public SceneLoader _sceneLoader;
        public Transform _position;
        public CharacterStatusInfomationUI _characterStatusInfomationUI;

        private void Start()
        {
            _info = ReleaseNewCharacterInfo.GetReleaseNewCharacterInfo();
            _next.onClick.AddListener(() => { _sceneLoader.SceneLoad(_info._nextStageName); });
            try
            {
                var character = Resources.Load(_info._newCharacterName);
                var characterInstance = Instantiate(character) as GameObject;
                characterInstance.transform.position = _position.position;
                _characterStatusInfomationUI.SetCharacter(characterInstance.GetComponent<CharacterBase>());
            }
            catch
            {
                Debug.LogError("cannot load character");
            }
        }
    }
}
