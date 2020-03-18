using UnityEngine;

public class AnimationAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    public void PlayAudio()
    {
        AudioPlayer.Instance.PlaySFX(_audioClip); 
    }
}