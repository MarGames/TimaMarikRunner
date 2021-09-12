using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldTimer : MonoBehaviour
{
    public float cooldown;

    [HideInInspector] public bool isCoolDown;

    private Image shieldImage;
    public GameObject player;
    public GameObject shield;
    void Start()
    {
        shieldImage = GetComponent<Image>();
        isCoolDown = true;
    }

    void Update()
    {
        if (isCoolDown)
        {
            shieldImage.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (shieldImage.fillAmount <= 0)
            {
                shieldImage.fillAmount = 1;
                isCoolDown = false;
                //shield.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetTimer()
    {
        shieldImage.fillAmount = 1;
    }
}
