using Diamond.SkeletonDefense.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace Diamond.SkeletonDefense.UI
{
    [CustomEditor(typeof(CharacterChooseButton))]
    public class CharacterChooseButtonEditor :ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            var bh = target as CharacterChooseButton;
            GUILayout.BeginHorizontal();
            bh._putCharacter = EditorGUILayout.ObjectField("PutCharacter", bh._putCharacter, typeof(CharacterBase), true) as CharacterBase;
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            bh._gameManager = EditorGUILayout.ObjectField("GameManager", bh._gameManager, typeof(GameManager), true) as GameManager;
            GUILayout.EndHorizontal();

            base.OnInspectorGUI();
        }
    }
}