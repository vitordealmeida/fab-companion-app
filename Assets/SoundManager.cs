using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    [SerializeField] private AudioSource swordHit;
    
    public static void PlaySwordHit()
    {
        _instance.swordHit.Play();
    }
}
