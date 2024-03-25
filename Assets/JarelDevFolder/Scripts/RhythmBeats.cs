using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RhythmBeats : MonoBehaviour
{
    public GameObject playKeyA;
    public GameObject playKeyB;
    public GameObject playKeyC;
    public GameObject playKeyD;
    public GameObject rhythmNote;

    public GameObject columnA;
    public GameObject columnB;
    public GameObject columnC; 
    public GameObject columnD;

    public bool pressedKeyA = false;
    public bool pressedKeyB = false;
    public bool pressedKeyC = false;
    public bool pressedKeyD = false;

    public GameObject worldCanvas;

    List<GameObject> spawnColumns = new List<GameObject>();

    SpriteRenderer noteSpriteA;
    SpriteRenderer noteSpriteB;
    SpriteRenderer noteSpriteC;
    SpriteRenderer noteSpriteD;

    int selectSpawn = 0;
    int noteGap = 0;
    float noteGapF = 0f;

    // Start is called before the first frame update
    void Start()
    {
        noteSpriteA = playKeyA.GetComponent<SpriteRenderer>();
        noteSpriteB = playKeyB.GetComponent<SpriteRenderer>();
        noteSpriteC = playKeyC.GetComponent<SpriteRenderer>();
        noteSpriteD = playKeyD.GetComponent<SpriteRenderer>();

        spawnColumns.Add(columnA);
        spawnColumns.Add(columnB);
        spawnColumns.Add(columnC);
        spawnColumns.Add(columnD);

        StartCoroutine(RhythmGameStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Keypress: A");
            pressedKeyA = true;
            noteSpriteA.color = Color.yellow;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            if(!Input.GetKey(KeyCode.Space))
            {
                pressedKeyA = false;
                noteSpriteA.color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Keypress: S");
            pressedKeyB = true;
            noteSpriteB.color = Color.green;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                pressedKeyB = false;
                noteSpriteB.color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Keypress: D");
            pressedKeyC = true;
            noteSpriteC.color = Color.blue;
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                pressedKeyC = false;
                noteSpriteC.color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Keypress: F");
            pressedKeyD = true;
            noteSpriteD.color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                pressedKeyD = false;
                noteSpriteD.color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Keypress: All");
            pressedKeyA = true;
            noteSpriteA.color = Color.yellow;
            pressedKeyB = true;
            noteSpriteB.color = Color.green;
            pressedKeyC = true;
            noteSpriteC.color = Color.blue;
            pressedKeyD = true;
            noteSpriteD.color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if(!Input.GetKey(KeyCode.A))
            {
                pressedKeyA = false;
                noteSpriteA.color = Color.white;
                Debug.Log("Not pressing A");
            }
            if(!Input.GetKey(KeyCode.S))
            {
                pressedKeyB = false;
                noteSpriteB.color = Color.white;
            }
            if (!Input.GetKey(KeyCode.K))
            {
                pressedKeyC = false;
                noteSpriteC.color = Color.white;
            }
            if (!Input.GetKey(KeyCode.L))
            {
                pressedKeyD = false;
                noteSpriteD.color = Color.white;
            }
        }
    }

    IEnumerator RhythmGameStart()
    {
        int noteCount = Random.Range(16, 32);
        for (int i = 0; i < noteCount; i++)
        {
            selectSpawn = Random.Range(0, 4);
            noteGap = Random.Range(0, 5);
            noteGapF = (float)noteGap;
            GameObject newNote = Instantiate(rhythmNote, spawnColumns[selectSpawn].transform.position, Quaternion.identity);
            newNote.transform.SetParent(spawnColumns[selectSpawn].transform);
            yield return new WaitForSeconds(noteGapF);
        }
    }
}
