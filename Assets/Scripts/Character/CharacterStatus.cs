using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Diamond.SkeletonDefense.Character.Status;

namespace Diamond.SkeletonDefense.Character
{
    public class CharacterStatus : MonoBehaviour
    {
        [SerializeField]
        /// <summary>
        /// 体力
        /// </summary>
        private int hp;

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Power;

        /// <summary>
        /// 移動スピード
        /// </summary>
        public float Speed;

        /// <summary>
        /// 守備力
        /// </summary>
        public int Defense;

        /// <summary>
        /// 敵キャラクターとの距離
        /// </summary>
        public float DistanseBetweenEnemy;

        /// <summary>
        /// 攻撃頻度（低いほど攻撃頻度が高くなる）
        /// </summary>
        public float FrequenceOfAttack;

        /// <summary>
        /// コスト
        /// </summary>
        public int cost;

        [SerializeField]
        private StatusUI _statusUI;

        private void Start()
        {
            var ui = Resources.Load("CharacterStatusUI");
            this._statusUI = ((GameObject)Instantiate(ui)).GetComponent<StatusUI>();
            this._statusUI.SetHpMaxValue(this.Hp);
            this._statusUI.transform.parent = transform;
            this._statusUI.transform.localPosition = Vector3.zero + (Vector3.up * 2);
        }

        public int Hp 
        { 
            get => hp; 
            set
                {
                if (value < hp)
                    _statusUI.DamagedDisplay(hp - value);
                hp = value; 
            }
        }
    }
}