using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Panel : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [Header("GameObjects")]
    public GameObject Player0;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject slide;
    public GameObject jump;
    public GameObject jumpWay;

    public float timer;
    public float timer1;
    public bool animSlide;
    private bool left = false;
    private bool right = false;
    private float TimerChange;
    private int ways = 1;
    Vector3 force = new Vector3(0f, 71f, 0f);

    [Header("Jump")]
    public Transform feetPos;
    bool slide1;
    bool jump1;


    Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);

    public void OnBeginDrag(PointerEventData eventData)
    {
        if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
        {
            if (TimerChange == 0f)
            {
                if (eventData.delta.x > 0.1f)
                {
                    if (ways != 2)
                    {
                        ways++;
                        right = true;
                        
                    }

                }
                else
                {
                    if (ways != 0)
                    {
                        ways--;
                        left = true;
                        
                    }

                }
            }
        }
        else
        {
            if (eventData.delta.y > 0.1f && feetPos.position.y <= -0.97f)
            {
                FindObjectOfType<AudioManager>().Play("Jump");
                Player0.GetComponent<Rigidbody>().AddForce(force);
                Player1.GetComponent<Rigidbody>().AddForce(force);
                Player2.GetComponent<Rigidbody>().AddForce(force);
                Player3.GetComponent<Rigidbody>().AddForce(force);
                jump1 = true;
                jump.SetActive(true);
            }
            if (eventData.delta.y < -0.1f)
            {
                slide1 = true;
                slide.SetActive(true);
            }
        }
    }
    

    public void OnDrag(PointerEventData eventData)
    {
    }
    private void Update()
    {
        if (Player0.activeSelf)
            feetPos.position = Player0.transform.position;
        if (Player1.activeSelf)
            feetPos.position = Player1.transform.position;
        if (Player2.activeSelf)
            feetPos.position = Player2.transform.position;
        if (Player3.activeSelf)
            feetPos.position = Player3.transform.position;
        if (slide1 == true)
        {
            timer += Time.deltaTime;
        }
        if (timer > 1)
        {
            slide1 = false;
            slide.SetActive(false);
            timer = 0f;
        }
        if (jump1 == true)
        {
            timer1 += Time.deltaTime;
        }
        if (timer1 > 1.1)
        {
            jump1 = false;
            jump.SetActive(false);
            timer1 = 0f;
        }
        if (left)
        {
            jumpWay.SetActive(true);
            TimerChange += Time.deltaTime;
            Player0.transform.position = new Vector3(Player0.transform.position.x - 1.5f * Time.deltaTime, Player0.transform.position.y, Player0.transform.position.z);
            Player1.transform.position = new Vector3(Player1.transform.position.x - 1.5f * Time.deltaTime, Player1.transform.position.y, Player1.transform.position.z);
            Player2.transform.position = new Vector3(Player2.transform.position.x - 1.5f * Time.deltaTime, Player2.transform.position.y, Player2.transform.position.z);
            Player3.transform.position = new Vector3(Player3.transform.position.x - 1.5f * Time.deltaTime, Player3.transform.position.y, Player3.transform.position.z);
        }
        if (right)
        {
            jumpWay.SetActive(true);
            TimerChange += Time.deltaTime;
            Player0.transform.position = new Vector3(Player0.transform.position.x + 1.5f * Time.deltaTime, Player0.transform.position.y, Player0.transform.position.z);
            Player1.transform.position = new Vector3(Player1.transform.position.x + 1.5f * Time.deltaTime, Player1.transform.position.y, Player1.transform.position.z);
            Player2.transform.position = new Vector3(Player2.transform.position.x + 1.5f * Time.deltaTime, Player2.transform.position.y, Player2.transform.position.z);
            Player3.transform.position = new Vector3(Player3.transform.position.x + 1.5f * Time.deltaTime, Player3.transform.position.y, Player3.transform.position.z);
        }
        if (TimerChange > 0.24f)
        {
            jumpWay.SetActive(false);
            TimerChange = 0f;
            left = false;
            right = false;
        }
    }
}
