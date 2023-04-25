using DG.Tweening;
using TMPro;
using UnityEngine;

public class CommonCanvasController : MonoBehaviour
{
    private static CommonCanvasController _instance;

    public BottomBarView bottomBarView;
    public BottomBarView snackbar;
    public TMP_Text snackbarText;

    private static Sequence _lastAnimation;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void ShowSnackbar(string text)
    {
        _instance.snackbarText.text = text;

        if (_lastAnimation?.IsPlaying() == true)
        {
            _lastAnimation.Append(_instance.snackbar.Show(true, 4));
            return;
        }
        
        _lastAnimation = _instance.snackbar.Show(true, 4);
    }

    public static void HideBottomBar()
    {
        if (_lastAnimation?.IsPlaying() == true)
        {
            _lastAnimation.Append(_instance.bottomBarView.Hide());
            return;
        }
        
        _lastAnimation = _instance.bottomBarView.Hide();
    }

    public static void ShowBottomBar()
    {
        if (_lastAnimation?.IsPlaying() == true)
        {
            _lastAnimation.Append(_instance.bottomBarView.Show());
            return;
        }
        
        _lastAnimation = _instance.bottomBarView.Show();
    }
}
