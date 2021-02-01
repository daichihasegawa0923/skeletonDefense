using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.SkeletonDefense.Character
{
    public class CharacterStatus : MonoBehaviour
    {
        /// <summary>
        /// 体力
        /// </summary>
        public int Hp;

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
    }
}