using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubVendorTrigger : MonoBehaviour
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
                triggerScript.OpenClubVendor();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("is in club upgrade");
        inTrigger = true;
        triggerScript.isClub = true;
        triggerScript.promptBack.enabled = true;
        triggerScript.promptUI.text = "(Enter) Clubhouse";
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        triggerScript.isClub = false;
        triggerScript.promptBack.enabled = false;
        triggerScript.promptUI.text = "";
    }
}
