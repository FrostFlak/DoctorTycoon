using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Toggle _sfxToggle;
    [SerializeField] private Toggle _musicToggle;
    private const string SFX_PARAMETER = "SfxVolume";
    private const string MUSIC_PARAMETER = "MusicVolume";
    private const int MAX_VOlUME_LEVEL = 0;
    private const int MIN_VOlUME_LEVEL = -80;

    public void CheckSfxToggleState()
    {
        if (_sfxToggle.isOn) TurnOnEffects();
        else TurnOffEffects();
    }
    public void CheckMusicToggleState()
    {
        if (_musicToggle.isOn) TurnOnMusic();
        else TurnOffMusic();
    }
    private void TurnOnEffects()
    {
        _audioMixer.SetFloat(SFX_PARAMETER, MAX_VOlUME_LEVEL);
    }
    private void TurnOffEffects()
    {
        _audioMixer.SetFloat(SFX_PARAMETER, MIN_VOlUME_LEVEL);
    }
    private void TurnOnMusic()
    {
        _audioMixer.SetFloat(MUSIC_PARAMETER, MAX_VOlUME_LEVEL);
    }
    private void TurnOffMusic()
    {
        _audioMixer.SetFloat(MUSIC_PARAMETER, MIN_VOlUME_LEVEL);
    }

}
