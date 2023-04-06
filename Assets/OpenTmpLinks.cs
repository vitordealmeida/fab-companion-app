using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OpenTmpLinks : MonoBehaviour
{
    private TextMeshProUGUI _tmpText;
    [SerializeField] private UrlWithId[] hyperlinks;

    [Serializable]
    public class UrlWithId
    {
        public string id;
        public UnityEvent unityEvent;
    }

    private void Awake()
    {
        _tmpText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var linkIndex = TMP_TextUtilities.FindIntersectingLink(_tmpText, Input.mousePosition, null);
        if (linkIndex == -1)
        {
            // click did not intersect with a link
            return;
        }

        var linkId = _tmpText.textInfo.linkInfo[linkIndex].GetLinkID();
        foreach (var hyperlink in hyperlinks)
        {
            if (hyperlink.id == linkId)
            {
                hyperlink.unityEvent?.Invoke();
                return;
            }
        }
    }
}