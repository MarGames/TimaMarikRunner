using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    string placementID_rewardedVideo = "rewardedVideo";
    string placementID_Video = "video";
    private string gameID = "4044881";
    public GameObject death;
    public int adsTime;
    private int coins;
    private int coinsIn1Game;
    public Text coinsText;
    void Start()
    {
        death.SetActive(false);
        adsTime = PlayerPrefs.GetInt("AdsTime");
        Advertisement.Initialize(gameID, false);
        
    }
    public void PlayVideo()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(placementID_rewardedVideo);
        }
        else 
        {
            Debug.Log("NotShown");
        }

    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            
            Debug.Log("Shown");
        }
        else if (showResult == ShowResult.Skipped)
        {
            
            Debug.Log("Skipped");
        }
        else if (showResult == ShowResult.Failed)
        {
            
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == placementID_rewardedVideo)
        {

        }
    }
    public void Coinsx2()
    {
        if (Advertisement.IsReady())
        {
            coinsIn1Game = PlayerPrefs.GetInt("CoinsIn1Game");
            Advertisement.Show(placementID_rewardedVideo);
            coinsIn1Game = coinsIn1Game * 2;
            coins = PlayerPrefs.GetInt("Coins");
            coins = coins + coinsIn1Game;
            PlayerPrefs.SetInt("Coins", coins);
            coinsText.GetComponent<Text>().text = "" + coins;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Ad is not ready");
        }
    }
    public void AddCoins()
    {
        if (Advertisement.IsReady())
        {
            coins = PlayerPrefs.GetInt("Coins");
            coins = coins + 100;
            PlayerPrefs.SetInt("Coins", coins);
            coinsText.GetComponent<Text>().text = "" + coins;
            Advertisement.Show(placementID_rewardedVideo);
        }
    }
    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }
}
