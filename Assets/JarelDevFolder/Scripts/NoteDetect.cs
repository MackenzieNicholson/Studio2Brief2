using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteDetect : MonoBehaviour
{
    public ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Note passing");
        if(gameObject.name == "keynoteA")
        {
            if (Input.GetKey(KeyCode.A))
            {
                scoreManager.scoreCount++;
                scoreManager.score.text = scoreManager.scoreCount.ToString();
            }
        }
        else if (gameObject.name == "keynoteB")
        {
            if (Input.GetKey(KeyCode.S))
            {
                scoreManager.scoreCount++;
                scoreManager.score.text = scoreManager.scoreCount.ToString();
            }
        }
        else if (gameObject.name == "keynoteC")
        {
            if (Input.GetKey(KeyCode.K))
            {
                scoreManager.scoreCount++;
                scoreManager.score.text = scoreManager.scoreCount.ToString();
            }
        }
        else if (gameObject.name == "keynoteD")
        {
            if (Input.GetKey(KeyCode.L))
            {
                scoreManager.scoreCount++;
                scoreManager.score.text = scoreManager.scoreCount.ToString();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Note has passed");
    }
}
