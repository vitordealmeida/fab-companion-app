using System;
using Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationController : MonoBehaviour
{
    public Canvas matchCanvas;
    public Canvas preMatchCanvas;
    public MatchController matchController;
    public GameObject fabraryLinkWindow;
    public TMP_InputField fabraryDeckId;
    public TMP_Text player1DeckName;
    public TMP_Text player2DeckName;
    public Button loadDeck;
    public TMP_Dropdown matchFormat;

    public HeroConfig generic1;
    public HeroConfig generic2;
    public HeroConfig stub1;
    public HeroConfig stub2;
    
    private bool _openingExternalLinkPlayer1;
    private bool _openingExternalLinkPlayer2;
    private MatchConfig _nextMatchConfig;

    private void Start()
    {
        matchCanvas.gameObject.SetActive(false);
        preMatchCanvas.gameObject.SetActive(true);
        _nextMatchConfig = new MatchConfig
        {
            player = generic1,
            opponent = generic2,
            MatchDuration = TimeSpan.MaxValue
        };
    }

    public void OpenFabraryPlayer1()
    {
        _openingExternalLinkPlayer1 = true;
        Application.OpenURL("https://fabrary.net/decks?tab=mine&action=copyDeckId");
    }

    public void OpenFabraryPlayer2()
    {
        _openingExternalLinkPlayer2 = true;
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
        if (_openingExternalLinkPlayer1)
        {
            player1DeckName.text = "(Mock) Player 1 Deck";
            _nextMatchConfig.player = stub1;
        }
        else if (_openingExternalLinkPlayer2)
        {
            player2DeckName.text = "(Mock) Player 2 Deck";
            _nextMatchConfig.opponent = stub2;
        }

        fabraryDeckId.text = string.Empty;
        HidePasteFabraryLinkWindow();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus == false && (_openingExternalLinkPlayer1 || _openingExternalLinkPlayer2))
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
        _openingExternalLinkPlayer1 = false;
        _openingExternalLinkPlayer2 = false;
    }

    public void StartMatch()
    {
        fabraryLinkWindow.SetActive(false);
        preMatchCanvas.gameObject.SetActive(false);
        matchCanvas.gameObject.SetActive(true);
        matchController.StartMatch(_nextMatchConfig, () =>
        {
            matchCanvas.gameObject.SetActive(false);
            preMatchCanvas.gameObject.SetActive(true);
        });
    }
}
