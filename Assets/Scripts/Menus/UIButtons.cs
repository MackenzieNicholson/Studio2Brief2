using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{

    public GameObject OptionsMenu;
    public GameObject StartMenu;

    // Start is called before the first frame update
    void Start()
    {
        OptionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        FishManager.earnedpoints = FishManager.earnedpoints + FishManager.totalPoints;
        FishManager.totalPoints = 0;
        SceneManager.GetActiveScene();
        SceneManager.LoadScene(2);
    }
    public void Resume()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Restart()
    {

    }
    public void OptionsExit()
    {
        SceneManager.LoadScene(0);
        
    }
    public void Options()
    {
        OptionsMenu.SetActive(true);
        StartMenu.SetActive(false);
    }
}
