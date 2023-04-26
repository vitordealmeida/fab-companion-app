using System;
using System.Collections.Generic;
using Domain;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationController : MonoBehaviour
{
    public Canvas matchCanvas;
    public Canvas preMatchCanvas;
    public MatchController matchController;
    public GameObject fabraryLinkWindow;
    public GameObject timeLimitWindow;
    public TMP_InputField fabraryDeckId;
    public TMP_Text player1DeckName;
    public Button loadDeck;
    public TMP_Text mainPhrase;

    public HeroConfig generic1;
    public HeroConfig generic2;
    public HeroConfig stub1;

    public Toggle noTimeLimitToggle;
    public Toggle blitzToggle;
    public Toggle ccToggle;

    private bool _openingExternalLink;
    private MatchConfig _nextMatchConfig;
    private List<MatchReport> _matchHistory;

    private string _phrase =
        //@"<font-weight=""300""><color=#FFFFFF99>Match using </color><link=deck><color=#FFFFFFDE><font-weight=""500""><u>Generic Deck</u></font-weight></color><color=#FFFFFF99></link> with duration of </color><color=#FFFFFFDE><u><font-weight=""500""><link=time>{0}</link></u></color>";
        @"<font-weight=""300""><color=#FFFFFF99>Match using Generic Deck with duration of </color><color=#FFFFFFDE><u><font-weight=""500""><link=time>{0}</link></u></color>";

    private void Start()
    {
        _matchHistory = FetchMatchHistoryList();
        matchCanvas.gameObject.SetActive(false);
        preMatchCanvas.gameObject.SetActive(true);
        _nextMatchConfig = new MatchConfig
        {
            player = generic1,
            opponent = generic2,
            MatchDuration = TimeSpan.MaxValue
        };

        noTimeLimitToggle.isOn = true;
        PrintPhrase();
    }

    private List<MatchReport> FetchMatchHistoryList()
    {
        var serializedHistory = PlayerPrefs.GetString("matchHistory");
        var history = JsonConvert.DeserializeObject<List<MatchReport>>(serializedHistory);
        return history ?? new List<MatchReport>();
    }

    private void PrintPhrase()
    {
        var matchDuration = noTimeLimitToggle.isOn
            ? "No Time Limit"
            : blitzToggle.isOn ? "30 minutes" : "50 minutes";
        mainPhrase.text = string.Format(_phrase, matchDuration);
    }

    public void OpenFabraryPlayer1()
    {
        _openingExternalLink = true;
        Application.OpenURL("https://fabrary.net/decks?tab=mine&action=copyDeckId");
    }

    public void PasteFabraryId()
    {
        var fabraryId = UniClipboard.GetText();
        fabraryDeckId.SetTextWithoutNotify(fabraryId);
        OnFabraryIdChanged(fabraryId);
    }

    public void OnFabraryIdChanged(string fabraryId)
    {
        loadDeck.interactable = !string.IsNullOrEmpty(fabraryId);
        Debug.Log($"Fabrary id changed to {fabraryId}, button interactable = {loadDeck.interactable}");
    }

    public void LoadFabraryDeck()
    {
        if (_openingExternalLink)
        {
            player1DeckName.text = "(Mock) Player 1 Deck";
            _nextMatchConfig.player = stub1;
        }

        fabraryDeckId.text = string.Empty;
        HidePasteFabraryLinkWindow();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus == false && _openingExternalLink)
        {
            ShowPasteFabraryLinkWindow();
        }
    }

    private void ShowPasteFabraryLinkWindow()
    {
        fabraryLinkWindow.SetActive(true);
        loadDeck.interactable = false;
    }

    public void HidePasteFabraryLinkWindow()
    {
        fabraryLinkWindow.SetActive(false);
        _openingExternalLink = false;
    }

    public void StartMatch()
    {
        _nextMatchConfig.MatchDuration = noTimeLimitToggle.isOn
            ? TimeSpan.MaxValue
            : TimeSpan.FromMinutes(blitzToggle.isOn ? 30 : 50);

        fabraryLinkWindow.SetActive(false);
        preMatchCanvas.gameObject.SetActive(false);
        matchCanvas.gameObject.SetActive(true);
        CommonCanvasController.HideBottomBar();
        matchController.StartMatch(_nextMatchConfig, report =>
        {
            SaveReport(report);
            matchCanvas.gameObject.SetActive(false);
            preMatchCanvas.gameObject.SetActive(true);
            CommonCanvasController.ShowBottomBar();
        });
    }

    public void ListDecks()
    {
    }

    public void PickMatchTime()
    {
        timeLimitWindow.SetActive(true);
    }

    public void CloseTimeLimitWindow()
    {
        timeLimitWindow.SetActive(false);
    }

    public void OnTimeLimitChanged()
    {
        PrintPhrase();
    }

    private void SaveReport(MatchReport report)
    {
        _matchHistory.Add(report);
        PersistMatchHistoryList();
    }

    private void PersistMatchHistoryList()
    {
        var serializedList = JsonConvert.SerializeObject(_matchHistory);
        PlayerPrefs.SetString("matchHistory", serializedList);
    }

    public void ShowMatchesHistory()
    {
        
    }
}