using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
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
    public void Options()
    {

    }
}
