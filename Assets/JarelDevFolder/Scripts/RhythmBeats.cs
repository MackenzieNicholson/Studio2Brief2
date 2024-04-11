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
            noteSpriteA.color = Color.yellow;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            noteSpriteA.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            noteSpriteB.color = Color.green;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            noteSpriteB.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            noteSpriteC.color = Color.blue;
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            noteSpriteC.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            noteSpriteD.color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            noteSpriteD.color = Color.white;
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
