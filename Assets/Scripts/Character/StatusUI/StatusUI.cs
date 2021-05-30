using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Diamond.SkeletonDefense.Character.Status
{
    public class StatusUI : MonoBehaviour
    {
        [SerializeField]
        private Slider _hp_slider;

        [SerializeField]
        private TextMeshProUGUI _damage_textMeshProUGUI;

        private Tween _tween;

        private Vector3 _textMeshLocalFromPosition;

        private void Start()
        {
            _textMeshLocalFromPosition = transform.localPosition;
        }

        public void SetHpMaxValue(int hp)
        {
            this._hp_slider.maxValue = hp;
            this._hp_slider.value = hp;
            _hp_slider.gameObject.SetActive(false);
        }

        public void DamagedDisplay(int damage)
        {
            _hp_slider.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(DamageEffect(2.0f));
            _damage_textMeshProUGUI.text = ("-" + damage);
            this._hp_slider.value -= damage;
        }

        IEnumerator DamageEffect(float time)
        {
            _damage_textMeshProUGUI.gameObject.SetActive(true);
            _tween = DOTween.Sequence()
                .Append(_damage_textMeshProUGUI.transform.DOMove(transform.position, 0))
                .Join(_damage_textMeshProUGUI.transform.DOMove(transform.position + Vector3.up, time));
            transform.localPosition = _textMeshLocalFromPosition;
            yield return new WaitForSeconds(time);
            _damage_textMeshProUGUI.gameObject.SetActive(false);
        }

        public void Update()
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }
}