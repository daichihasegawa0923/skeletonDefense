﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.SkeletonDefense.Character
{
    /// <summary>
    /// バトルフェーズで動きキャラクターの基底クラス
    /// </summary>
    [RequireComponent(typeof(CharacterStatus))]
    public abstract class CharacterBase : MonoBehaviour
    {
        /// <summary>
        /// キャラクターの状態
        /// </summary>
        public CharacterBehaviour CharacterBehaviour { set; get; }

        /// <summary>
        /// キャラクターのステータス
        /// </summary>
        public CharacterStatus CharacterStatus { set; get; }

        /// <summary>
        /// チームID
        /// </summary>
        public virtual string TeamId { get; }

        /// <summary>
        /// 攻撃を受けたときの挙動
        /// </summary>
        public abstract void Damaged(int damage);

        /// <summary>
        /// 待機中
        /// </summary>
        protected abstract void Stay();

        /// <summary>
        /// 攻撃
        /// </summary>
        public abstract void Attack();

        /// <summary>
        /// 索敵
        /// </summary>
        protected abstract CharacterBase FindTargetEnemy();

        /// <summary>
        /// 移動する
        /// </summary>
        protected abstract void Move();

        /// <summary>
        /// 攻撃時の非同期処理
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator AttackCoroutine();

        /// <summary>
        /// 死亡判定
        /// </summary>
        /// <returns>死んでいるか否か</returns>
        public abstract bool IsDead();

        /// <summary>
        /// 行動に変化をつける（Updateメソッドで回す）
        /// </summary>
        protected abstract void ActByBehaviour();

        /// <summary>
        /// 死亡時の挙動
        /// </summary>
        protected abstract void Dead();

        /// <summary>
        /// キャラクターの状態を変えます。
        /// </summary>
        /// <param name="characterBehaviour"></param>
        public abstract void ChangeBehaviour(CharacterBehaviour characterBehaviour);
    }

    /// <summary>
    /// キャラクターの状態の定義
    /// </summary>
    public enum CharacterBehaviour
    {
        /// <summary>
        /// 待機中
        /// </summary>
        Stay,

        /// <summary>
        /// 移動中
        /// </summary>
        Move,

        /// <summary>
        /// 攻撃中
        /// </summary>
        Attack,

        /// <summary>
        /// 被ダメージ中
        /// </summary>
        Damaged,

        /// <summary>
        /// 死亡
        /// </summary>
        Dead,
    }
}