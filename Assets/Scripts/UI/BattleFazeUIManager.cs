using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFazeUIManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _ChildUIs = new List<GameObject>();

    private void Start()
    {
        var children = transform.GetComponentsInChildren<Canvas>();
        foreach(var child in children)
        {
            if(child.gameObject != gameObject)
                _ChildUIs.Add(child.gameObject);
        }

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
        if(_ChildUIs != null && _ChildUIs.Count != 0)
        {
            _ChildUIs.ForEach(ui => ui.SetActive(ui.name == name));
        }
    }
}
