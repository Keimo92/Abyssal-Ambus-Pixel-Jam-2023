using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    [SerializeField] GameObject lastBossActive;
    [SerializeField] GameObject image;
    [SerializeField] Animator animator;
    [SerializeField] GameObject text;
    [SerializeField] GameObject player;
    [SerializeField] GameObject fov;

    void Start()
    {
        lastBossActive.SetActive(true);

        text.SetActive(false);

        animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(lastBossActive == null)
        {
            image.SetActive(true);
            animator.SetTrigger("GameEnd");
            StartCoroutine(ChangeSceneActivateImage());
            StartCoroutine(BackToMainMenu());
            player.SetActive(false);
            fov.SetActive(false);
        }
    }


    IEnumerator ChangeSceneActivateImage()
    {
        yield return new WaitForSeconds(3);
        text.SetActive(true);
    }

    IEnumerator BackToMainMenu()
    {
        yield return new WaitForSeconds(15);

        SceneManager.LoadScene("MainMenu");
    }
}
