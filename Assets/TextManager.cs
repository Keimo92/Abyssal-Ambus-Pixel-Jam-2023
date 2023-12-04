using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public  Text text;
    private float cooldown;

    
    
    void Start()
    {
      
      
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        text.enabled = cooldown > 0;
    }

   public void DisplayText(string newText,float newCooldown = 10.0f)
    {
        text.text = newText;
        cooldown = newCooldown;
    }


    public void HideText()
    {
       

        cooldown = 0;
    }
}
