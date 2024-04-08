using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteDetect : MonoBehaviour
{
    public ScoreManager scoreManager;
    public NoteManager noteState;
    public RhythmBeats keypressCheck;

    GameObject noteObject;

    bool collisionCheckA = false;
    bool collisionCheckB = false;
    bool collisionCheckC = false;
    bool collisionCheckD = false;

    bool keycheckA = false;
    bool keycheckB = false;
    bool keycheckC = false;
    bool keycheckD = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            keycheckA = true;
            if (collisionCheckA)
            {
                scoreManager.scoreCount++;
                scoreManager.score.text = scoreManager.scoreCount.ToString();
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            if (collisionCheckA)
            {
                scoreManager.accuracy.text = "Bad!";
                Destroy(noteObject);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            keycheckB = true;
            if (collisionCheckB)
            {
                scoreManager.scoreCount++;
                scoreManager.score.text = scoreManager.scoreCount.ToString();
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (collisionCheckB)
            {
                scoreManager.accuracy.text = "Bad!";
                Destroy(noteObject);
            }
        }

        if (Input.GetKey(KeyCode.K))
        {
            keycheckC = true;
            if (collisionCheckC)
            {
                scoreManager.scoreCount++;
                scoreManager.score.text = scoreManager.scoreCount.ToString();
            }
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            if (collisionCheckC)
            {
                scoreManager.accuracy.text = "Bad!";
                Destroy(noteObject);
            }
        }

        if (Input.GetKey(KeyCode.L))
        {
            keycheckD = true;
            if (collisionCheckD)
            {
                scoreManager.scoreCount++;
                scoreManager.score.text = scoreManager.scoreCount.ToString();
            }
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            if (collisionCheckD)
            {
                scoreManager.accuracy.text = "Bad!";
                Destroy(noteObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Note passing");
        noteObject = other.gameObject;
        if (gameObject.name == "keynoteA")
        {
            collisionCheckA = true;
        }
        else if (gameObject.name == "keynoteB")
        {
            collisionCheckB = true;
        }
        else if (gameObject.name == "keynoteC")
        {
            collisionCheckC = true;
        }
        else if (gameObject.name == "keynoteD")
        {
            collisionCheckD = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Note has passed");
        if (gameObject.name == "keynoteA")
        {
            if (!keycheckA)
            {
                scoreManager.accuracy.text = "Miss!";
            }
            collisionCheckA = false;
        }
        else if (gameObject.name == "keynoteB")
        {
            if (!keycheckB)
            {
                scoreManager.accuracy.text = "Miss!";
            }
            collisionCheckB = false;
        }
        else if (gameObject.name == "keynoteC")
        {
            if (!keycheckC)
            {
                scoreManager.accuracy.text = "Miss!";
            }
            collisionCheckC = false;
        }
        else if (gameObject.name == "keynoteD")
        {
            if (!keycheckD)
            {
                scoreManager.accuracy.text = "Miss!";
            }
            collisionCheckD = false;
        }
        Destroy(noteObject);
    }
}
