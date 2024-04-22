using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadDifferentScene : MonoBehaviour
{
    public void FishingClub()
    {
        SceneManager.GetActiveScene();
        SceneManager.LoadScene(2);
    }
    public void Pond()
    {
        SceneManager.GetActiveScene();
        SceneManager.LoadScene("FishingPond");
    }
    
}
