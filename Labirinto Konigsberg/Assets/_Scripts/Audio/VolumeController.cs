using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _masterMixer;

    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Start()
    {
        StartSliders();
        VolumeBGM();
        VolumeSFX();
    }

    private void StartSliders()
    {
        _bgmSlider.value = PlayerPrefs.GetFloat("BGM", -20.0f);
        _sfxSlider.value = PlayerPrefs.GetFloat("SFX", -20.0f);
    }

    public void VolumeBGM()
    {
        _masterMixer.SetFloat("BGM", _bgmSlider.value);
        PlayerPrefs.SetFloat("BGM", _bgmSlider.value);
    }

    public void VolumeSFX()
    {
        _masterMixer.SetFloat("SFX", _sfxSlider.value);
        PlayerPrefs.SetFloat("SFX", _sfxSlider.value);
    }
}
