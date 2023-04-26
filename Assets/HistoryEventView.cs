using System.Collections;
using System.Collections.Generic;
using Domain;
using TMPro;
using UnityEngine;

public class HistoryEventView : MonoBehaviour
{
    public TMP_Text deltaLife;
    public TMP_Text eventLabel;
    public string textMask;

    public void Populate(MatchEvent matchEvent)
    {
        deltaLife.text = matchEvent.lifeChange > 0 ? $"+{matchEvent.lifeChange}" : matchEvent.lifeChange.ToString();
        eventLabel.text = string.Format(textMask, matchEvent.totalLifeAfterChange);
    }
}
