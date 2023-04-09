using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class BottomBarView : MonoBehaviour
{
    private RectTransform _rectTransform;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Hide()
    {
        _rectTransform.DOAnchorPosY(-_rectTransform.rect.height, .3f);
    }

    public void Show(bool animate = true)
    {
        _rectTransform.DOAnchorPosY(0, animate ? .3f : 0);
    }
}
