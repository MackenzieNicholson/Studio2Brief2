using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingVendorTrigger : MonoBehaviour
{
    public LiamTriggerScript triggerScript;
    bool inTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger)
        {
            if (Input.GetButton("Submit") || Input.GetKeyDown(KeyCode.E))
            {
                triggerScript.OpenFishVendor();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("is in rod upgrade");
        inTrigger = true;
        triggerScript.isRod = true;
        triggerScript.promptBack.enabled = true;
        triggerScript.promptUI.text = "(Enter) Fish & Gear";
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        triggerScript.isRod = false;
        triggerScript.promptBack.enabled = false;
        triggerScript.promptUI.text = "";
    }
}
