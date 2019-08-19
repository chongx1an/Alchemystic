using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public Image coverDeath;

    private bool isPlayedDeathSound;
    // Update is called once per frame


    private void Start()
    {
        pauseMenu.SetActive(false);
        isPlayedDeathSound = false;
    }
    void Update()
    {

        

        if (PlayerController.instance.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    AudioManagerController.instance.Play("PauseSound");
                    Pause();
                }
            }
        }
        else
        {

            if (!isPlayedDeathSound)
            {
                StartCoroutine("Death");
                AudioManagerController.instance.Play("PlayerDeath");
                isPlayedDeathSound = true;
            }

        }

        
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public IEnumerator Death()
    {
        Time.timeScale = 0.2f;
        while (coverDeath.color.a < 0.7f)
        {
            coverDeath.color = new Color(coverDeath.color.r, coverDeath.color.g, coverDeath.color.b, coverDeath.color.a + 0.01f);
            yield return new WaitForSeconds(0.02f);
        }
        deathMenu.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
}
