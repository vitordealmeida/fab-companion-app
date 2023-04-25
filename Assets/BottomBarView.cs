using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class BottomBarView : MonoBehaviour
{
    private RectTransform _rectTransform;

    protected virtual void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public Sequence Hide(bool animate = true)
    {
        return DOTween.Sequence()
            .Append(_rectTransform.DOAnchorPosY(-_rectTransform.rect.height, animate ? .3f : 0)
                .SetEase(Ease.InQuad));
    }

    public Sequence Show(bool animate = true, float autoHide = 0)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOAnchorPosY(0, animate ? .3f : 0)
            .SetEase(Ease.OutQuad));

        if (autoHide > 0)
        {
            sequence.AppendInterval(autoHide)
                .Append(Hide());
        }

        return sequence;
    }
}