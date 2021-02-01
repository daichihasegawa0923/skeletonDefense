using Diamond.SkeletonDefense.Character;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Diamond.SkeletonDefense
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        List<CharacterBase> _characterBases;
        // Start is called before the first frame update
        void Start()
        {
            _characterBases = FindObjectsOfType<CharacterBase>().ToList();
            _characterBases.ForEach(c => c.ChangeBehaviour(CharacterBehaviour.Move));

            StartCoroutine("GetCharacters");
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// キャラクターリストを更新する
        /// </summary>
        /// <returns></returns>
        private IEnumerator GetCharacters()
        {
            while(true)
            {
                this._characterBases = FindObjectsOfType<CharacterBase>().ToList();
                yield return new WaitForSeconds(2.0f);
            }
        }
    }
}