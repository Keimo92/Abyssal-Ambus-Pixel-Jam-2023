using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject pauseText;
   // [SerializeField] GameObject trainingLevelText;


    AudioSource audioSource;

    public AudioClip bgm;
    public AudioClip bossMusic;


    public bool isPaused;

    [SerializeField] GameObject pausePanel;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = bgm;
        audioSource.Play();
    }
    void Start()
    {
        pauseText.SetActive(false);

        pausePanel.SetActive(false);

    }

    public void StartBossFight()
    {
        audioSource.clip = bossMusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)

            {
                pausePanel.SetActive(false);
                ResumeGame();
               // trainingLevelText.SetActive(true);

            }
            else
            {
                pausePanel.SetActive(true);
                PauseGame();
                //trainingLevelText?.SetActive(false);

            }
        }



    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("JariDev");
            Debug.Log("Change Scene");
        }
    }

    public void BackToMainMenu()
    {

        SceneManager.LoadScene("MainMenu");



    }

    public void StartGame()
    {

        SceneManager.LoadScene("JariDev");

        //trainingLevelText.SetActive(true);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        pauseText.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseText.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void TrainingLevel()
    {

        SceneManager.LoadScene("TrainingLevel");
        Time.timeScale = 1f;
    }

    public void PlayActualGame()
    {
        SceneManager.LoadScene("JariDev");
        Time.timeScale = 1f;
    }
}
