using System.Collections;
using System.Collections.Generic;
using Diamond.SkeletonDefense.Character;
using Diamond.SkeletonDefense.Data;
using UnityEngine;

namespace Diamond.SkeletonDefense.TitleSpecial
{
    /// <summary>
    /// sporn character in title.
    /// </summary>
    public class CharacterSpornInTitle : MonoBehaviour
    {
        [SerializeField]
        private List<CharacterBase> _spornCharacters = new List<CharacterBase>();

        // Start is called before the first frame update
        void Start()
        {
            SpornCharacter();
        }

        void Update()
        {
            transform.eulerAngles += Vector3.up;
        }

        /// <summary>
        /// Sporn the random characters
        /// </summary>
        private void SpornCharacter()
        {
            var data = SaveData.Load();
            if(data.ReleasedCharacterNames != null && data.ReleasedCharacterNames.Count > 0)
            {
                foreach(var charaName in data.ReleasedCharacterNames)
                {
                    var loadedChara = (GameObject)Resources.Load(charaName);
                    _spornCharacters.Add(loadedChara.GetComponent<CharacterBase>());
                }
            }

            if (_spornCharacters.Count == 0)
                return;

            var gi = (GameObject)Instantiate(_spornCharacters[(int)Random.Range(0, _spornCharacters.Count)].gameObject);
            gi.transform.parent = transform;
            gi.transform.localPosition = Vector3.zero;
            gi.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}