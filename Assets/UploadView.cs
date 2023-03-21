using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UploadView : MonoBehaviour
{
    public TMP_Text uploading;
    public TMP_Text uploadSuccess;
    public Button quitButton;
    public GameObject loading;
    public GameObject resultsPanel;

    void OnEnable()
    {
        uploading.gameObject.SetActive(true);
        uploadSuccess.gameObject.SetActive(false);
        loading.gameObject.SetActive(true);
        resultsPanel.SetActive(false);
        quitButton.interactable = false;
        Invoke(nameof(ShowUploadResult), 1.2f);
    }

    private void ShowUploadResult()
    {
        if (this == null) return;
        uploadSuccess.gameObject.SetActive(true);
        loading.SetActive(false);
        resultsPanel.SetActive(true);
        quitButton.interactable = true;
    }
}