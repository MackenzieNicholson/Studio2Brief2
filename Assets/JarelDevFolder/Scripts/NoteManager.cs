using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public RectTransform spriteRectTransform;
    float slideSpeed = 5f;

    public bool hasCollided = false;
    public bool hasLeft = false;

    float noteRadius;
    float noteDistance;

    bool longNote = false;

    GameObject parentKeynote;
    KeyCode parentKeyCtrl;
    ScoreManager scoreManager;

    int ratingScale = 0; //send to score manager; 0 - miss; 1 - bad; 2 - good; 3 - perfect

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.gameObject.name == "pipSpawnA")
        {
            parentKeynote = GameObject.Find("keynoteA");
            parentKeyCtrl = KeyCode.A;
        }
        else if (transform.parent.gameObject.name == "pipSpawnB")
        {
            parentKeynote = GameObject.Find("keynoteB");
            parentKeyCtrl = KeyCode.S;
        }
        else if (transform.parent.gameObject.name == "pipSpawnC")
        {
            parentKeynote = GameObject.Find("keynoteC");
            parentKeyCtrl = KeyCode.K;
        }
        else if (transform.parent.gameObject.name == "pipSpawnD")
        {
            parentKeynote = GameObject.Find("keynoteD");
            parentKeyCtrl = KeyCode.L;
        }

        scoreManager = GameObject.Find("scoreUI").GetComponent<ScoreManager>();


        if (longNote)
        {
            int noteSize = Random.Range(5, 10);
            //Debug.Log("Note size is " + noteSize);
            float noteSizeY = (float)noteSize;

            Vector3 currentScale = transform.localScale;
            currentScale.y = currentScale.y * noteSizeY;
            transform.localScale = currentScale;
        }

        Bounds bounds = GetComponent<SpriteRenderer>().bounds;
        noteRadius = bounds.size.y / 2;
    }

    void Update()
    {
        //downward movement
        Vector3 downwardMovement = Vector3.down * slideSpeed * Time.deltaTime;
        transform.position += downwardMovement;

        noteDistance = Vector3.Distance(gameObject.transform.position, parentKeynote.transform.position);

        if (hasCollided)
        {
            if (Input.GetKeyDown(parentKeyCtrl))
            {
                if (noteDistance > (noteRadius * 1.50f)) //miss
                {
                    ratingScale = 0;
                    scoreManager.ScoreUpdate(ratingScale);
                    Destroy(gameObject);
                }
                else if ((noteDistance <= (noteRadius * 1.50f)) && (noteDistance >= (noteRadius * 0.85f))) //perfect
                {
                    ratingScale = 3;
                    if (Input.GetKey(parentKeyCtrl))
                    {
                        scoreManager.ScoreUpdate(ratingScale);
                    }
                    //Destroy(gameObject);
                }
                else if ((noteDistance < (noteRadius * 0.85f)) && (noteDistance >= (noteRadius * 0.35f))) //good
                {
                    ratingScale = 2;
                    if (Input.GetKey(parentKeyCtrl))
                    {
                        scoreManager.ScoreUpdate(ratingScale);
                    }
                    //Destroy(gameObject);
                }
                else if ((noteDistance < (noteRadius * 0.35f))) //bad
                {
                    ratingScale = 1;
                    if (Input.GetKey(parentKeyCtrl))
                    {
                        scoreManager.ScoreUpdate(ratingScale);
                    }
                    //Destroy(gameObject);
                }
            }
            else if (Input.GetKeyUp(parentKeyCtrl))
            {
                if (noteDistance <= noteRadius) //bad
                {
                    ratingScale = 1;
                    scoreManager.ScoreUpdate(ratingScale);
                    Destroy(gameObject);
                }
            }
        }
    }
}
