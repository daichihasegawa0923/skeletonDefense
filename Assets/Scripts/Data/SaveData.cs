
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.SkeletonDefense.Data
{

    [Serializable]
    public class SaveData
    {
        /// <summary>
        /// クリア済みのシーン名
        /// </summary>
        public List<string> ClearedSceneName = new List<string>();

        /// <summary>
        /// 解放済みのキャラクター一覧
        /// </summary>
        public List<string> ReleasedCharacterNames = new List<string>();

        private static readonly string _saveDataPrefsName = "save_data";

        /// <summary>
        /// 保存されているデータを呼び出します。
        /// </summary>
        /// <returns>セーブデータを呼び出す</returns>
        public static SaveData Load()
        {
            var str = PlayerPrefs.GetString(_saveDataPrefsName, string.Empty);
            var saveData = new SaveData();

            if (string.IsNullOrWhiteSpace(str))
            {
                return saveData;
            }

            saveData = JsonUtility.FromJson<SaveData>(str);
            return saveData;
        }

        /// <summary>
        /// データを保存します。
        /// </summary>
        /// <param name="data">データの保存</param>
        public static void Save(SaveData data)
        {
            var str = JsonUtility.ToJson(data);
            if (str == null)
                str = string.Empty;

            PlayerPrefs.SetString(_saveDataPrefsName, str);
        }
    }
}