using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textdisplay : MonoBehaviour
{
    TextManager textManager;
    public string textToShow;

    public bool destroyCollider;
    private void Start()
    {
        textManager = FindObjectOfType<TextManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            
            if (destroyCollider)
            {
                textManager.DisplayText(textToShow);
                Destroy(gameObject);
            }

            else
            {
                textManager.DisplayText(textToShow,99999999999);
            }



        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!destroyCollider)
        {
            textManager.HideText();
        }
    }
}
