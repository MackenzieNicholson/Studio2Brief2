using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
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
                triggerScript.OpenLocationSelect();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("is in map select");
        inTrigger = true;
        triggerScript.isDoor = true;
        triggerScript.promptBack.enabled = true;
        triggerScript.promptUI.text = "(Enter) Go fishing";
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        triggerScript.isDoor = false;
        triggerScript.promptBack.enabled = false;
        triggerScript.promptUI.text = "";
    }
}
