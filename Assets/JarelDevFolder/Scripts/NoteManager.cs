using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    Image noteImage;
    public Sprite noteYellow;
    public Sprite noteGreen;
    public Sprite noteBlue;
    public Sprite noteOrange;


    public bool hasCollided = false;
    public bool hasLeft = false;
    //public GameObject helper;
    
    float slideSpeed = 250f;
    float noteEdgeUp;
    float noteEdgeDown;
    float noteRadius;
    bool longNote = false;
    bool activeNote = false;
    int noteSize;

    Animator ratingAnimator;
    GameObject accuracyUI;
    GameObject keyLine;
    GameObject keyLineGood;
    GameObject checkLine;
    RectTransform parentObject;
    KeyCode parentKeyCtrl;

    ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        noteImage = GetComponent<Image>();
        noteImage.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        if (transform.parent.gameObject.name == "pipSpawnA")
        {
            parentObject = GameObject.Find("pipSpawnA").GetComponent<RectTransform>();
            accuracyUI = GameObject.Find("hitRatingA");
            parentKeyCtrl = KeyCode.A;
            noteImage.sprite = noteYellow;
        }
        else if (transform.parent.gameObject.name == "pipSpawnB")
        {
            parentObject = GameObject.Find("pipSpawnB").GetComponent<RectTransform>();
            accuracyUI = GameObject.Find("hitRatingB");
            parentKeyCtrl = KeyCode.S;
            noteImage.sprite = noteGreen;
        }
        else if (transform.parent.gameObject.name == "pipSpawnC")
        {
            parentObject = GameObject.Find("pipSpawnC").GetComponent<RectTransform>();
            accuracyUI = GameObject.Find("hitRatingC");
            parentKeyCtrl = KeyCode.K;
            noteImage.sprite = noteBlue;
        }
        else if (transform.parent.gameObject.name == "pipSpawnD")
        {
            parentObject = GameObject.Find("pipSpawnD").GetComponent<RectTransform>();
            accuracyUI = GameObject.Find("hitRatingD");
            parentKeyCtrl = KeyCode.L;
            noteImage.sprite = noteOrange;
        }

        ratingAnimator = accuracyUI.GetComponent<Animator>();
        scoreManager = GameObject.Find("scoreUI").GetComponent<ScoreManager>();
        keyLine = GameObject.Find("keyLinePerfect");
        keyLineGood = GameObject.Find("keyLineGood");
        checkLine = GameObject.Find("checkLineImage");

        slideSpeed = 250 + (50 * PlayerData.rhythmDiff);

        noteSize = Random.Range(1, 10);
        if (noteSize > 5)
        {
            longNote = true;
            // This just expands the note into something longer
            float noteSizeY = (float)noteSize;

            noteImage.rectTransform.localScale = new Vector3(1f, (1f * noteSizeY), 1f);
        }

        // Calculate y-bounds
        Vector3[] corners = new Vector3[4];
        noteImage.rectTransform.GetWorldCorners(corners);

        float topY = corners[1].y; // Top-right corner's y-coordinate
        float bottomY = corners[0].y; // Bottom-left corner's y-coordinate

        Vector2 yBounds = new Vector2(topY, bottomY);
        noteRadius = (yBounds.x - yBounds.y) / 2f + 3f; // 3f is to give a wider margin of error when timing hits; any larger than 3f is no good and would probably break the challenge;

        noteEdgeUp = noteImage.rectTransform.position.y + noteRadius;
        noteEdgeDown = noteImage.rectTransform.position.y - noteRadius;

        int noteIndex = noteImage.rectTransform.GetSiblingIndex();

        if (noteImage.rectTransform.GetSiblingIndex() > 0) // Resolve issues with overlapping notes when a note is instantiated
        {
            Transform precedingNoteTransform = parentObject.GetChild(noteIndex - 1);
            RectTransform precedingNote = precedingNoteTransform.GetComponent<RectTransform>();
            NoteManager precedingNoteManager = precedingNoteTransform.GetComponent<NoteManager>();

            if (noteEdgeDown < precedingNoteManager.noteEdgeUp)
            {
                //Debug.Log("Resolving note overlap");
                Vector2 noteBoundA = new Vector2(noteImage.rectTransform.position.x, noteEdgeDown);
                Vector2 noteBoundB = new Vector2(precedingNoteManager.noteImage.rectTransform.position.x, precedingNoteManager.noteEdgeUp);
                float noteDistance = Vector2.Distance(noteBoundA, noteBoundB);
                float newPos = noteImage.rectTransform.position.y + noteDistance + 0.1f;
                Vector2 adjustPos = new Vector3(noteImage.rectTransform.position.x, newPos);
                noteImage.rectTransform.position = adjustPos;
                //Debug.Log("Overlap resolved");
            }
        }
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
        if (!PlayerData.beatPlaying)
        {
            Destroy(gameObject);
        }

        Vector3 downwardMovement = Vector3.down * slideSpeed * Time.deltaTime;
        transform.position += downwardMovement;

        noteEdgeUp = noteImage.rectTransform.position.y + noteRadius;
        noteEdgeDown = noteImage.rectTransform.position.y - noteRadius;

        if (noteEdgeDown <= checkLine.transform.position.y)
        {
            hasCollided = true;
        }

        if (hasCollided && activeNote)
        {
            if (Input.GetKeyDown(parentKeyCtrl))
            {
                if (noteEdgeDown > keyLine.transform.position.y) //miss
                {
                    scoreManager.finishedNotes++;
                    ratingAnimator.Play("ratingAnim_miss");
                    Destroy(gameObject);
                }
                else if ((noteEdgeDown <= keyLine.transform.position.y) && (noteEdgeDown > keyLineGood.transform.position.y)) //perfect
                {
                    if (longNote && Input.GetKey(parentKeyCtrl))
                    {
                        StartCoroutine(LongNotePerfect());
                    }
                    else
                    {
                        scoreManager.finishedNotes++;
                        ratingAnimator.Play("ratingAnim_perfect");
                        Destroy(gameObject);
                    }
                }
                else if ((noteEdgeDown <= keyLineGood.transform.position.y) && (transform.position.y > keyLine.transform.position.y)) //good
                {
                    if (longNote && Input.GetKey(parentKeyCtrl))
                    {
                        StartCoroutine(LongNoteGood());
                    }
                    else
                    {
                        scoreManager.finishedNotes++;
                        ratingAnimator.Play("ratingAnim_good");
                        Destroy(gameObject);
                    }
                }
                else if ((transform.position.y <= keyLineGood.transform.position.y) && (noteEdgeUp > keyLine.transform.position.y)) //bad
                {
                    if (longNote && Input.GetKey(parentKeyCtrl))
                    {
                        StartCoroutine(LongNoteBad());
                    }
                    else
                    {
                        scoreManager.finishedNotes++;
                        ratingAnimator.Play("ratingAnim_bad");
                        Destroy(gameObject);
                    }
                }
            }
            else if (Input.GetKeyUp(parentKeyCtrl))
            {
                if ((noteEdgeUp > keyLine.transform.position.y) && (noteEdgeDown < keyLine.transform.position.y)) //bad
                {
                    ratingAnimator.Play("ratingAnim_bad");
                }
            }

            /*///For troubleshooting
            if (noteEdgeDown > keyLine.transform.position.y) //miss
            {
                gameObject.GetComponent<Image>().color = Color.gray;
            }
            else if ((noteEdgeDown <= keyLine.transform.position.y) && (noteEdgeDown > keyLineGood.transform.position.y)) //perfect
            {
                gameObject.GetComponent<Image>().color = Color.yellow;
            }
            else if ((noteEdgeDown <= keyLineGood.transform.position.y) && (transform.position.y > keyLine.transform.position.y)) //good
            {
                gameObject.GetComponent<Image>().color = Color.blue;
            }
            else if ((transform.position.y <= keyLineGood.transform.position.y) && (noteEdgeUp > keyLine.transform.position.y)) //bad
            {
                gameObject.GetComponent<Image>().color = Color.red;
            }*/

            if (noteEdgeUp < keyLine.transform.position.y)
            {
                if (!Input.GetKey(parentKeyCtrl))
                {
                    ratingAnimator.Play("ratingAnim_miss");
                }
                scoreManager.finishedNotes++;
                Destroy(gameObject);
            }
        }
    }

    IEnumerator LongNotePerfect()
    {
        while (Input.GetKey(parentKeyCtrl) && (noteEdgeUp > keyLine.transform.position.y))
        {
            ratingAnimator.Play("ratingAnim_perfect");
            yield return null;
        }
    }

    IEnumerator LongNoteGood()
    {
        while (Input.GetKey(parentKeyCtrl) && (noteEdgeUp > keyLine.transform.position.y))
        {
            ratingAnimator.Play("ratingAnim_good");
            yield return null;
        }
    }

    IEnumerator LongNoteBad()
    {
        while (Input.GetKey(parentKeyCtrl) && (noteEdgeUp > keyLine.transform.position.y))
        {
            ratingAnimator.Play("ratingAnim_bad");
            yield return null;
        }
    }

}
