using Diamond.SkeletonDefense.Character;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Diamond.SkeletonDefense.Data;
using System;

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
        [SerializeField]
        private List<CharacterBase> _playerCharacterBases;

        private void Awake()
        {
            GameManager.GameFaze = GameFaze.PrepareForFighting;
        }

        /// <summary>
        ///   タップ時にプレイヤーのキャラクターのリストを更新する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CheckPlayerCharacterBases(object sender, EventArgs args)
        {
            _playerCharacterBases.Clear();
            var list = FindObjectsOfType<CharacterBase>().Where(cb => cb.TeamId == this._playerTeamId).ToList();
            if (list.Count() == 0)
                return;

            _playerCharacterBases = list;
        }

        // Start is called before the first frame update
        void Start()
        {
            _fingerControlManager.SetTeamId = this._playerTeamId;
            _fingerControlManager.ClickHandler += new EventHandler(this.CheckPlayerCharacterBases);
        }

        // Update is called once per frame
        void Update()
        {

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
        /// 配置するキャラクターを決定する
        /// </summary>
        /// <param name="characterBase">配置するキャラクター</param>
        public void SetPutCharacter(CharacterBase characterBase)
        {
            _fingerControlManager.PutCharacter = characterBase;
        }

        public void GameStart()
        {
            _characterBases = FindObjectsOfType<CharacterBase>().ToList();
            _characterBases.ForEach(c => c.ChangeBehaviour(CharacterBehaviour.Move));
            GameFaze = GameFaze.InBattle;
            _battleFazeUIManager.SetBattleUI();
        }
        
        public void ChangeGameFaze(GameFaze gameFaze)
        {
            switch(gameFaze)
            {
                case GameFaze.PrepareForFighting:
                    break;
                case GameFaze.InBattle:
                    break;
                case GameFaze.Result:
                    break;
            }
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