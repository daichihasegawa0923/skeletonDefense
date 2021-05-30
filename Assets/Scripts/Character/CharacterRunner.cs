using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Diamond.SkeletonDefense.Character
{
    public class CharacterRunner :NormalCharacter
    {
        public override void AttackAttribute()
        {
            _targetEnemy = null;
            ChangeBehaviour(CharacterBehaviour.Move);
        }

        protected override CharacterBase FindTargetEnemy()
        {
            if (_targetEnemy)
                return _targetEnemy;

            var enemies = FindObjectsOfType<CharacterBase>().Where(characterBase => characterBase.TeamId != this.TeamId).ToList();
            if (enemies == null || enemies.Count == 0)
                return null;

            return enemies.OrderByDescending(en => Vector3.Distance(en.transform.position, transform.position)).FirstOrDefault();
        }
    }
}