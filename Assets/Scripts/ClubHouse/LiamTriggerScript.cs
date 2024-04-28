using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LiamTriggerScript : MonoBehaviour
{
    public bool isDoor;
    public bool isRod;
    public bool isClub;
    public bool isFish;

    private bool inDoorTrigger;
    private bool inRodTrigger;
    private bool inClubTrigger;
    private bool inFishTrigger;

    public Animator clubVendorAnimator;
    public GameObject vendorCanvasUI;
    public GameObject fishVendorUI;
    public GameObject clubVendorUI;
    public GameObject playerStatsUI;

    public TextMeshProUGUI promptUI;
    public Image promptBack;

    private void Start()
    {
        promptBack.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Submit") || Input.GetKeyDown(KeyCode.E))
        {
            if(inDoorTrigger== true)
            {
                SceneManager.LoadScene(3);
            }
            else if(inRodTrigger== true)
            {
                PlayerData.speed = 0f;
                playerStatsUI.SetActive(false);
                clubVendorAnimator.Play("vendorCanvas_fish_open");
                
            }
            else if(inClubTrigger== true)
            {
                PlayerData.speed = 0f;
                playerStatsUI.SetActive(false);
                clubVendorAnimator.Play("vendorCanvas_club_open");
            }
            else if(inFishTrigger== true)
            {
                /*PlayerData.speed = 0f;
                playerStatsUI.SetActive(false);*/
            }

        }

        /*if (inRodTrigger== true)
        {
            if (Input.GetButton("Cancel"))
            {
                fishVendorAnimator.Play("vendorCanvas_close");
                PlayerData.speed = 150f;
            }
            
        }
        else if (inClubTrigger == true)
        {
            if (Input.GetButton("Cancel"))
            {
                clubVendorAnimator.SetTrigger("close");
                PlayerData.speed = 150f;
            }
        }*/

    }

    private void OnTriggerEnter(Collider other)
    {
        if(isDoor == true)
        {
            inDoorTrigger = true;
            Debug.Log("is in door");
            promptBack.enabled = true;
            promptUI.text = "Enter: Go Fishing";
        }
        else if(isRod == true)
        {
            inRodTrigger = true;
            Debug.Log("is in rod upgrade");
            promptBack.enabled = true;
            promptUI.text = "Enter: Upgrade Gear";
        }
        else if(isClub == true)
        {
            inClubTrigger = true;
            Debug.Log("is in club upgrade");
            promptBack.enabled = true;
            promptUI.text = "Enter: Upgrade Club";
        }
        else if(isFish == true)
        {
            inFishTrigger = true;
            Debug.Log("is in fish tank");
            promptBack.enabled = true;
            promptUI.text = "Enter: Check Fish";
        }
    }


    private void OnTriggerExit(Collider other)
    {
        inDoorTrigger = false;
        inRodTrigger = false;
        inClubTrigger = false;
        inFishTrigger = false;
        promptUI.text = "";
        promptBack.enabled = false;
        //menu.SetActive(false);
    }

    /*public void CloseMenu()
    {
        animator.SetTrigger("close");
        PlayerMovement.speed = 100;
    }*/
}
