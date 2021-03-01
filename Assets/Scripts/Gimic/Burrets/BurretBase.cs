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
        protected GameObject _particleSystems;

        /// <summary>
        /// ê⁄ìGÇµÇΩéûÇ…å¯â Ç™è¡Ç¶ÇÈÇ©
        /// </summary>
        [SerializeField]
        private bool _isAttachWithEnemy = true;

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
            if (!_isAttachWithEnemy)
                return;

            transform.parent = character.transform;
            Destroy(GetComponent<Rigidbody>());
            if(_particleSystems)
            {
                _particleSystems.transform.parent = null;
                Destroy(_particleSystems, 5.0f);
            }
            Destroy(this);
        }
    }
}
