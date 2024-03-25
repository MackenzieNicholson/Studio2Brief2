using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

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

    public TextMeshProUGUI promptUI;


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

            }
            else if(inClubTrigger== true)
            {

            }
            else if(inFishTrigger== true)
            {

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isDoor == true)
        {
            inDoorTrigger = true;
            Debug.Log("is in door");
            promptUI.text = "Enter: Go Fishing";
        }
        else if(isRod == true)
        {
            inRodTrigger = true;
            Debug.Log("is in rod upgrade");
            promptUI.text = "Enter: Upgrade Gear";
        }
        else if(isClub == true)
        {
            inClubTrigger = true;
            Debug.Log("is in club upgrade");
            promptUI.text = "Enter: Upgrade Club";
        }
        else if(isFish == true)
        {
            inFishTrigger = true;
            Debug.Log("is in fish tank");
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
    }
}
