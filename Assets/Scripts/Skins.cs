using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skins : MonoBehaviour
{
    public int skinChoosen = 1;
    public int skinChoosing = 1;
    private int coins;
    public Text coinstext;

    [Header("GameObjects")]
    public GameObject skinsPlane;
    public GameObject defaultSkin;
    public GameObject skin0;
    public GameObject skin3;
    public GameObject skin2;
    public GameObject buyButton;
    public GameObject chooseButton;
    public GameObject[] gameObjects;

    int purchased0 = 0;
    int purchased1 = 1;
    public int purchased2 = 0;
    int purchased3 = 0;
    private Animator anim;
    private bool update = true;
    private float updateTimer;
    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins");
        coinstext.GetComponent<Text>().text = "" + coins;
        anim = GetComponent<Animator>();
        skinChoosen = PlayerPrefs.GetInt("Skin");
        purchased0 = PlayerPrefs.GetInt("Purchased0");
        purchased2 = PlayerPrefs.GetInt("Purchased2");
        switch (skinChoosen)
        {
            case 0:
                skin0.SetActive(true);
                skinChoosing = skinChoosen;
                gameObjects[1].SetActive(false);
                skinsPlane.transform.position = new Vector3(skinsPlane.transform.position.x, skinsPlane.transform.position.y, 7.479f);
                anim.SetBool("Left", true);
                Debug.Log("0");
                break;
            case 1:
                defaultSkin.SetActive(true);
                skinsPlane.transform.position = new Vector3(skinsPlane.transform.position.x, skinsPlane.transform.position.y, 7.979f);
                anim.SetBool("Middle", true);
                anim.SetBool("Right", false);
                anim.SetBool("Left", false);
                Debug.Log("1");
                break;
            case 2:
                skin2.SetActive(true);
                skinChoosing = skinChoosen;
                gameObjects[0].SetActive(false);
                skinsPlane.transform.position = new Vector3(skinsPlane.transform.position.x, skinsPlane.transform.position.y, 8.479f);
                anim.SetBool("Right", true);
                Debug.Log("2");
                break;
            case 3:
                skin3.SetActive(true);
                skinChoosing = skinChoosen;
                gameObjects[0].SetActive(false);
                skinsPlane.transform.position = new Vector3(skinsPlane.transform.position.x, skinsPlane.transform.position.y, 8.479f);
                anim.SetBool("Right", true);
                Debug.Log("3");
                break;
            default:
                defaultSkin.SetActive(true);
                break;
        }
        gameObjects[4].SetActive(false);
    }

    public void Right()
    {
        if (skinChoosing < 2)
        {
            if (skinChoosing == 0)
            {
                anim.SetBool("Middle", true);
                gameObjects[3].SetActive(false);
                gameObjects[2].SetActive(false);
            }
            if (skinChoosing == 1)
            {
                anim.SetBool("Right", true);
                anim.SetBool("Middle", false);
                gameObjects[0].SetActive(false);
                skin3.SetActive(true);
                if (purchased2 == 0)
                {
                    gameObjects[3].SetActive(true);
                    gameObjects[4].SetActive(false);
                }
                else
                {
                    gameObjects[4].SetActive(true);
                }
                
            }
            skinChoosing++;
            skinsPlane.transform.position = new Vector3(skinsPlane.transform.position.x, skinsPlane.transform.position.y, skinsPlane.transform.position.z + 0.5f);
            anim.SetBool("Left" , false);
            gameObjects[1].SetActive(true);
        }
        FindObjectOfType<AudioManager>().Play("ButtonSound");
    }
    public void Left()
    {
        if (skinChoosing > 0)
        {
            if (skinChoosing == 1 )
            {
                anim.SetBool("Middle", false);
                anim.SetBool("Left", true);
                gameObjects[1].SetActive(false);
                if (purchased0 == 0)
                {
                    gameObjects[2].SetActive(true);
                }                
            }
            if (skinChoosing == 2)
            {
                anim.SetBool("Middle", true);
                gameObjects[3].SetActive(false);
                gameObjects[2].SetActive(false);
            }
            skinChoosing--;
            skinsPlane.transform.position = new Vector3(skinsPlane.transform.position.x, skinsPlane.transform.position.y, skinsPlane.transform.position.z - 0.5f);
            anim.SetBool("Right", false);
            gameObjects[0].SetActive(true);
            skin3.SetActive(false);
            gameObjects[4].SetActive(false);
        }
        FindObjectOfType<AudioManager>().Play("ButtonSound");
    }
    public void Choose()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        skinChoosen = skinChoosing;
        PlayerPrefs.SetInt("Skin" , skinChoosen); 
    }
    public void Purchase()
    {
        coins = PlayerPrefs.GetInt("Coins");
        if (skinChoosing == 0)
        {
            if (coins >= 500)
            {
                FindObjectOfType<AudioManager>().Play("ButtonSound");
                coins = coins - 500;
                PlayerPrefs.SetInt("Coins", coins);
                gameObjects[2].SetActive(false);
                purchased0 = 1;
                PlayerPrefs.SetInt("Purchased0", purchased0);
                coinstext.GetComponent<Text>().text = "" + coins;
            }
        }
        else
        {
            if (coins >= 1000)
            {
                FindObjectOfType<AudioManager>().Play("ButtonSound");
                coins = coins - 1000;
                PlayerPrefs.SetInt("Coins", coins);
                gameObjects[3].SetActive(false);
                gameObjects[4].SetActive(true);
                purchased2 = 1;
                PlayerPrefs.SetInt("Purchased2", purchased2);
                coinstext.GetComponent<Text>().text = "" + coins;
            }
        }
    }
    private void Update()
    {
        if (update == true)
        {
            
            if (skinChoosing == 2 && purchased2 == 1)
            {
                buyButton.SetActive(false);
                chooseButton.SetActive(true);
            }
            else
            {
                chooseButton.SetActive(false);
                buyButton.SetActive(true);
            }
            if (skinChoosing == 1 && purchased1 == 1)
            {
                buyButton.SetActive(false);
                chooseButton.SetActive(true);
            }
            if (skinChoosing == 0 && purchased0 == 1)
            {
                buyButton.SetActive(false);
                chooseButton.SetActive(true);
            } 
            if(skinChoosing == 3 && purchased3 == 1)
            {
                buyButton.SetActive(false);
                chooseButton.SetActive(true);
            }
            update = false;
        }
        updateTimer += Time.deltaTime;
        if(updateTimer > 0.5f)
        {
            updateTimer = 0f;
            update = true;
        }
    }
    public void Refresh()
    {
        purchased0 = 0;
        purchased2 = 0;
        PlayerPrefs.DeleteAll();
        skinChoosen = 1;
        PlayerPrefs.SetInt("Skin", skinChoosen);
        FindObjectOfType<AudioManager>().Play("ButtonSound");
    }
    public void change()
    {
        skin3.SetActive(true);
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        buyButton.SetActive(false);
        chooseButton.SetActive(true);
        purchased3 = 1;
        //purchased3 = 1;
        //PlayerPrefs.SetInt("Purchased3", purchased3);
        if (skinChoosing == 2)
        {
            skinChoosing = 3;
            anim.SetBool("Change", true);
        }
        else if (skinChoosing == 3)
        {
            skinChoosing = 2;
            anim.SetBool("Change", false);
        }
        
    }
}
