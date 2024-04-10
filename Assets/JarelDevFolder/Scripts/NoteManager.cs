using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public RectTransform spriteRectTransform;
    public bool hasCollided = false;
    public bool hasLeft = false;
    
    float slideSpeed = 3f;
    float noteEdgeUp;
    float noteEdgeDown;
    float noteRadius;
    bool longNote = false;
    bool activeNote = true;
    int noteSize;

    SpriteRenderer spriteRenderer;
    Bounds noteBounds;
    GameObject parentKeynote;
    GameObject keyLine;
    KeyCode parentKeyCtrl;
    ScoreManager scoreManager;

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
        keyLine = GameObject.Find("keyLine");

        noteSize = Random.Range(1, 10);
        if (noteSize > 7)
        {
            longNote = true;
            //this just expands the note into something longer
            float noteSizeY = (float)noteSize;

            Vector3 currentScale = transform.localScale;
            currentScale.y = currentScale.y * noteSizeY;
            transform.localScale = currentScale;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        noteBounds = spriteRenderer.bounds;
        noteRadius = noteBounds.size.y / 2;
    }

    void Update()
    {
        //downward movement
        Vector3 downwardMovement = Vector3.down * slideSpeed * Time.deltaTime;
        transform.position += downwardMovement;

        noteEdgeUp = transform.position.y + noteRadius;
        noteEdgeDown = transform.position.y - noteRadius;

        if (hasCollided)
        {
            if (Input.GetKeyDown(parentKeyCtrl))
            {
                if (noteEdgeDown > keyLine.transform.position.y) //miss
                {
                    gameObject.SetActive(false);
                    scoreManager.ScoreMiss();
                    Destroy(gameObject);
                }
                else if ((noteEdgeDown < keyLine.transform.position.y) && (transform.position.y > keyLine.transform.position.y)) //perfect
                {
                    if (longNote)
                    {
                        scoreManager.ScorePerfect();
                    }
                    else
                    {
                        gameObject.SetActive(false);
                        scoreManager.ScorePerfect();
                        Destroy(gameObject);
                    }
                }
                else if ((transform.position.y < keyLine.transform.position.y) && (noteEdgeUp > keyLine.transform.position.y)) //good
                {
                    gameObject.SetActive(false);
                    scoreManager.ScoreGood();
                    Destroy(gameObject);
                }
                else if (noteEdgeUp < keyLine.transform.position.y) //bad
                {
                    gameObject.SetActive(false);
                    scoreManager.ScoreBad();
                    Destroy(gameObject);
                }
            }
            else if (Input.GetKeyUp(parentKeyCtrl))
            {
                if ((noteEdgeDown < keyLine.transform.position.y) && (noteEdgeUp > keyLine.transform.position.y)) //miss
                {
                    scoreManager.ScoreMiss();
                    Destroy(gameObject);
                }
            }

            if (noteEdgeDown > keyLine.transform.position.y) //miss
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            }
            else if ((noteEdgeDown < keyLine.transform.position.y) && (transform.position.y > keyLine.transform.position.y)) //perfect
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if ((transform.position.y < keyLine.transform.position.y) && (noteEdgeUp > keyLine.transform.position.y)) //good
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if (noteEdgeUp < keyLine.transform.position.y) //bad
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }

            if (noteEdgeUp < keyLine.transform.position.y)
            {
                activeNote = false;
                Destroy(gameObject);
            }
        }
    }

    IEnumerator LongNoteScore()
    {
        for (int i = 0; i < noteSize; i++)
        {
            if (activeNote)
            {
                scoreManager.ScorePerfect();
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
