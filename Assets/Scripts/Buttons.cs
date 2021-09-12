using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject[] images;
    public Animator transition;
    public void Exit()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        Application.Quit();
    }
    public void Play()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        Time.timeScale = 1f;
        StartCoroutine(LoadPlayLevel());
    }
    public void Skins()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        Time.timeScale = 1f;
        StartCoroutine(LoadSkinLevel());
    }
    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        Time.timeScale = 1f;
        StartCoroutine(LoadMenuLevel());
    }
    public void Pause()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        Time.timeScale = 0f;
        images[0].SetActive(true);
        images[1].SetActive(false);
        images[2].SetActive(true);
    }
    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        Time.timeScale = 1f;
        images[0].SetActive(false);
        images[1].SetActive(true);
        images[2].SetActive(true);
    }
    public void Refresh()
    {
        PlayerPrefs.DeleteAll();
    }
    void Start()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);
    }
    IEnumerator LoadPlayLevel()
    {
        transition.Play("CircleWipe");
        
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main");
    }
    IEnumerator LoadSkinLevel()
    {
        transition.Play("CircleWipe");

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Skins");
    }
    IEnumerator LoadMenuLevel()
    {
        transition.Play("CircleWipe");

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }
}
