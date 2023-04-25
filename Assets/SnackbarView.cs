using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SnackbarView : BottomBarView
{
    protected override void Awake()
    {
        base.Awake();
        Hide(false);
    }
}
