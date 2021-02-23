using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.SkeletonDefense.Character
{
    public class CharacterShooter:NormalCharacter
    {
        /// <summary>
        ///  Burret
        /// </summary>
        [SerializeField]
        private GameObject _burret;

        [SerializeField]
        private float _burretSpeed;

        [SerializeField]
        private Transform _burretFromPosition;

        public override void Attack()
        {
            if(this.IsNearEnemy())
            {
                this._animator.SetTrigger(NormalCharacter.ATTACK_ANIMATION_TRIGGER);
                this._animator.SetTrigger(NormalCharacter.STAY_ANIMATION_TRIGGER);
            }
        }

        /// <summary>
        /// Act in animation
        /// </summary>
        public virtual void Shoot()
        {
            if (!_burret)
                return;

            var burretInstance = Instantiate(_burret).gameObject;
            burretInstance.transform.position = _burretFromPosition.position;
            var rigidbody = burretInstance.GetComponent<Rigidbody>();

            if (_targetEnemy)
            {
                rigidbody.velocity = (_targetEnemy.transform.position - transform.position) * _burretSpeed;
                burretInstance.transform.LookAt(_targetEnemy.transform.position);
            }

            Destroy(burretInstance, 20.0f);
        }
    }
}