﻿using System;
using System.Collections;
using System.Collections.Generic;
using Diamond.SkeletonDefense.Character;
using UnityEngine;

namespace Diamond.SkeletonDefense
{
    /// <summary>
    /// 指による操作を管理するクラス
    /// </summary>
    public class FingerControlManager : MonoBehaviour
    {
        /// <summary>
        /// キャラクターが配置できる箇所のタグ名
        /// </summary>
        public readonly static string _canPutCharacterPositionTagName = "CharacterPutable";

        /// <summary>
        /// 指を置いている時間
        /// </summary>
        [SerializeField]
        private float _fingerPutTime = 0;

        /// <summary>
        /// 指を置いているか否か
        /// </summary>
        [SerializeField]
        private bool _isFingerPut = false;

        public CharacterBase PutCharacter { set { _putCharacter = value; } get { return this._putCharacter; } }

        /// <summary>
        /// 配置するキャラクター
        /// </summary>
        [SerializeField]
        private CharacterBase _putCharacter;

        /// <summary>
        /// タップ化スライドかを判定する時間の分け目
        /// </summary>
        [SerializeField]
        private float _fingerTimeSeparate = 0.25f;

        /// <summary>
        /// タップしたか
        /// </summary>
        public bool IsTap { get { return _fingerPutTime > 0 && _fingerPutTime < _fingerTimeSeparate; } }

        /// <summary>
        /// スライド中か
        /// </summary>
        public bool IsSlide { get { return (_fingerPutTime > 0 && _fingerPutTime >= _fingerTimeSeparate) && _isFingerPut; } }

        /// <summary>
        /// X方向にカメラが動く最大値/マイナスの最低値
        /// </summary>
        [SerializeField]
        private float _cameraXMax = 50;

        /// <summary>
        /// Z方向にカメラが動く最大値/マイナスの最低値
        /// </summary>
        [SerializeField]
        private float _cameraZMax = 25;

        // Update is called once per frame
        void Update()
        {
            ControlByFinger();
        }

        private void ControlByFinger()
        {
            if(Input.GetMouseButtonDown(0))
            {
                _isFingerPut = true;
            }

            if(Input.GetMouseButtonUp(0))
            {
                _isFingerPut = false;
                if (IsTap)
                    InstantiateCharater();

                _fingerPutTime = 0;
            }

            if (_isFingerPut)
                _fingerPutTime += Time.deltaTime;

            if (IsSlide)
                MoveCameraBySlide();
        }

        /// <summary>
        /// キャラクターを配置します。
        /// </summary>
        public void InstantiateCharater()
        {
            var mainCamera = Camera.main;
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var rayCast = Physics.Raycast(ray,out raycastHit);

            if (raycastHit.collider == null ||
                raycastHit.collider.gameObject.tag != FingerControlManager._canPutCharacterPositionTagName
                || this._putCharacter == null)
                return;

            var position = raycastHit.point;
            var chara = Instantiate(_putCharacter);
            chara.transform.position = position;
        }

        public void MoveCameraBySlide()
        {
            var moveDistanceX = Input.GetAxis("Mouse X");
            var moveDistanceY = Input.GetAxis("Mouse Y");

            Func<float, float, float> fixPosition = new Func<float, float, float>((value, max) => 
            {
                if(value > max)
                {
                    return max;
                }else if(value < -max)
                {
                    return -max;
                }
                return value;
            });

            var position = Camera.main.transform.position;
            
            position.x -= moveDistanceX;
            position.z -= moveDistanceY;

            position.x = fixPosition(position.x, _cameraXMax);
            position.z = fixPosition(position.z, _cameraZMax);
            
            Camera.main.transform.position = position;
        }
    }
}