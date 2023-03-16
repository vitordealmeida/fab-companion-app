using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UploadView : MonoBehaviour
{
    public TMP_Text uploading;
    public TMP_Text uploadSuccess;
    public Button quitButton;

    void OnEnable()
    {
        uploading.gameObject.SetActive(true);
        uploadSuccess.gameObject.SetActive(false);
        quitButton.interactable = false;
        Invoke(nameof(ShowUploadResult), 1f);
    }

    private void ShowUploadResult()
    {
        if (this == null) return;
        uploadSuccess.gameObject.SetActive(true);
        quitButton.interactable = true;
    }
}