using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoretxt;
    public int score = 0;
    public Text highscore;
    public GameObject[] players;
    public GameObject multiplier;
    int Mult;
    void Start()
    {
        Mult = 1;
        highscore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        scoretxt.GetComponent<Text>().text = "" + score;
    }

    void Update()
    {
        if (multiplier.activeSelf)
            Mult = 2;
        else
            Mult = 1;
        if (players[0].activeSelf)
            score = (int)players[0].transform.position.z * Mult;
        if (players[1].activeSelf)
            score = (int)players[1].transform.position.z * Mult;
        if (players[2].activeSelf)
            score = (int)players[2].transform.position.z * Mult;
        if (players[3].activeSelf)
            score = (int)players[3].transform.position.z;
        scoretxt.GetComponent<Text>().text = "" + score;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highscore.text = score.ToString();
        }
    }
}
