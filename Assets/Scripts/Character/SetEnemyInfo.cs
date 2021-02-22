using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.SkeletonDefense.Character
{
    public class SetEnemyInfo : MonoBehaviour
    {
        /// <summary>
        /// 敵のプレハブ
        /// </summary>
        [SerializeField]
        private CharacterBase _enemyClass;

        /// <summary>
        /// 敵のインスタンス（シーン上に存在）
        /// </summary>
        private CharacterBase _enemyInstance;

        public void SetEnemy(string id)
        {
            if (_enemyInstance == null)
                _enemyInstance = Instantiate(_enemyClass).GetComponent<CharacterBase>();

            _enemyInstance.TeamId = id;
            _enemyInstance.transform.position = transform.position;
        }

        public void DeleteEnemy()
        {
            if (_enemyInstance == null)
                return;

            Destroy(_enemyInstance.gameObject);
            // 念の為null割り当て
            _enemyInstance = null;
        }
    }
}