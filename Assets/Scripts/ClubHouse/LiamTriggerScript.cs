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

    public PlayerMovement player;
    public Animator animator;
    public GameObject menu;

    public TextMeshProUGUI promptUI;
    public Image promptBack;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        promptBack.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Submit"))
        {
            if(inDoorTrigger== true)
            {
                SceneManager.LoadScene(3);
            }
            else if(inRodTrigger== true)
            {
                player.speed = 0;
                animator.SetTrigger("open");
            }
            else if(inClubTrigger== true)
            {
                player.speed = 0;
                animator.SetTrigger("open");
            }
            else if(inFishTrigger== true)
            {

            }

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (inDoorTrigger == true)
            {
                SceneManager.LoadScene(3);
            }
            else if (inRodTrigger == true)
            {
                player.speed = 0;
                animator.SetTrigger("open");
            }
            else if (inClubTrigger == true)
            {
                player.speed = 0;
                animator.SetTrigger("open");
            }
            else if (inFishTrigger == true)
            {

            }

        }

        if (inRodTrigger== true)
        {
            if (Input.GetButton("Cancel"))
            {
                animator.SetTrigger("close");
                player.speed = 100;
            }
            
        }
        else if (inClubTrigger == true)
        {
            if (Input.GetButton("Cancel"))
            {
                animator.SetTrigger("close");
                player.speed = 100;
            }
        }

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
            menu.SetActive(true);
        }
        else if(isClub == true)
        {
            inClubTrigger = true;
            Debug.Log("is in club upgrade");
            promptBack.enabled = true;
            promptUI.text = "Enter: Upgrade Club";
            menu.SetActive(true);
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
        menu.SetActive(false);
    }

    public void CloseMenu()
    {
        animator.SetTrigger("close");
        player.speed = 100;
    }
}
