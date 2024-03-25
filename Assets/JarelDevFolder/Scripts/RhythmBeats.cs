using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RhythmBeats : MonoBehaviour
{
    public Image playKeyA;
    public Image playKeyB;
    public Image playKeyC;
    public Image playKeyD;
    public Image rhythmNote;

    public GameObject columnA;
    public GameObject columnB;
    public GameObject columnC; 
    public GameObject columnD;

    List<GameObject> spawnColumns = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        RhythmGameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Keypress: A");
            playKeyA.color = Color.yellow;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            if(!Input.GetKey(KeyCode.Space))
            {
                playKeyA.color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Keypress: S");
            playKeyB.color = Color.green;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                playKeyB.color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Keypress: D");
            playKeyC.color = Color.blue;
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                playKeyC.color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Keypress: F");
            playKeyD.color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                playKeyD.color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Keypress: All");
            playKeyA.color = Color.yellow;
            playKeyB.color = Color.green;
            playKeyC.color = Color.blue;
            playKeyD.color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if(!Input.GetKey(KeyCode.A))
            {
                playKeyA.color = Color.white;
                Debug.Log("Not pressing A");
            }
            if(!Input.GetKey(KeyCode.S))
            {
                playKeyB.color = Color.white;
            }
            if (!Input.GetKey(KeyCode.K))
            {
                playKeyC.color = Color.white;
            }
            if (!Input.GetKey(KeyCode.L))
            {
                playKeyD.color = Color.white;
            }
        }
    }

    void RhythmGameStart()
    {
        int noteCount = Random.Range(0, 32);

    }
}
