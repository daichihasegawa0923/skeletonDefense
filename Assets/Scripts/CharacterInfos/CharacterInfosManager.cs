using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Diamond.SkeletonDefense.UI;
using Diamond.SkeletonDefense.Character;
using Diamond.SkeletonDefense.Data;

namespace Diamond.SkeletonDefense.CharacterInfos
{
    public class CharacterInfosManager : MonoBehaviour
    {
        [SerializeField]
        private CharacterStatusInfomationUI _infoUi;

        [SerializeField]
        private List<CharacterBase> _characterBases = new List<CharacterBase>();

        [SerializeField]
        private CharacterBase _currentCharacterBase;

        [SerializeField]
        private Transform _pivot;

        [SerializeField]
        private int _currentIndex = 0;

        private void Start()
        {
            var data = SaveData.Load();
            var list = data.ReleasedCharacterNames;
            list.ForEach(n =>
            {
                try
                {
                    var chara = (GameObject)Resources.Load(n);
                    _characterBases.Add(chara.GetComponent<CharacterBase>());
                }
                catch
                {
                    Debug.LogError("Cannot load character");
                }
            });

            SetCharacterSet(_currentIndex);
        }

        public void SetNextCharacter()
        {
            _currentIndex++;

            if (_currentIndex >= _characterBases.Count)
                _currentIndex = 0;

            SetCharacterSet(_currentIndex);
        }

        public void SetPreviousCharacter()
        {
            _currentIndex--;

            if (_currentIndex < 0)
                _currentIndex = _characterBases.Count - 1;

            SetCharacterSet(_currentIndex);
        }

        public void SetCharacterSet(int index)
        {
            if (_characterBases.Count == 0)
                return;

            if (index < 0 || index >= _characterBases.Count)
                return;

            if (_currentCharacterBase)
                Destroy(_currentCharacterBase.gameObject);

            var instance = Instantiate(_characterBases[index].gameObject);
            instance.transform.position = _pivot.transform.position;
            _currentCharacterBase = instance.GetComponent<CharacterBase>();
            _infoUi.SetCharacter(_currentCharacterBase);
        }
    }
}