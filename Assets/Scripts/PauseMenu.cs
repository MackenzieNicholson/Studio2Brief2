using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public FishManager fishmanager;

    public GameObject Options;
    public GameObject Pause;

    public bool NotOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        NotOpen = true;
        Pause.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
       
        
            
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("helpppp");
            Pause.SetActive(true);
            fishmanager.CanFish = false;
                
        }
        
    }
    public void PauseResume()
    {
        Pause.SetActive(false);
        fishmanager.CanFish = true;
    }
    public void PauseExit()
    {
        SceneManager.LoadScene(2);
        
    }
    public void OptionsMenu()
    {
        Options.SetActive(true);
        Pause.SetActive(false);
    }
}
