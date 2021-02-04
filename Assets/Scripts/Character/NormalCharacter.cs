using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        public override string TeamId => this._teamId;

        /// <summary>
        /// 攻撃対象の敵
        /// </summary>
        protected CharacterBase _targetEnemy;

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
            _enemies = FindObjectsOfType<CharacterBase>().Where(characterBase => characterBase.TeamId != this.TeamId).ToList();
        }

        protected virtual void Update()
        {
            this.ActByBehaviour();
        }

        public override void Attack()
        {

            if(_targetEnemy == null || this.IsDead())
            {
                StopCoroutine("AttackCoroutine");
                this.ChangeBehaviour(CharacterBehaviour.Move);
                return;
            }

            transform.LookAt(_targetEnemy.transform.position);
        }

        public override void Damaged(int damage)
        {
            var damageResult = damage - this.CharacterStatus.Defense;
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
                        this.CharacterBehaviour = CharacterBehaviour.Stay;
                        return;
                    }
                    if(_targetEnemy != targetEn)
                        _targetEnemy = targetEn;

                    this.Move();

                    break;
                case CharacterBehaviour.Attack:
                    this.Attack();
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

            if (Vector3.Distance(transform.position, _targetEnemy.transform.position) <= this.CharacterStatus.DistanseBetweenEnemy)
            {
                this.ChangeBehaviour(CharacterBehaviour.Attack);
                return;
            }

            transform.LookAt(_targetEnemy.transform.position);
            this.Rigidbody.velocity = transform.forward * this.CharacterStatus.Speed;
        }

        protected override IEnumerator AttackCoroutine()
        {
            while(true)
            {
                _targetEnemy.Damaged(this.CharacterStatus.Power);
                yield return new WaitForSeconds(this.CharacterStatus.FrequenceOfAttack);
            }
        }

        public override bool IsDead()
        {
            return this.CharacterStatus.Hp <= 0;
        }

        protected override void Dead()
        {
            Destroy(gameObject);
        }

        protected override void Stay()
        {

        }

        public override void ChangeBehaviour(CharacterBehaviour characterBehaviour)
        {
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
                    StartCoroutine("AttackCoroutine");
                    animationSetTriggerAction(NormalCharacter.ATTACK_ANIMATION_TRIGGER);
                    break;
                case CharacterBehaviour.Dead:
                    break;
                default:
                    break;
            }

            this.CharacterBehaviour = characterBehaviour;
        }
    }
}
