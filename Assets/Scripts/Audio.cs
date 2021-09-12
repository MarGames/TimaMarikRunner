using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public int music = 1;
    public AudioSource m_MyAudioSource;
    public Button Sound;
    public Sprite[] OnOff;

    public GameObject musicObject;
    void Start()
    {
        music = PlayerPrefs.GetInt("Music");
        if (music == 1)
        {
            Sound.GetComponent<Image>().sprite = OnOff[1];
        }
        else
        {
            Sound.GetComponent<Image>().sprite = OnOff[0];
        }
        m_MyAudioSource = GetComponent<AudioSource>();
    }
    public void TurnOffOnMusic()
    {
        if (music == 1)
        {
            Sound.GetComponent<Image>().sprite = OnOff[0];
            music = 0;
            PlayerPrefs.SetInt("Music", music);
            musicObject.SetActive(true);
        }
        else
        {
            Sound.GetComponent<Image>().sprite = OnOff[1];
            music = 1;
            PlayerPrefs.SetInt("Music", music);
            musicObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (music == 0)
        {
            m_MyAudioSource.mute = true;
        }
        if (music == 1)
        {
            m_MyAudioSource.mute = false;
        }
        if (musicObject.activeSelf == true)
        {
            m_MyAudioSource.mute = true;
        }
    }
}
