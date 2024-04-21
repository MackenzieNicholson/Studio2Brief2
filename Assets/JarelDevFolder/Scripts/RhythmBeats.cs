using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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

    public int rhythmDiff = 1;

    List<GameObject> spawnColumns = new List<GameObject>();

    Image keynoteImageA;
    Image keynoteImageB;
    Image keynoteImageC;
    Image keynoteImageD;

    int selectSpawn = 0;
    int noteGap = 0;
    float noteGapF = 0f;

    public ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        keynoteImageA = playKeyA.GetComponent<Image>();
        keynoteImageB = playKeyB.GetComponent<Image>();
        keynoteImageC = playKeyC.GetComponent<Image>();
        keynoteImageD = playKeyD.GetComponent<Image>();

        spawnColumns.Add(columnA);
        spawnColumns.Add(columnB);
        spawnColumns.Add(columnC);
        spawnColumns.Add(columnD);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.beatPlaying)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                keynoteImageA.color = Color.yellow;
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                keynoteImageA.color = Color.white;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                keynoteImageB.color = Color.green;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                keynoteImageB.color = Color.white;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                keynoteImageC.color = Color.blue;
            }
            else if (Input.GetKeyUp(KeyCode.K))
            {
                keynoteImageC.color = Color.white;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                keynoteImageD.color = Color.red;
            }
            else if (Input.GetKeyUp(KeyCode.L))
            {
                keynoteImageD.color = Color.white;
            }
        }
    }

    public IEnumerator RhythmGameStart()
    {
        PlayerData.beatPlaying = true;
        int noteCount = 16 * PlayerData.rhythmDiff;
        scoreManager.totalNotes = noteCount;
        scoreManager.finishedNotes = 0;

        scoreManager.scoreMaxPerfect = noteCount * 6;
        scoreManager.scoreMaxGood = noteCount * 3;
        scoreManager.scoreMaxBad = noteCount;

        for (int i = 0; i < noteCount; i++)
        {
            if (PlayerData.beatPlaying)
            {
                int noteLength = Random.Range(1, 10);
                selectSpawn = Random.Range(0, 4);
                noteGap = Random.Range(0, 5);
                noteGapF = (float)noteGap + 0.5f;
                GameObject newNote = Instantiate(rhythmNote, spawnColumns[selectSpawn].transform.position, Quaternion.identity);
                newNote.transform.SetParent(spawnColumns[selectSpawn].transform);
                yield return new WaitForSeconds(noteGapF);
            }
            else
            {
                StopCoroutine(RhythmGameStart());
            }
        }
    }
}
