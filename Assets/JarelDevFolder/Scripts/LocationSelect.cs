using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LocationSelect : MonoBehaviour
{
    public Image oceanImage;
    public Image pondImage;
    public Image riverImage;
    public Image clubhouseImage;

    public Button oceanButton;
    public Button pondButton;
    public Button riverButton;
    public Button clubhouseButton;
    // Start is called before the first frame update
    void Start()
    {
        switch (PlayerData.areaID)
        {
            case 0:
                riverButton.interactable = false;
                riverImage.color = Color.gray;
                oceanButton.interactable = false;
                oceanImage.color = Color.gray;
                break;
            case 1:
                riverButton.interactable = true;
                riverImage.color = Color.white;
                oceanButton.interactable = false;
                oceanImage.color = Color.gray;
                break;
            case 2:
                riverButton.interactable = true;
                riverImage.color = Color.white;
                oceanButton.interactable = true;
                oceanImage.color = Color.white;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    public void HoverOverPond()
    {

    }

    public void DisableButtonPond()
    {

    }

    public void EnableButtonPond()
    {

    }

    public void ClickButtonPond()
    {
        SceneManager.LoadScene("FishingPond");
    }
}
