using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchController : MonoBehaviour
{
    public TMP_Text playerLifeTotal;
    public TMP_Text enemyLifeTotal;
    public TMP_Text playerDamage;
    public TMP_Text enemyDamage;
    public GameObject confirmExitWindow;
    public GameObject victoryWindow;
    public GameObject defeatWindow;
    public Image playerBackground;
    public Image enemyBackground;
    public ClockView clock;
    
    private int _playerLifePoints;
    private int _enemyLifePoints;
    private int _playerLifePointsConsolidated;
    private int _enemyLifePointsConsolidated;
    private bool _playerDirty;
    private bool _enemyDirty;
    private MatchReport _matchReport;
    
    private Action<MatchReport> _matchFinishedListener;
    private Coroutine _consolidateLifeChange;
    
    public void StartMatch(MatchConfig matchConfig, Action<MatchReport> onMatchFinished)
    {
        confirmExitWindow.SetActive(false);
        victoryWindow.SetActive(false);
        defeatWindow.SetActive(false);
        
        _playerLifePoints = matchConfig.player.lifePoints;
        _playerLifePointsConsolidated = _playerLifePoints;
        _enemyLifePoints = matchConfig.opponent.lifePoints;
        _enemyLifePointsConsolidated = _enemyLifePoints;
        _matchFinishedListener = onMatchFinished;

        playerDamage.alpha = 0;
        enemyDamage.alpha = 0;
        
        playerBackground.sprite = matchConfig.player.backgroundImage;
        playerBackground.color = matchConfig.player.alternativeColor;
        enemyBackground.sprite = matchConfig.opponent.backgroundImage;
        enemyBackground.color = matchConfig.opponent.alternativeColor;
        
        clock.StartClock(matchConfig.MatchDuration, () =>
        {
            Debug.Log("Time's up!");
        }, true);
        
        playerLifeTotal.SetText(_playerLifePoints.ToString());
        enemyLifeTotal.SetText(_enemyLifePoints.ToString());
        
        _matchReport = new MatchReport
        {
            matchEvents = new List<MatchEvent>(),
            matchInfo = matchConfig,
            MatchDateTime = DateTime.Now
        };
        
        CommonCanvasController.ShowSnackbar("Adjust life totals and start the timer!");
    }

    private void RefreshLifeTotals()
    {
        if (_playerDirty)
        {
            var damage = _playerLifePoints - _playerLifePointsConsolidated;
            playerLifeTotal.SetText(_playerLifePoints.ToString());
            playerDamage.alpha = 1;
            playerDamage.text = damage <= 0 ? damage.ToString() : $"+{damage}";
            playerDamage.DOKill();
        }

        if (_enemyDirty)
        {
            var damage = _enemyLifePoints - _enemyLifePointsConsolidated;
            enemyLifeTotal.SetText(_enemyLifePoints.ToString());
            enemyDamage.alpha = 1;
            enemyDamage.text = damage <= 0 ? damage.ToString() : $"+{damage}";
            enemyDamage.DOKill();
        }
        
        if (_consolidateLifeChange != null)
        {
            StopCoroutine(_consolidateLifeChange);
        }

        _consolidateLifeChange = StartCoroutine(ConsolidateLifeChange());
    }

    private IEnumerator ConsolidateLifeChange()
    {
        yield return new WaitForSecondsRealtime(2f);

        if (_playerDirty)
        {
            _matchReport.matchEvents.Add(new MatchEvent()
            {
                isPlayer = true,
                lifeChange = _playerLifePoints - _playerLifePointsConsolidated
            });
        }
        if (_enemyDirty)
        {
            _matchReport.matchEvents.Add(new MatchEvent()
            {
                isPlayer = false,
                lifeChange = _enemyLifePoints - _enemyLifePointsConsolidated
            });
        }

        playerDamage.DOFade(0, 1f);
        enemyDamage.DOFade(0, 1f);
        _playerDirty = false;
        _enemyDirty = false;
        _enemyLifePointsConsolidated = _enemyLifePoints;
        _playerLifePointsConsolidated = _playerLifePoints;
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
        _matchReport.finalPlayerLife = _playerLifePointsConsolidated;
        _matchReport.finalOpponentLife = _enemyLifePointsConsolidated;
        _matchFinishedListener(_matchReport);
    }

    public void AbortQuit()
    {
        confirmExitWindow.SetActive(false);
    }

    public void OnPlayerIncreaseLife()
    {
        _playerDirty = true;
        _playerLifePoints += 1;
        RefreshLifeTotals();
    }

    public void OnPlayerDecreaseLife()
    {
        _playerDirty = true;
        _playerLifePoints -= 1;
        RefreshLifeTotals();
    }

    public void OnEnemyIncreaseLife()
    {
        _enemyDirty = true;
        _enemyLifePoints += 1;
        RefreshLifeTotals();
    }

    public void OnEnemyDecreaseLife()
    {
        _enemyDirty = true;
        _enemyLifePoints -= 1;
        RefreshLifeTotals();
    }

    public void OnReportClicked()
    {
        Application.OpenURL("https://fabtcg.com/accounts/profile/");
    }
}
