# skeletonDefense
💀のタワーディフェンス

# 詳細設計
## SaveDataクラスの使い方
### メソッド
- Load (Static)
    - 端末に保存されているデータをロードします。
    - 返り値：SaveData型のオブジェクト
- Save (Static)
    - 端末にデータを保存します。
    - 引数：SaveData型のオブジェクト
### 使用例
- 解放済みのキャラクターの更新
    1. Loadメソッドを呼び出し、SaveDataの中にある解放済みキャラクターのリストを更新する
    1. 1.で更新したSaveDataオブジェクトをSaveメソッドで端末に保存する
- 実装例

``` C#
// 端末に保存されている情報を取得する
var saveData = SaveData.Load();
saveData.キャラクターのリスト.Add("新しいキャラクターの名前");
// 端末に情報を保存する
SaveData.Save(saveData);
```

### 注意
- SaveメソッドとLoadメソッドはPlayerPrefsを使っているため、PlayerPrefsが使用できないデバイスではデータの保存が不可能