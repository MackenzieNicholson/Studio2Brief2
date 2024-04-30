using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumTrigger : MonoBehaviour
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
                triggerScript.OpenAquarium();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("is in aquarium");
        inTrigger = true;
        triggerScript.isFish = true;
        triggerScript.promptBack.enabled = true;
        triggerScript.promptUI.text = "(Enter) View Fish";
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        triggerScript.isFish = false;
        triggerScript.promptBack.enabled = false;
        triggerScript.promptUI.text = "";
    }
}
