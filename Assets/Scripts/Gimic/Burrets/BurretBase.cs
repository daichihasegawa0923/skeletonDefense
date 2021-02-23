using System.Collections;
using System.Collections.Generic;
using Diamond.SkeletonDefense.Character;
using UnityEngine;

namespace Diamond.SkeletonDefense.Gimic.Burrets
{
    public class BurretBase : MonoBehaviour
    {
        [SerializeField]
        protected int _power = 30;

        protected virtual void OnTriggerEnter(Collider collider)
        {
            var character = collider.gameObject.GetComponent<CharacterBase>();
            if (!character)
                return;

            this.ReachAction(character);
        }

        protected virtual void ReachAction(CharacterBase character)
        {
            character.Damaged(_power);
            transform.parent = character.transform;
            Destroy(GetComponent<Rigidbody>());
            Destroy(this);
        }
    }
}
