using System;
using UnityEngine;

namespace Diamond.SkeletonDefense.Character
{
    /// <summary>
    /// プレイヤーのキャラクターの位置などの情報のセット
    /// </summary>
    public class SetPlayerCharacterInfo:MonoBehaviour
    {
        /// <summary>
        ///  インスタンス
        /// </summary>
        public CharacterBase CharacterBase { set; get; }

        /// <summary>
        /// キャラクター名（リソースから取得するため）
        /// </summary>
        [SerializeField]
        private string _characterName;

        /// <summary>
        /// 場所
        /// </summary>
        [SerializeField]
        private Vector3 _characterPosition;

        /// <summary>
        /// インスタンス
        /// </summary>
        /// <param name="characterName"></param>
        /// <param name="characterPosition"></param>
        public void SetPlayerCharacterInfoParams( CharacterBase characterBase,string characterName, Vector3 characterPosition)
        {
            CharacterBase = characterBase;
            _characterName = characterName;
            _characterPosition = characterPosition;
        }
        /// <summary>
        /// セット位置に戻る
        /// </summary>
        public void Reset()
        {
            if (CharacterBase != null)
                Destroy(CharacterBase.gameObject);

            var cls = ((GameObject)Resources.Load(this._characterName));
            var instance = Instantiate(cls);
            CharacterBase = instance.GetComponent<CharacterBase>();
            CharacterBase.gameObject.transform.position = this._characterPosition;
        }
    }
}