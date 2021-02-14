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
        private List<GameObject> _ChildUIs = new List<GameObject>();

        [SerializeField]
        private TextMeshProUGUI _maxCountText;

        [SerializeField]
        private TextMeshProUGUI _currentCountText;

        /// <summary>
        /// キャラクターのボタンのリスト
        /// </summary>
        [SerializeField]
        private List<CharacterChooseButton> _characterButtonList;

        private void Start()
        {
            var children = transform.GetComponentsInChildren<Canvas>();
            foreach (var child in children)
            {
                if (child.gameObject != gameObject)
                    _ChildUIs.Add(child.gameObject);
            }

            this.ActiveOneCanvas("PrepareUI");
            this.LoadReleasedCharacter();
        }

        /// <summary>
        /// セーブデータにある解放済みのキャラクターのボタンを表示する
        /// </summary>
        private void LoadReleasedCharacter()
        {
            var data = SaveData.Load();
            var clist = data.ReleasedCharacterNames;

            if (clist == null || clist.Count == 0)
                return;

            foreach (var b in _characterButtonList)
            {
                var preList = clist.Where(c => c == b._putCharacter.name).ToList();
                b.gameObject.SetActive(preList.Count > 0);
            }

        }

        public void SetPrepareUI()
        {
            this.ActiveOneCanvas("PrepareUI");
        }

        public void SetBattleUI()
        {
            this.ActiveOneCanvas("BattleUI");
        }

        /// <summary>
        /// 指定された名前のオブジェクトのみをアクティブにします。
        /// </summary>
        /// <param name="name">オブジェクトの名前</param>
        public void ActiveOneCanvas(string name)
        {
            if (_ChildUIs != null && _ChildUIs.Count != 0)
            {
                _ChildUIs.ForEach(ui => ui.SetActive(ui.name == name));
            }
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