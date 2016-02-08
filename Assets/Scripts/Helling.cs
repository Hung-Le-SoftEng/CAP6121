using System;
using UnityEngine;

public class Helling : MonoBehaviour
{
    private GameObject LeftHand; // I create variable
    private GameObject Head; // I create variable
    private GameObject HealingAlpha;
    private GameObject Player;
    Vector3 OriginalPosition;
    private float timer = 3f;

    void Start()
    {
        //OriginalPosition = gameObject.transform.position;
        HealingAlpha = GameObject.Find("HealingAlpha");
        Player = GameObject.Find("Player");
        HealingAlpha.SetActive(false);
        //Move();
        //Turning();
    }
    void Update()
    {
        /*
        // If Right Hand is empty null it finding else it changing hand position
        if (LeftHand == null || Head == null)
        {
            LeftHand = GameObject.Find("HandLeft");
            Head = GameObject.Find("Head");
        }
        else
        {
            Debug.Log(LeftHand.transform.position);
            Debug.Log(Head.transform.position);
           // if (LeftHand.transform.position.y >= Head.transform.position.y)
           if(Player.currentHealth <= 50f)
            {
                HealingAlpha.SetActive(true);
                HealingAlpha.GetComponent<Animation>().Play();
                timer -= Time.deltaTime;
                if (timer < 0)
                {               
                    HealingAlpha.GetComponent<Animation>().Stop();
                    HealingAlpha.SetActive(false);
                }
            }
        }*/

        timer -= Time.deltaTime;

        // if (LeftHand.transform.position.y >= Head.transform.position.y)
        if (timer < 0)
        {
            HealingAlpha.SetActive(true);
            HealingAlpha.GetComponent<Animation>().Play();
            //timer -= Time.deltaTime;
            //if (timer < 0)
            //{
            //    HealingAlpha.GetComponent<Animation>().Stop();
            //    HealingAlpha.SetActive(false);
            //}
        }
    }
}
