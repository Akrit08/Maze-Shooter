using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume );
    }

    public void SetSensitivity()
    {
        PlayerLook.mouseSensitivity = slider.value;
    }
}
