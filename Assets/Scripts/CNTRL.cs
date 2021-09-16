using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Advertisements;

public class CNTRL : MonoBehaviour, IUnityAdsListener
{
    private Rigidbody rb;

    public Text coinsText;
    private int coins;
    private int coinsIn1Game;
    private int score;
    string placementID_rewardedVideo = "rewardedVideo";
    string placementID_Video = "video";
    private string gameID = "4044881";
    private bool AdShown = false;
    private bool Invulnerable = false;
    private int revive = 1;
    float InvulnerableTimer;
    private bool Shield;
    float shieldTime;


    [Header("GameObjects")]
    public GameObject[] players;
    public GameObject panel;
    public GameObject slide;
    public GameObject death;
    public GameObject jump;
    public GameObject jumpWays;
    public GameObject camera1;
    public GameObject button;
    public GameObject objectsToDestroy;
    public GameObject[] menu;
    public GameObject[] obstacles;
    public GameObject multiplier;

    private int skin;
    public static CNTRL instance;
    private float timer;
    private bool crash = false;

    private Animator anim;

    public GameObject music;

    [Header("Shield")]
    //public GameObject shield;
    public ShieldTimer shieldTimer;
    void Start()
    {
        multiplier.SetActive(false);
        coinsIn1Game = 0;
        death.SetActive(false);
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, false);
        coins = PlayerPrefs.GetInt("Coins");
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0f, 0f, 70f, ForceMode.Acceleration);
        anim = GetComponent<Animator>();
        music.SetActive(false);
    }

    void Update()
    {
        OnUpdate();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (Shield)
        {
            if (collision.collider.tag == "Obstacle")
                Destroy(collision.gameObject);
        }
        if (Invulnerable == false && Shield == false)
        {
            if (collision.collider.tag == "Obstacle")
            {
                PlayerPrefs.SetInt("Coins", coins);
                PlayerPrefs.SetInt("CoinsIn1Game", coinsIn1Game);
                crash = true;
                anim.SetBool("Death", true);
                FindObjectOfType<AudioManager>().Play("End");
                FindObjectOfType<AudioManager>().Play("Death");
                music.SetActive(true);
                button.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Shield)
        {
            if (other.tag == "car")
                Destroy(other.gameObject);
            if (other.tag == "Obstacle")
                Destroy(other.gameObject);
        }
        if (Invulnerable == false && Shield == false)
        {
            if (other.tag == "car")
            {
                PlayerPrefs.SetInt("Coins", coins);
                PlayerPrefs.SetInt("CoinsIn1Game", coinsIn1Game);
                crash = true;
                anim.SetBool("Death", true);
                FindObjectOfType<AudioManager>().Play("End");
                FindObjectOfType<AudioManager>().Play("Death");
                music.SetActive(true);
                rb.velocity = new Vector3(0f, 0f, 0f);
                button.SetActive(true);
            }
        }
        if (other.tag == "Coin")
        {
            FindObjectOfType<AudioManager>().Play("Coin");
            coins = coins + 1;
            coinsIn1Game = coinsIn1Game + 1;
            PlayerPrefs.SetInt("Coins", coins);
            Destroy(other.gameObject);
            coinsText.GetComponent<Text>().text = "" + coins;
        }
        if (other.tag == "Booster")
        {
            FindObjectOfType<AudioManager>().Play("PowerUp");
            rb.AddForce(0f, 0f, 17.6f, ForceMode.Acceleration);
        }
        if (other.tag == "Shield")
        {
            FindObjectOfType<AudioManager>().Play("PowerUp");
            shieldTimer.gameObject.SetActive(true);
            shieldTimer.isCoolDown = true;
            Shield = true;
            obstacles[3].GetComponentInChildren<MeshCollider>().isTrigger = true;
            obstacles[4].GetComponentInChildren<MeshCollider>().convex = true;
            obstacles[4].GetComponentInChildren<MeshCollider>().isTrigger = true;
            obstacles[5].GetComponentInChildren<MeshCollider>().isTrigger = true;
            Destroy(other.gameObject);

        }
        if (other.tag == "Acceleration")
        {
            FindObjectOfType<AudioManager>().Play("PowerUp");
            rb.AddForce(0f, 0f, 10f, ForceMode.Acceleration);
            
            Destroy(other.gameObject);
        }
        
        if (other.tag == "2X")
        {
            FindObjectOfType<AudioManager>().Play("PowerUp");
            multiplier.SetActive(true);
            Destroy(other.gameObject);
        }
    }
    public void Button()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }
    public void Revieve()
    {
        revive = 0;
        Advertisement.Show(placementID_rewardedVideo);
        
        menu[2].SetActive(false);
        menu[3].SetActive(false);
        menu[4].SetActive(false);
        menu[5].SetActive(false);
        button.SetActive(false);
    }

 
    private void ResultFinished()
    {
        if (revive == 0)
        {
            Debug.Log("The ad was successfully shown.");
            Time.timeScale = 1f;
            menu[0].SetActive(false);
            menu[2].SetActive(false);
            menu[1].SetActive(true);
            music.SetActive(false);
            Invulnerable = true;
            crash = false;
            AdShown = true;
            Destroy(GameObject.Find("Car1 1(Clone)"));
            Destroy(GameObject.Find("Car2 1(Clone)"));
            Destroy(GameObject.Find("Car3 1(Clone)"));
            Destroy(GameObject.Find("Obstacle 2 2(Clone)"));
            Destroy(GameObject.Find("Obstacle 1 2(Clone)"));
            Destroy(GameObject.Find("Obstacle 3(Clone)"));
            anim.SetBool("Death", false);
            rb.AddForce(0f, 0f, 70f, ForceMode.Acceleration);
        }
    }
    private void OnUpdate()
    {
        
        //score = (int)transform.position.z * multiplier;
        camera1.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z - 0.6f);
        if (slide.activeSelf == true)
        {
            anim.SetBool("slide", true);
            GetComponent<MeshCollider>().isTrigger = true;
        }
        else
        {
            GetComponent<MeshCollider>().isTrigger = false;
            anim.SetBool("slide", false);
        }
        if (jump.activeSelf == true)
        {
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }
        if (jumpWays.activeSelf)
        {
            anim.SetBool("jumpWays", true);
        }
        else
        {
            anim.SetBool("jumpWays", false);
        }

        if (crash == true)
        {
            timer += Time.deltaTime;
        }
        if (timer > 2.5)
        {
            if (AdShown == false)
            {
                menu[0].SetActive(true);
                menu[1].SetActive(false);
                menu[2].SetActive(true);
                menu[4].SetActive(false);
            }
            else
            {
                menu[0].SetActive(true);
                menu[1].SetActive(false);
                menu[2].SetActive(false);
                button.SetActive(true);
                menu[4].SetActive(false);
            }
            Time.timeScale = 0f;
            timer = 0f;
        }
        if (Invulnerable == true)
        {
            InvulnerableTimer += Time.deltaTime;
        }
        if (InvulnerableTimer > 2.5f)
        {
            Invulnerable = false;
            InvulnerableTimer = 0f;
        }
        if (!Shield)
        {
            obstacles[3].GetComponentInChildren<MeshCollider>().isTrigger = false;
            obstacles[4].GetComponentInChildren<MeshCollider>().isTrigger = false;
            obstacles[4].GetComponentInChildren<MeshCollider>().convex = false;
            obstacles[5].GetComponentInChildren<MeshCollider>().isTrigger = false;
        }
        shieldTime += Time.deltaTime;
        if (shieldTime > 30f)
        {
            Shield = false;
            shieldTime = 0f;
            shieldTimer.gameObject.SetActive(false);
            shieldTimer.isCoolDown = false;
        }
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
                ResultFinished();
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
    public void Coinx2()
    {
        if (Advertisement.IsReady())
        {
            revive = 1;
            Advertisement.Show(placementID_rewardedVideo);
            coinsIn1Game = coinsIn1Game * 2;
            coins = coins + coinsIn1Game;
            PlayerPrefs.SetInt("Coins", coins);
            menu[3].SetActive(false);
        }
    }

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }
}
