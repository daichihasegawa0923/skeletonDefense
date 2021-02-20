using System;
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

        public string SetTeamId { set; get; } = "1";

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

        /// <summary>
        /// pivot position of main camera
        /// </summary>
        [SerializeField]
        private Vector3 _firstCameraPosition;

        /// <summary>
        ///  キャラクター追加時のイベントハンドラ
        /// </summary>
        public EventHandler ClickAddHandler;

        /// <summary>
        ///  キャラクター削除時のイベントハンドラ
        /// </summary>
        public EventHandler ClickDeleteHandler;

        /// <summary>
        /// Player can put or delete characters??
        /// </summary>
        public bool CanEditPlayerCharacter { set; get; } = true;

        private void Start()
        {
            this._firstCameraPosition = Camera.main.transform.position;
        }

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
                if (IsTap && CanEditPlayerCharacter)
                {
                    InstantiateCharater();
                    DeleteCharacter();
                }

                _fingerPutTime = 0;
            }

            if (_isFingerPut)
                _fingerPutTime += Time.deltaTime;

            if (IsSlide)
                MoveCameraBySlide();
        }

        private GameObject RaycastAndGetObject(out Vector3 position)
        {
            position = Vector3.zero;
            var mainCamera = Camera.main;
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var rayCast = Physics.Raycast(ray, out raycastHit);

            if (raycastHit.collider == null)
                return null;

            position = raycastHit.point;
            return raycastHit.collider.gameObject;
        }

        public void DeleteCharacter()
        {
            var obj = RaycastAndGetObject(out var position);

            if (obj == null)
                return;

            var characterBase = obj.GetComponent<CharacterBase>();

            if (characterBase == null || characterBase.TeamId != SetTeamId)
                return;

            Destroy(characterBase.gameObject);
            ClickDeleteHandler(characterBase, EventArgs.Empty);
        }

        /// <summary>
        /// キャラクターを配置します。
        /// </summary>
        public void InstantiateCharater()
        {
            var obj = RaycastAndGetObject(out var position);

            if (obj == null || obj.tag != FingerControlManager._canPutCharacterPositionTagName)
                return;

            var charaName = _putCharacter.name;
            var chara = Instantiate(_putCharacter);
            chara.transform.position = position;

            var pc = new GameObject("playerSetInfo");
            var info = pc.AddComponent<SetPlayerCharacterInfo>();
            info.SetPlayerCharacterInfoParams(chara, charaName, chara.transform.position);
            ClickAddHandler(info, EventArgs.Empty);
        }

        public void MoveCameraBySlide()
        {
            var moveDistanceX = Input.GetAxis("Mouse X");
            var moveDistanceY = Input.GetAxis("Mouse Y");

            // calculate distance camera can move
            var fixPosition = new Func<float, float ,float, float>((value, pivot ,max) => 
            {
                if(value > pivot + max)
                {
                    return pivot + max;
                }
                else if(value < pivot - max)
                {
                    return pivot - max;
                }
                return value;
            });

            var position = Camera.main.transform.position;
            
            position.x -= moveDistanceX;
            position.z -= moveDistanceY;

            position.x = fixPosition(position.x,_firstCameraPosition.x, _cameraXMax);
            position.z = fixPosition(position.z,_firstCameraPosition.z, _cameraZMax);
            
            Camera.main.transform.position = position;
        }
    }
}