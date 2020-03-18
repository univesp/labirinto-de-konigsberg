using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _sfx;
    [SerializeField] private AudioSource _bgm;

    public static AudioPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(AudioClip sound)
    {
        _sfx.PlayOneShot(sound);
    }

    public void PlayBGM(AudioClip music)
    {
        _bgm.clip = music;
        _bgm.Play();
    }

    public void StopSFX()
    {
        _sfx.Stop();
    }

    public void StopBGM()
    {
        _bgm.Stop();
    }
}