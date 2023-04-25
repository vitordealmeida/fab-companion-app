using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    private bool _sfxOn;
    private bool _bgmOn;
    public bool SfxOn
    {
        get => _sfxOn;
        set
        {
            _sfxOn = value;
            PlayerPrefs.SetInt("sfx", value ? 1 : 0);
        }
    }
    public bool BgmOn
    {
        get => _bgmOn;
        set
        {
            _bgmOn = value;
            PlayerPrefs.SetInt("bgm", value ? 1 : 0);
        }
    }

    [SerializeField] private AudioSource swordHit; 
    [SerializeField] private AudioSource bgm; 
    
    private void Awake()
    {
        _sfxOn = PlayerPrefs.GetInt("sfx", 1) == 1;
        _sfxOn = PlayerPrefs.GetInt("bgm", 1) == 1;
        
        DontDestroyOnLoad(gameObject);
    }

    public static void PlaySwordHit()
    {
        _instance.swordHit.Play();
    }

    public static void PlayBgm()
    {
        _instance.bgm.Play();
    }
}
