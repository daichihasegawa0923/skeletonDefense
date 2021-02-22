using Diamond.SkeletonDefense.Character;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Diamond.SkeletonDefense.UI 
{
    public class CharacterChooseButton : MonoBehaviour
    {
        [SerializeField]
        public CharacterBase _putCharacter;

        [SerializeField]
        public GameManager _gameManager;

        [SerializeField]
        public Button _button;

        protected void Start()
        {
            SetCharacter(_putCharacter);
            var costText = transform.GetComponentInChildren<TextMeshProUGUI>();
            costText.text = _putCharacter.CharacterStatus.cost.ToString();
        }

        /// <summary>
        /// ゲーム管理オブジェクトをセットする
        /// </summary>
        /// <param name="gameManager"></param>
        public void SetGameManager(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        /// <summary>
        /// ボタンに配置するキャラクターを設定する
        /// </summary>
        /// <param name="characterBase"></param>
        public void SetCharacter(CharacterBase characterBase)
        {
            _putCharacter = characterBase;
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => { _gameManager.SetPutCharacter(_putCharacter);});
        }
    }
}