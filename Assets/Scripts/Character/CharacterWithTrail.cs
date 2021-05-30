using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.SkeletonDefense.Character
{
    public class CharacterWithTrail : NormalCharacter
    {
        [SerializeField]
        protected TrailRenderer _trailRenderer;

        public override void ChangeBehaviour(CharacterBehaviour characterBehaviour)
        {
            switch(characterBehaviour)
            {
                case CharacterBehaviour.Attack:
                    _trailRenderer.gameObject.SetActive(true);
                    break;
                default:
                    _trailRenderer.gameObject.SetActive(false);
                    break;
            }

            base.ChangeBehaviour(characterBehaviour);
        }
    }
}