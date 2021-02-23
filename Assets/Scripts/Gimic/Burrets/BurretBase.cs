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

        [SerializeField]
        protected Transform _rayPositionBase;

        protected virtual CharacterBase IsReached(out bool isReached)
        {
            isReached = false;

            var ray = new Ray(_rayPositionBase.transform.position, transform.forward);
            Physics.Raycast(ray, out var hit, 0.25f);
            if (!hit.collider)
                return null;

            var character = hit.collider.gameObject.GetComponent<CharacterBase>();
            if (!character)
                return null;

            isReached = true;
            return character;
        }

        protected virtual void ReachAction()
        {
            var character = IsReached(out var isReached);
            if (!isReached)
                return;

            character.Damaged(_power);
            transform.parent = character.transform;
            Destroy(GetComponent<Rigidbody>());
            Destroy(this);
        }

        protected virtual void Update()
        {
            this.ReachAction();
        }
    }
}
