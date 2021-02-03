using System.Collections;
using System.Collections.Generic;
using Diamond.SkeletonDefense.Character;
using UnityEngine;

namespace Diamond.SkeletonDefense
{
    /// <summary>
    /// 指による操作を管理するクラス
    /// </summary>
    public class FingerControlManager : MonoBehaviour
    {
        /// <summary>
        /// キャラクターが配置できる箇所
        /// </summary>
        public readonly static string _canPutCharacterPositionTagName = "CharacterPutable";

        /// <summary>
        /// 指を置いている時間
        /// </summary>
        [SerializeField]
        private float _fingerPutTime = 0;

        /// <summary>
        /// 指を置いているか否か
        /// </summary>
        [SerializeField]
        private bool _isFingerPut = false;

        /// <summary>
        /// 配置するキャラクター
        /// </summary>
        [SerializeField]
        private CharacterBase _putCharacter;

        // Update is called once per frame
        void Update()
        {

        }

        private void PutFinger()
        {
            if(Input.GetMouseButtonDown(0))
            {
                _isFingerPut = true;
            }

            if(Input.GetMouseButtonUp(0))
            {
                _isFingerPut = false;
                _fingerPutTime = 0;
            }
        }
    }
}