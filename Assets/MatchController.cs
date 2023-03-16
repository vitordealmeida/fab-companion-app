using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MatchController : MonoBehaviour
{
    public TMP_Text playerLifeTotal;
    public TMP_Text enemyLifeTotal;
    public GameObject confirmExitWindow;
    
    private int _playerLifePoints;
    private int _enemyLifePoints;
    private Action _matchFinishedListener;

    public void StartMatch(int playerStartLife, int enemyStartLife, Action onMatchFinished)
    {
        confirmExitWindow.SetActive(false);
        
        _playerLifePoints = playerStartLife;
        _enemyLifePoints = enemyStartLife;
        _matchFinishedListener = onMatchFinished;
        
        RefreshLifeTotals();
    }

    private void RefreshLifeTotals()
    {
        playerLifeTotal.SetText(_playerLifePoints.ToString());
        enemyLifeTotal.SetText(_enemyLifePoints.ToString());
    }

    public void OnExitMatch()
    {
        confirmExitWindow.SetActive(true);
    }

    public void ConfirmQuit()
    {
        _matchFinishedListener();
    }

    public void AbortQuit()
    {
        confirmExitWindow.SetActive(false);
    }

    public void OnPlayerIncreaseLife()
    {
        _playerLifePoints += 1;
        RefreshLifeTotals();
    }

    public void OnPlayerDecreaseLife()
    {
        _playerLifePoints -= 1;
        RefreshLifeTotals();
    }

    public void OnEnemyIncreaseLife()
    {
        _enemyLifePoints += 1;
        RefreshLifeTotals();
    }

    public void OnEnemyDecreaseLife()
    {
        _enemyLifePoints -= 1;
        RefreshLifeTotals();
    }
}
