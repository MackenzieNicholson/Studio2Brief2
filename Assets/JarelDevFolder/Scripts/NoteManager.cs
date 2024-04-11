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
    //public GameObject helper;
    
    float slideSpeed = 3f;
    float noteEdgeUp;
    float noteEdgeDown;
    float noteCenterUp;
    float noteCenterDown;
    float noteRadius;
    float noteCenterRadius;
    bool longNote = false;
    bool activeNote = false;
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
        if (noteSize > 5)
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
        noteRadius = (noteBounds.size.y / 2) + 0.25f; // 0.25f is to give the player a wider margin of error when timing hits; any larger than 0.25f is no good and would remove the challenge of timing hits;
        noteCenterRadius = (noteRadius / 4);

        ///
        /// Enable for visual aid; assign noteHelper prefab to helper GameObject reference
        ///
        /*noteEdgeUp = transform.position.y + noteRadius;
        noteEdgeDown = transform.position.y - noteRadius;

        Vector3 boundUpper = new Vector3(transform.position.x, noteEdgeUp, transform.position.z);
        Vector3 boundLower = new Vector3(transform.position.x, noteEdgeDown, transform.position.z);

        GameObject helperUp = Instantiate(helper, boundUpper, Quaternion.identity);;
        helperUp.transform.SetParent(gameObject.transform);
        GameObject helperDown = Instantiate(helper, boundLower, Quaternion.identity);
        helperDown.transform.SetParent(gameObject.transform);*/
    }

    void Update()
    {
        if (transform.GetSiblingIndex() == 0)
        {
            activeNote = true;
        }
        else
        {
            activeNote = false;
        }
        Vector3 downwardMovement = Vector3.down * slideSpeed * Time.deltaTime;
        transform.position += downwardMovement;

        noteEdgeUp = transform.position.y + noteRadius;
        noteEdgeDown = transform.position.y - noteRadius;
        noteCenterUp = transform.position.y + noteCenterRadius;
        noteCenterDown = transform.position.y - noteCenterRadius;

        if (hasCollided && activeNote)
        {
            if (Input.GetKeyDown(parentKeyCtrl))
            {
                if (noteEdgeDown > keyLine.transform.position.y) //miss
                {
                    scoreManager.ScoreMiss();
                    Destroy(gameObject);
                }
                else if ((noteEdgeDown < keyLine.transform.position.y) && (noteCenterDown > keyLine.transform.position.y)) //perfect
                {
                    if (longNote && Input.GetKey(parentKeyCtrl))
                    {
                        StartCoroutine(LongNotePerfect());
                    }
                    else
                    {
                        scoreManager.ScorePerfect();
                        Destroy(gameObject);
                    }
                }
                else if ((noteCenterDown < keyLine.transform.position.y) && (noteCenterUp > keyLine.transform.position.y)) //good
                {
                    if (longNote && Input.GetKey(parentKeyCtrl))
                    {
                        StartCoroutine(LongNoteGood());
                    }
                    else
                    {
                        scoreManager.ScoreGood();
                        Destroy(gameObject);
                    }
                }
                else if ((noteEdgeUp > keyLine.transform.position.y) && (noteCenterUp < keyLine.transform.position.y)) //bad
                {
                    if (longNote && Input.GetKey(parentKeyCtrl))
                    {
                        StartCoroutine(LongNoteBad());
                    }
                    else
                    {
                        scoreManager.ScoreBad();
                        Destroy(gameObject);
                    }
                }
            }
            else if (Input.GetKeyUp(parentKeyCtrl))
            {
                if ((noteEdgeUp > keyLine.transform.position.y) && (noteEdgeDown < keyLine.transform.position.y)) //bad
                {
                    scoreManager.ScoreBad();
                    Destroy(gameObject);
                }
            }

            ///For troubleshooting
            /*if (noteEdgeDown > keyLine.transform.position.y) //miss
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            }
            else if ((noteEdgeDown < keyLine.transform.position.y) && (noteCenterDown > keyLine.transform.position.y)) //perfect
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if ((noteCenterDown < keyLine.transform.position.y) && (noteCenterUp > keyLine.transform.position.y)) //good
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if ((noteEdgeUp > keyLine.transform.position.y) && (noteCenterUp < keyLine.transform.position.y)) //bad
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }*/

            if (noteEdgeUp < keyLine.transform.position.y)
            {
                if (!Input.GetKey(parentKeyCtrl))
                {
                    scoreManager.ScoreMiss();
                }
                Destroy(gameObject);
            }
        }
    }

    IEnumerator LongNotePerfect()
    {
        while (Input.GetKey(parentKeyCtrl) && (noteEdgeUp > keyLine.transform.position.y))
        {
            scoreManager.ScorePerfect();
            yield return null;
        }
    }

    IEnumerator LongNoteGood()
    {
        while (Input.GetKey(parentKeyCtrl) && (noteEdgeUp > keyLine.transform.position.y))
        {
            scoreManager.ScoreGood();
            yield return null;
        }
    }

    IEnumerator LongNoteBad()
    {
        while (Input.GetKey(parentKeyCtrl) && (noteEdgeUp > keyLine.transform.position.y))
        {
            scoreManager.ScoreBad();
            yield return null;
        }
    }

}
