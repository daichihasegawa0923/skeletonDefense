﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Diamond.SkeletonDefense.Util;

namespace Diamond.SkeletonDefense.Character
{
    public class NormalCharacter : CharacterBase
    {
        /// <summary>
        /// チームID
        /// </summary>
        [SerializeField]
        protected string _teamId;

        /// <summary>
        /// アニメーションコントローラ
        /// </summary>
        [SerializeField]
        protected Animator _animator;

        public override string TeamId { set { this._teamId = value; } get { return this._teamId; } }

        /// <summary>
        /// 敵を認識するためのリスト
        /// </summary>
        [SerializeField]
        private List<CharacterBase> _enemies = new List<CharacterBase>();

        public static readonly string WALK_ANIMATION_TRIGGER = "IsWalking";
        public static readonly string ATTACK_ANIMATION_TRIGGER = "IsAttacking";
        public static readonly string STAY_ANIMATION_TRIGGER = "IsStaying";


        protected virtual void Start()
        {
            this.CharacterStatus = GetComponent<CharacterStatus>();
            this.Rigidbody = GetComponent<Rigidbody>();
            this.Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _enemies = FindObjectsOfType<CharacterBase>().OrderBy(en => Vector3.Distance(en.transform.position, transform.position)).Where(characterBase => characterBase.TeamId != this.TeamId).ToList();

            if(_enemies != null && _enemies.Count() != 0)
            {
                transform.LookAt(_enemies[0].transform.position);
                Debug.Log(_enemies[0].name);
            }
        }

        override protected void Update()
        {
            this.Rigidbody.angularVelocity = Vector3.zero;
            this.ActByBehaviour();

            base.Update();
        }

        public override void AttackAttribute()
        {
            if(_targetEnemy == null || this.IsDead() || !IsNearEnemy())
            {
                StopAllCoroutines();
                this.ChangeBehaviour(CharacterBehaviour.Move);
                return;
            }

            transform.LookAt(_targetEnemy.transform.position);
        }

        public override void Damaged(int damage)
        {
            var damageResult = damage - this.CharacterStatus.Defense;
            if (damageResult < 0)
                damageResult = 0;

            this.CharacterStatus.Hp -= damageResult;
        }

        protected override CharacterBase FindTargetEnemy()
        {
            _enemies = FindObjectsOfType<CharacterBase>().Where(characterBase => characterBase.TeamId != this.TeamId).ToList();

            if (_enemies == null || _enemies.Count == 0)
                return null;

            return _enemies.OrderBy(en => Vector3.Distance(en.transform.position, transform.position)).FirstOrDefault();
        }

        protected override void ActByBehaviour()
        {
            switch(this.CharacterBehaviour)
            {
                case CharacterBehaviour.Stay:
                    this.Stay();
                    break;
                case CharacterBehaviour.Move:

                    var targetEn = this.FindTargetEnemy();
                    if (targetEn == null)
                    {
                        ChangeBehaviour(CharacterBehaviour.Stay);
                        return;
                    }
                    if(_targetEnemy != targetEn)
                        _targetEnemy = targetEn;

                    this.Move();

                    break;
                case CharacterBehaviour.Attack:
                    this.AttackAttribute();
                    break;
                case CharacterBehaviour.Dead:
                    this.Dead();
                    break;
                default:
                    break;
            }
        }

        protected override void Move()
        {
            if (_targetEnemy == null)
            {
                this.ChangeBehaviour(CharacterBehaviour.Stay);
                return;
            }

            if (this.IsDead())
            {
                this.ChangeBehaviour(CharacterBehaviour.Dead);
                return;
            }

            if (this.IsNearEnemy())
            {
                this.ChangeBehaviour(CharacterBehaviour.Attack);
                return;
            }

            transform.LookAt(_targetEnemy.transform.position);
            var velocity = transform.forward * this.CharacterStatus.Speed;
            velocity.y = this.Rigidbody.velocity.y;
            this.Rigidbody.velocity = velocity;
        }

        protected override IEnumerator AttackCoroutine()
        {
            this._animator.SetTrigger(NormalCharacter.STAY_ANIMATION_TRIGGER);
            while (true)
            {
                yield return new WaitForSeconds(this.CharacterStatus.FrequenceOfAttack);
                this.Attack();
            }
        }

        public override bool IsDead()
        {
            return this.CharacterStatus.Hp <= 0;
        }

        protected override void Dead()
        {
            // appear effect when dead
            var deadEffect = (GameObject)Instantiate(_deadEffect.gameObject);
            deadEffect.transform.position = transform.position;
            Destroy(deadEffect, 5.0f);

            // destroy itself
            Destroy(gameObject);
        }

        protected override void Stay()
        {

        }

        public override void ChangeBehaviour(CharacterBehaviour characterBehaviour)
        {
            _animator.ResetAllAnimation();

            var animationSetTriggerAction = new Action<string>((name) => { if (_animator == null || name == null) return; _animator.SetTrigger(name); });

            switch (characterBehaviour)
            {
                case CharacterBehaviour.Stay:
                    animationSetTriggerAction(NormalCharacter.STAY_ANIMATION_TRIGGER);
                    break;
                case CharacterBehaviour.Move:
                    animationSetTriggerAction(NormalCharacter.WALK_ANIMATION_TRIGGER);
                    break;
                case CharacterBehaviour.Attack:
                    StartCoroutine(AttackCoroutine());
                    break;
                case CharacterBehaviour.Dead: 
                    break;
                default:
                    break;
            }

            this.CharacterBehaviour = characterBehaviour;
        }

        public override void Attack()
        {
            if(this.IsNearEnemy())
            {
                this._animator.SetTrigger(NormalCharacter.ATTACK_ANIMATION_TRIGGER);
                this._animator.SetTrigger(NormalCharacter.STAY_ANIMATION_TRIGGER);
                _targetEnemy.Damaged(this.CharacterStatus.Power);
            }
        }
    }
}
