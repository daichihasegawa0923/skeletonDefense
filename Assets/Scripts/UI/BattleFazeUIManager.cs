using Diamond.SkeletonDefense.Data;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Diamond.SkeletonDefense.UI
{
    public class BattleFazeUIManager : MonoBehaviour
    {
        [SerializeField]
        public ActiveCanvasSwitcher _activeCanvasSwitcher;

        [SerializeField]
        private TextMeshProUGUI _maxCountText;

        [SerializeField]
        private TextMeshProUGUI _currentCountText;

        [SerializeField]
        private GameResultUI _resultUI;

        /// <summary>
        /// キャラクターのボタンのリスト
        /// </summary>
        [SerializeField]
        private List<CharacterChooseButton> _characterButtonList;

        private void Start()
        {
            this._activeCanvasSwitcher.Active("PrepareUI");
            this.LoadReleasedCharacter();
        }

        /// <summary>
        /// セーブデータにある解放済みのキャラクターのボタンを表示する
        /// </summary>
        private void LoadReleasedCharacter()
        {
            var data = SaveData.Load();
            var clist = data.ReleasedCharacterNames;

            foreach (var b in _characterButtonList)
            {
                var preList = clist.Where(c => c == b._putCharacter.name).ToList();
                b.gameObject.SetActive(preList.Count > 0);
            }

        }

        public void SetPrepareUI()
        {
            this._activeCanvasSwitcher.Active("PrepareUI");
        }

        public void SetBattleUI()
        {
            this._activeCanvasSwitcher.Active("BattleUI");
        }

        public void SetResultUI(bool isWin)
        {
            this._activeCanvasSwitcher.Active("ResultUI");
            this._resultUI.gameObject.SetActive(true);

            if (isWin)
                this._resultUI.ExposeWinnerUI();
            else
                this._resultUI.ExposeLoserUI();
        }

        /// <summary>
        /// Set max cost count (called by game manager)
        /// </summary>
        public void SetMaxCostCountText(int maxCost)
        {
            _maxCountText.text = maxCost.ToString();
        }

        /// <summary>
        /// Set current cost count(called by game manager)
        /// </summary>
        /// <param name="currentCost"></param>
        public void SetCurrentCostCountText(int currentCost)
        {
            _currentCountText.text = currentCost.ToString();
        }
    }
}