using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

namespace Diamond.SkeletonDefense.Util
{
    public class TweenAnimationUIMove : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _differPosition;

        [SerializeField]
        private float _duration;

        [SerializeField]
        private bool _startOnAwake;

        [SerializeField]
        private bool _isEnableStart = false;

        [SerializeField]
        private Vector3 _endPosition;

        [SerializeField]
        private TweenAnimationUIMove _afterAnimation;

        private void Start()
        {

            _endPosition = transform.position;
            transform.position += _differPosition;

            if (!_startOnAwake)
                return;

            AnimationByTransform();
        }

        private void OnEnable()
        {
            if (!_isEnableStart)
                return;

            transform.position += _differPosition;

            AnimationByTransform();
        }

        public void AnimationByTransform(Sequence sequence = null)
        {
            if (sequence == null)
                sequence = DOTween.Sequence();

            sequence.Append(transform.DOMove(_endPosition, _duration));
            if(_afterAnimation)
            {
                _afterAnimation.AnimationByTransform(sequence);
            }
            else
            {
                sequence.Play();
            }
        }
    }
}