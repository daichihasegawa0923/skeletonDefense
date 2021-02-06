﻿using Diamond.SkeletonDefense.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Diamond.SkeletonDefense.UI 
{
    public class CharacterChooseButton : Button
    {
        [SerializeField]
        public CharacterBase _putCharacter;

        [SerializeField]
        public GameManager _gameManager;

        protected override void Start()
        {
            SetCharacter(_putCharacter);
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
            onClick.RemoveAllListeners();
            onClick.AddListener(() => { _gameManager.SetPutCharacter(_putCharacter);});
        }
    }
}