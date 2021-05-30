using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameResultUI : MonoBehaviour
{
    /// <summary>
    /// result of game
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _resultTextUI;

    /// <summary>
    /// ui for winner user
    /// </summary>
    [SerializeField]
    private GameObject _winnerUI;

    /// <summary>
    /// ui for loser user
    /// </summary>
    [SerializeField]
    private GameObject _loserUI;

    /// <summary>
    ///  Epose ui when user win
    /// </summary>
    public void ExposeWinnerUI()
    {
        _resultTextUI.text = "YOU WIN!!";
        _winnerUI.SetActive(true);
        _loserUI.SetActive(false);
    }

    /// <summary>
    /// Expose ui when user lose
    /// </summary>
    public void ExposeLoserUI()
    {
        _resultTextUI.text = "YOU LOSE...!";
        _loserUI.SetActive(true);
        _winnerUI.SetActive(false);
    }
}

public static class SetActiveExtension
{
    public static void SetActive(this GameObject gameObject,bool isActive,Action action)
    {
        action();
        gameObject.SetActive(isActive);
    }
}
