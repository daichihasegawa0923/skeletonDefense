using System.Collections;
using System.Collections.Generic;
using Diamond.SkeletonDefense.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Diamond.SkeletonDefense.UI
{
    /// <summary>
    /// キャラクターの情報を表示するためのUI
    /// </summary>
    public class CharacterStatusInfomationUI : MonoBehaviour
    {
        /// <summary>
        /// HP表示用のテキスト
        /// </summary>
        [SerializeField]
        private Text _hpText;

        /// <summary>
        /// 攻撃力表示用のテキスト
        /// </summary>
        [SerializeField]
        private Text _powerText;

        /// <summary>
        /// 移動速度表示用のテキスト
        /// </summary>
        [SerializeField]
        private Text _speedText;

        /// <summary>
        /// キャラクター名を表示するためのテキスト
        /// </summary>
        [SerializeField]
        private Text _nameText;

        /// <summary>
        /// キャラクターの説明を表示するためのテキスト
        /// </summary>
        [SerializeField]
        private Text _descriptionText;

        /// <summary>
        /// キャラクターの情報をUIに表示する
        /// </summary>
        /// <param name="characterBase">情報を表示するキャラクター</param>
        public void SetCharacter(CharacterBase characterBase)
        {
            var characterStatus = characterBase.CharacterStatus;

            _hpText.text = characterStatus.Hp.ToString();
            _powerText.text = characterStatus.Power.ToString();
            _speedText.text = (characterStatus.Speed*100).ToString();
            _nameText.text = characterBase.Name;
            _descriptionText.text = characterBase.Description;
        }
    }
}