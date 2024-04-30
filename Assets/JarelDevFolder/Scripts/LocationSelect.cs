using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;

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

    public GameObject mapCanvas;
    public GameObject statsUI;

    Animator animator;

    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        mapCanvas.SetActive(false);
        /*switch (PlayerData.areaID)
        {
            case 0:
                riverButton.interactable = false;
                oceanButton.interactable = false;
                break;
            case 1:
                riverButton.interactable = true;
                oceanButton.interactable = false;
                break;
            case 2:
                riverButton.interactable = true;
                oceanButton.interactable = true;
                break;
            default:
                break;
        }*/
    }

    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            if (isOpen)
            {
                ClickClose();
            }
        }
    }

    // Update is called once per frame
    public void ClickClose()
    {
        PlayerData.speed = 150f;
        statsUI.SetActive(true);
        animator.Play("mapCanvas_close");
    }
    void SetCanvasFalse()
    {
        isOpen = false;
        mapCanvas.SetActive(false);
    }
    void SetCanvasTrue()
    {
        isOpen = true;
        mapCanvas.SetActive(true);
    }
    public void ClickButtonPond()
    {
        SceneManager.LoadScene("FishingPond");
    }

    public void ClickButtonRiver()
    {

    }

    public void ClickButtonOcean()
    {

    }
}
