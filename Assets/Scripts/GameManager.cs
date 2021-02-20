using Diamond.SkeletonDefense.Character;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Diamond.SkeletonDefense.Data;
using System;
using UnityEngine.SceneManagement;
using Diamond.SkeletonDefense.UI;

namespace Diamond.SkeletonDefense
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// 盤面のキャラクターたち
        /// </summary>
        [SerializeField]
        List<CharacterBase> _characterBases;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private BattleFazeUIManager _battleFazeUIManager;

        /// <summary>
        /// 指での操作
        /// </summary>
        [SerializeField]
        private FingerControlManager _fingerControlManager;

        /// <summary>
        ///  the team id of player.
        /// </summary>
        [SerializeField]
        private string _playerTeamId = "1";

        /// <summary>
        /// ゲームの進行状況
        /// </summary>
        public static GameFaze GameFaze { private set; get; } = GameFaze.PrepareForFighting;

        /// <summary>
        ///  プレイヤー側のキャラクターたち
        /// </summary>
        private List<SetPlayerCharacterInfo> _setPlayerCharacterInfos = new List<SetPlayerCharacterInfo>();

        /// <summary>
        /// 制限コスト
        /// </summary>
        [SerializeField]
        private int _maxCost = 10;

        /// <summary>
        /// クリアするとリリースされるキャラクター
        /// </summary>
        [SerializeField]
        private CharacterBase _releaseCharacter;

        /// <summary>
        ///  Scene is released when clear current stage.
        /// </summary>
        [SerializeField]
        private Scene _releaseScene;

        /// <summary>
        /// Places player can put characters
        /// </summary>
        [SerializeField]
        private List<GameObject> _putables;

        /// <summary>
        ///  現在配置されているキャラクターの総コスト
        /// </summary>
        public int CurrentCost
        { get
            {
                var cost = 0;
                foreach(var info in _setPlayerCharacterInfos)
                {
                    if(info != null
                        && info.CharacterBase != null
                        && info.CharacterBase.CharacterStatus != null)
                        cost += info.CharacterBase.CharacterStatus.cost;
                }
                Debug.Log(cost);
                return cost;
            }
        }

        private void FixedUpdate()
        {
        }

        /// <summary>
        ///  敵の配置情報
        /// </summary>
        [SerializeField]
        private List<SetEnemyInfo> _setEnemyInfos = new List<SetEnemyInfo>();

        private void Awake()
        {
            ChangeGameFaze(GameFaze.PrepareForFighting);
        }

        /// <summary>
        ///   タップ時にプレイヤーのキャラクターのリストを更新する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void AddPlayerCharacter(object sender, EventArgs args)
        {
            var charaInfo = sender as SetPlayerCharacterInfo;
            if (charaInfo == null)
                return;

            _setPlayerCharacterInfos.Add(charaInfo);
            charaInfo.transform.parent = transform;

            // 
            if(CurrentCost > _maxCost)
            {
                _setPlayerCharacterInfos.Remove(charaInfo);
                Destroy(charaInfo.CharacterBase.gameObject);
                Destroy(charaInfo.gameObject);
            }

            this._battleFazeUIManager.SetCurrentCostCountText(CurrentCost);
        }

        private void DeletePlayerCharacter(object sender,EventArgs args)
        {
            var character = sender as CharacterBase;
            if (character == null)
                return;
            if (_setPlayerCharacterInfos.Count() == 0)
                return;

            var deleteCharaInfo = _setPlayerCharacterInfos.Where(ci => ci.CharacterBase == character).ToList();

            if (deleteCharaInfo.Count() == 0)
                return;

            Destroy(deleteCharaInfo[0].CharacterBase.gameObject);
            _setPlayerCharacterInfos.Remove(deleteCharaInfo[0]);
            Destroy(deleteCharaInfo[0].gameObject);

            this._battleFazeUIManager.SetCurrentCostCountText(CurrentCost);
        }

        // Start is called before the first frame update
        void Start()
        {
            _fingerControlManager.SetTeamId = this._playerTeamId;
            _fingerControlManager.ClickAddHandler += new EventHandler(this.AddPlayerCharacter);
            _fingerControlManager.ClickDeleteHandler += new EventHandler(this.DeletePlayerCharacter);
            _battleFazeUIManager.SetMaxCostCountText(this._maxCost);
        }

        // Update is called once per frame
        void Update()
        {
            switch(GameManager.GameFaze)
            {
                case GameFaze.PrepareForFighting:
                    break;
                case GameFaze.InBattle:
                    JudgeGameIsEnd();
                    break;
                case GameFaze.Result:
                    break;
                default:
                    break;
            }
        }

        private void JudgeGameIsEnd()
        {
            if (this.IsGameEnd())
            {
                if (this.IsPlayerWin())
                {

                    if (this._releaseCharacter)
                    {
                        var data = SaveData.Load();

                        if (data.ReleasedCharacterNames == null)
                            data.ReleasedCharacterNames = new List<string>();

                        if (data.ReleasedCharacterNames.Where(n => n == _releaseCharacter.name).ToList().Count == 0)
                            data.ReleasedCharacterNames.Add(this._releaseCharacter.name);

                        if (data.ReleasedStageNames == null)
                            data.ReleasedStageNames = new List<string>();

                        if (!data.ReleasedStageNames.Contains(_releaseScene.name))
                            data.ReleasedStageNames.Add(_releaseScene.name);

                        SaveData.Save(data);
                    }
                }
                else
                {

                }

                ChangeGameFaze(GameFaze.Result);
            }
        }

        private bool IsGameEnd()
        {
            var characters = FindObjectsOfType<CharacterBase>();
            var otherId = characters[0].TeamId;
            var playerCharacters = characters.Where(ch => ch.TeamId == otherId);

            return characters.Count() == playerCharacters.Count();
        }

        public bool IsPlayerWin()
        {
            var characters = FindObjectsOfType<CharacterBase>();
            foreach (var character in characters)
            {
                if (character.TeamId != this._playerTeamId)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// プレイヤーが設定したキャラクターをクリアする
        /// </summary>
        public void Clear()
        {
            this._setPlayerCharacterInfos.ForEach(info =>
            {
                Destroy(info.CharacterBase.gameObject);
                Destroy(info.gameObject);
            });

            this._setPlayerCharacterInfos.Clear();
            this._battleFazeUIManager.SetCurrentCostCountText(CurrentCost);
        }

        /// <summary>
        /// 配置するキャラクターを決定する
        /// </summary>
        /// <param name="characterBase">配置するキャラクター</param>
        public void SetPutCharacter(CharacterBase characterBase)
        {
            _fingerControlManager.PutCharacter = characterBase;

        }

        /// <summary>
        ///  start the battle
        /// </summary>
        public void GameStart()
        {
            if (this._setPlayerCharacterInfos.Count == 0)
                return;

            ChangeGameFaze(GameFaze.InBattle);
        }

        public void ResetEnemySet()
        {
            foreach(var ene in _setEnemyInfos)
            {
                ene.DeleteEnemy();
                ene.SetEnemy();
            }
        }

        public void ResetPlayerSet()
        {
            foreach(var pc in _setPlayerCharacterInfos)
            {
                pc.Reset();
            }
        }

         /// <summary>
         /// 準備中に戻す、ボタンから呼び出す想定
         /// </summary>
        public void RePrepareGame()
        {
            this.ChangeGameFaze(GameFaze.PrepareForFighting);
        }

        /// <summary>
        /// ゲームのフェーズを変える
        /// </summary>
        /// <param name="gameFaze"></param>
        public void ChangeGameFaze(GameFaze gameFaze)
        {
            switch(gameFaze)
            {
                case GameFaze.PrepareForFighting:
                    _fingerControlManager.CanEditPlayerCharacter = true;
                    _battleFazeUIManager.SetPrepareUI();
                    ResetEnemySet();
                    ResetPlayerSet();
                    _putables.ForEach(p => p.SetActive(true));
                    break;

                case GameFaze.InBattle:
                    _characterBases = FindObjectsOfType<CharacterBase>().ToList();
                    _characterBases.ForEach(c => c.ChangeBehaviour(CharacterBehaviour.Move));
                    _battleFazeUIManager.SetBattleUI();
                    _fingerControlManager.CanEditPlayerCharacter= false;
                    _putables.ForEach(p => p.SetActive(false));
                    break;

                case GameFaze.Result:
                    _fingerControlManager.CanEditPlayerCharacter = false;
                    _putables.ForEach(p => p.SetActive(false));
                    _battleFazeUIManager.SetResultUI(IsPlayerWin());
                    break;
            }

            GameManager.GameFaze = gameFaze;
        }
    }

    /// <summary>
    /// ゲームの進行状況を表します。
    /// </summary>
    public enum GameFaze
    {
        /// <summary>
        /// 戦闘準備中
        /// </summary>
        PrepareForFighting,

        /// <summary>
        /// 戦闘中
        /// </summary>
        InBattle,

        /// <summary>
        /// 試合結果
        /// </summary>
        Result
    }
}