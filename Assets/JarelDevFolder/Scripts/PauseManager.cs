using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;

    bool isRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isRunning && !PlayerData.usingClubUI)
            {
                isRunning = false;
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
            else if (!isRunning)
            {
                ExitPause();
            }
        }
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnToClubhouse()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ClubHouse");
    }

    public void ExitPause()
    {
        isRunning = true;
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }
}
