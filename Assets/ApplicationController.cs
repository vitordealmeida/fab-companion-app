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
    public Button startFabraryMatch;

    private bool _openingExternalLink;
    
    void Start()
    {
        matchCanvas.gameObject.SetActive(false);
        preMatchCanvas.gameObject.SetActive(true);
    }

    public void OpenFabrary()
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
        startFabraryMatch.interactable = !string.IsNullOrEmpty(fabraryId);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus == false && _openingExternalLink)
        {
            ShowPasteFabraryLinkWindow();
            _openingExternalLink = false;
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && _openingExternalLink)
        {
            ShowPasteFabraryLinkWindow();
            _openingExternalLink = false;
        }
    }

    private void ShowPasteFabraryLinkWindow()
    {
        fabraryLinkWindow.SetActive(true);
        startFabraryMatch.interactable = false;
    }

    public void HidePasteFabraryLinkWindow()
    {
        fabraryLinkWindow.SetActive(false);
    }

    public void StartMatch()
    {
        fabraryLinkWindow.SetActive(false);
        preMatchCanvas.gameObject.SetActive(false);
        matchCanvas.gameObject.SetActive(true);
        matchController.StartMatch(20, 20, () =>
        {
            matchCanvas.gameObject.SetActive(false);
            preMatchCanvas.gameObject.SetActive(true);
        });
    }
}
