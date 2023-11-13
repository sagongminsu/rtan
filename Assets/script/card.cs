using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public AudioClip flip;
    public AudioSource audioSource;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void destroycard()
    {
        Invoke("destroycardInvoke", 1.0f);
    }

    void destroycardInvoke()
    {
        Destroy(gameObject);
    }

    public void closecard()
    {
        Invoke("closecardInvoke", 1.0f);
    }

    void closecardInvoke()
    {
        anim.SetBool("isopen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
    }

    public void opencard()
    {
        audioSource.PlayOneShot(flip);

        anim.SetBool("isopen", true);
        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);

        if   (gameManager.I.firstcard == null)
        {
            gameManager.I.firstcard = gameObject;
        }
        else
        {
            gameManager.I.secondcard = gameObject;
            gameManager.I.isMatched();
        }

    }


}
