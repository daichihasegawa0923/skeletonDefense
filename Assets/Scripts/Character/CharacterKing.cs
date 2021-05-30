using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.SkeletonDefense.Character
{
    public class CharacterKing:CharacterShooter
    {
        /// <summary>
        /// Act in animation
        /// </summary>
        public override void Shoot()
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

            var chara = burretInstance.GetComponent<CharacterBase>();
            chara.ChangeBehaviour(CharacterBehaviour.Move);
        }
    }
}