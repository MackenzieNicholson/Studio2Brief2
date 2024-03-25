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

    List<GameObject> spawnColumns = new List<GameObject>();

    SpriteRenderer noteSpriteA;
    SpriteRenderer noteSpriteB;
    SpriteRenderer noteSpriteC;
    SpriteRenderer noteSpriteD;

    // Start is called before the first frame update
    void Start()
    {
        noteSpriteA = playKeyA.GetComponent<SpriteRenderer>();
        noteSpriteB = playKeyB.GetComponent<SpriteRenderer>();
        noteSpriteC = playKeyC.GetComponent<SpriteRenderer>();
        noteSpriteD = playKeyD.GetComponent<SpriteRenderer>();
        RhythmGameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Keypress: A");
            noteSpriteA.color = Color.yellow;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            if(!Input.GetKey(KeyCode.Space))
            {
                noteSpriteA.color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Keypress: S");
            noteSpriteB.color = Color.green;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                noteSpriteB.color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Keypress: D");
            noteSpriteC.color = Color.blue;
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                noteSpriteC.color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Keypress: F");
            noteSpriteD.color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                noteSpriteD.color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Keypress: All");
            noteSpriteA.color = Color.yellow;
            noteSpriteB.color = Color.green;
            noteSpriteC.color = Color.blue;
            noteSpriteD.color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if(!Input.GetKey(KeyCode.A))
            {
                noteSpriteA.color = Color.white;
                Debug.Log("Not pressing A");
            }
            if(!Input.GetKey(KeyCode.S))
            {
                noteSpriteB.color = Color.white;
            }
            if (!Input.GetKey(KeyCode.K))
            {
                noteSpriteC.color = Color.white;
            }
            if (!Input.GetKey(KeyCode.L))
            {
                noteSpriteD.color = Color.white;
            }
        }
    }

    void RhythmGameStart()
    {
        int noteCount = Random.Range(0, 32);

    }
}
