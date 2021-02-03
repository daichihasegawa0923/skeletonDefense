using Diamond.SkeletonDefense.Character;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        /// ゲームの進行状況
        /// </summary>
        public static GameFaze GameFaze { private set; get; } = GameFaze.PrepareForFighting;

        private void Awake()
        {
            GameManager.GameFaze = GameFaze.PrepareForFighting;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

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