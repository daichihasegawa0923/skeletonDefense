using System.Collections;
using System.Collections.Generic;
using Diamond.SkeletonDefense.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Diamond.SkeletonDefense.UI.Title
{
    public class StageSelectManager : MonoBehaviour
    {
        [SerializeField]
        private Image _selectedImageSprite;
        
        [SerializeField]
        private string _nextSceneName;
        
        [SerializeField]
        private Button _fightButton;

        [SerializeField]
        private List<StageSelectButton> _stageSelectButtons = new List<StageSelectButton>();

        [SerializeField]
        private List<string> _releasedStageName = new List<string>();

        [SerializeField]
        private SceneLoader _sceneLoader;

        private void Start()
        {
            _fightButton.interactable = false;
        }

        private void OnEnable()
        {
            var data = SaveData.Load();

            foreach(var sceneName in data.ReleasedStageNames)
                if(!_releasedStageName.Contains(sceneName))
                     _releasedStageName.Add(sceneName);

            _stageSelectButtons.ForEach(sb =>
            {
                sb.GetComponent<Button>().onClick.RemoveAllListeners();
                sb.GetComponent<Button>().onClick.AddListener(() =>
                {
                    this.SetNextStageInfo(sb);
                });

                if (!_releasedStageName.Contains(sb.StageName))
                {
                    sb.gameObject.SetActive(false);
                }
            });
        }

        public void SetNextStageInfo(StageSelectButton btn)
        {
            this._nextSceneName = btn.StageName;
            this._selectedImageSprite.sprite = btn.StageImage;
            _fightButton.interactable = true;
        }

        public void GoToNextStage()
        {
            if (_nextSceneName != null)
                _sceneLoader.SceneLoad(_nextSceneName);
        }
    }
}