using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    public TMP_Text playerLifeTotal;
    public TMP_Text enemyLifeTotal;
    public GameObject confirmExitWindow;
    public GameObject victoryWindow;
    public GameObject defeatWindow;
    
    private int _playerLifePoints;
    private int _enemyLifePoints;
    private Action _matchFinishedListener;

    private Coroutine _consolidateLifeChange;
    
    public void StartMatch(int playerStartLife, int enemyStartLife, Action onMatchFinished)
    {
        confirmExitWindow.SetActive(false);
        victoryWindow.SetActive(false);
        defeatWindow.SetActive(false);
        
        _playerLifePoints = playerStartLife;
        _enemyLifePoints = enemyStartLife;
        _matchFinishedListener = onMatchFinished;
        
        RefreshLifeTotals();
    }

    private void RefreshLifeTotals()
    {
        playerLifeTotal.SetText(_playerLifePoints.ToString());
        enemyLifeTotal.SetText(_enemyLifePoints.ToString());

        if (_consolidateLifeChange != null)
        {
            StopCoroutine(_consolidateLifeChange);
        }

        _consolidateLifeChange = StartCoroutine(ConsolidateLifeChange());
    }

    private IEnumerator ConsolidateLifeChange()
    {
        yield return new WaitForSecondsRealtime(2f);
        if (_playerLifePoints <= 0)
        {
            defeatWindow.SetActive(true);
        }
        else if (_enemyLifePoints <= 0)
        {
            victoryWindow.SetActive(true);
        }
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
