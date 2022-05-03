using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Mixer")]
    [SerializeField] AudioMixer mixer;

    [Header("Sliders")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    [Header("Delete Button")]
    [SerializeField] GameObject deleteDataButton;
    [SerializeField] GameObject dataDEletedText;
    
    MainHUD mainHud;

    private void Awake()
    {
        mainHud = GetComponent<MainHUD>();
    }

    private void OnEnable()
    {
        mixer.GetFloat(AudioConsts.Master, out float masterVolume);
        masterSlider.value = VolumeConvertDb(masterVolume);

        mixer.GetFloat(AudioConsts.Music, out float musicVolume);
        musicSlider.value = VolumeConvertDb(musicVolume);

        mixer.GetFloat(AudioConsts.Sfx, out float sfxVolume);
        sfxSlider.value = VolumeConvertDb(sfxVolume);

        deleteDataButton.SetActive(true);
        dataDEletedText.SetActive(false);
    }

    public void CloseSettings()
    {
        mainHud.ShowStartGameOverlay();
    }

    public void MasterVolume(float volume)
    {
        float convertedVolume = VolumeConvert(volume);
        mixer.SetFloat(AudioConsts.Master, convertedVolume);
    }

    public void MusicVolume(float volume)
    {
        float convertedVolume = VolumeConvert(volume);
        mixer.SetFloat(AudioConsts.Music, convertedVolume);
    }

    public void SfxVolume(float volume)
    {
        float convertedVolume = VolumeConvert(volume);
        mixer.SetFloat(AudioConsts.Sfx, convertedVolume);
    }

    private float VolumeConvert(float volume)
    {
        const float MinDb = -80;
        const float MaxDb = 0;

        return Mathf.Lerp(MinDb, MaxDb, volume);
    }

    private float VolumeConvertDb(float volume)
    {
        const float MinDb = -80;
        const float MaxDb = 0;

        return Mathf.InverseLerp(MinDb, MaxDb, volume);
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();

        deleteDataButton.SetActive(false);
        dataDEletedText.SetActive(true);
    }
}
