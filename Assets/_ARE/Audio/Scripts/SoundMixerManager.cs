using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundFXSlider;
    [SerializeField] private Slider masterSlider;


    private void Start()
    {
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {

        }

        SetMusicVolume();
        SetSoundFXVolume();
        SetMasterVolume();
    }

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetSoundFXVolume()
    {
        float volume = soundFXSlider.value;
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("soundFXVolume", volume);

    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("musicVolume", volume);

    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        soundFXSlider.value = PlayerPrefs.GetFloat("soundFXVolume");
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");

        SetMusicVolume();
        SetSoundFXVolume();
        SetMasterVolume();
    }

}
