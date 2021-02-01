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

        public override string TeamId => this._teamId;

        /// <summary>
        /// 攻撃対象の敵
        /// </summary>
        protected CharacterBase _targetEnemy;

        /// <summary>
        /// 移動速度
        /// </summary>
        [SerializeField]
        protected float _speed;

        /// <summary>
        /// 敵キャラクターと対峙したときに止まる位置
        /// </summary>
        [SerializeField]
        protected float _stopDistance;

        /// <summary>
        /// 敵を認識するためのリスト
        /// </summary>
        [SerializeField]
        private List<CharacterBase> _enemies = new List<CharacterBase>();

        protected virtual void Start()
        {
            this.CharacterStatus = GetComponent<CharacterStatus>();
            _enemies = FindObjectsOfType<CharacterBase>().Where(characterBase => characterBase.TeamId != this.TeamId).ToList();
        }

        protected virtual void Update()
        {
            this.ChangeBehaviourByFaze();
        }

        public override void Attack()
        {
            if(this.CharacterBehaviour != CharacterBehaviour.Attack)
            {
                this.CharacterBehaviour = CharacterBehaviour.Attack;
                StartCoroutine("AttackCoroutine");
            }

            if(_targetEnemy == null || this.IsDead())
            {
                StopCoroutine("AttackCoroutine");
                this.CharacterBehaviour = CharacterBehaviour.Move;
            }
        }

        public override void Damaged(int damage)
        {
            var damageResult = damage - this.CharacterStatus.Defense;
            this.CharacterStatus.Hp -= damageResult;
        }

        protected override CharacterBase FindTargetEnemy()
        {
            if (_enemies == null || _enemies.Count == 0)
                return null;

            return _enemies.OrderBy(en => Vector3.Distance(en.transform.position, transform.position)).FirstOrDefault();
        }

        protected override void ChangeBehaviourByFaze()
        {
            switch(this.CharacterBehaviour)
            {
                case CharacterBehaviour.Move:

                    var targetEn = this.FindTargetEnemy();
                    if (targetEn == null)
                        return;
                    if(_targetEnemy != targetEn)
                        _targetEnemy = targetEn;

                    this.Move();

                    break;
                case CharacterBehaviour.Attack:
                    this.Attack();
                    break;
                case CharacterBehaviour.Dead:
                    break;
                default:
                    break;
            }
        }

        protected override void Move()
        {
            if (_targetEnemy == null)
                return;

            if (this.IsDead())
            {
                this.CharacterBehaviour = CharacterBehaviour.Dead;
                return;
            }

            if (Vector3.Distance(transform.position, _targetEnemy.transform.position) <= _stopDistance)
                this.Attack();

            transform.LookAt(_targetEnemy.transform.position);
            transform.position += transform.forward * _speed;
        }

        protected override IEnumerator AttackCoroutine()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsDead()
        {
            return this.CharacterStatus.Hp <= 0;
        }

        protected override void Dead()
        {

        }
    }
}
