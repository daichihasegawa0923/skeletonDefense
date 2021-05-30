using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.SkeletonDefense.UI.Title
{
    public class StageSelectButton : MonoBehaviour
    {
        [SerializeField]
        private string _stageName;

        [SerializeField]
        private Sprite _stageImage;

        public string StageName { get => _stageName; private set => _stageName = value; }
        public Sprite StageImage { get => _stageImage; private set => _stageImage = value; }
    }
}