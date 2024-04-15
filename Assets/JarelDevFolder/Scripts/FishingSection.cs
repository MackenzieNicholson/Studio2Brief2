using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSection : MonoBehaviour
{
    Animator playerAnimator;
    public Animator alertAnimator;
    PlayerMovement playerMovement;
    public RhythmBeats rhythmGame;
    public GameObject rhythmSet;

    int chanceToBite;
    int fishID;
    int selectFromPool;
    bool hasCatch = false;
    bool isJunk = false;
    bool castWaiting = false;

    public GameObject carp;
    public GameObject koi;
    public GameObject goldfish;
    public GameObject junkBoot;

    public GameObject fishingAlert;

    GameObject fishingHook;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GameObject.Find("playerSprite").GetComponent<Animator>();
        playerMovement = GameObject.Find("player").GetComponent<PlayerMovement>();
        fishingHook = GameObject.Find("hookDetector");
        fishingAlert.SetActive(false);
        alertAnimator.StopPlayback();
        rhythmSet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(!playerMovement.isFishing)
            {
                Debug.Log("Fishing start!");
                playerMovement.isFishing = true;
                playerAnimator.Play("player_cast");
            }
            else if (hasCatch)
            {
                if (isJunk)
                {
                    GameObject junkCatch = Instantiate(junkBoot, fishingHook.transform.position, Quaternion.identity);
                    junkCatch.transform.SetParent(fishingHook.transform);
                    playerAnimator.Play("player_rod_catch");
                    alertAnimator.StopPlayback();
                    fishingAlert.SetActive(false);
                    isJunk = false;
                }
                else
                {
                    rhythmSet.SetActive(true);
                    StartCoroutine(rhythmGame.RhythmGameStart());
                }
            }
            else
            {
                castWaiting = false;
                playerAnimator.Play("player_rod_catch");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hook"))
        {
            Debug.Log("Hook in the water");
            StartCoroutine(SpawnTablePond());
        }
    }

    IEnumerator SpawnTablePond()
    {
        castWaiting = true;
        while (castWaiting)
        {
            Debug.Log("Waiting for catch");
            chanceToBite = Random.Range(0, 100);
            if (chanceToBite >= 30 && chanceToBite <= 69) //common fish and junk pool
            {
                selectFromPool = Random.Range(0, 6);
                rhythmGame.rhythmDiff = 1;
                switch (selectFromPool)
                {
                    case 1:
                        isJunk = true;
                        break;
                    case 3:
                        isJunk = true;
                        break;
                    case 5:
                        isJunk = true;
                        break;
                    default:
                        isJunk = false;
                        break;
                }
            }
            else if (chanceToBite >= 70 && chanceToBite <= 84) //rare pool
            {
                selectFromPool = Random.Range(6, 3);
                rhythmGame.rhythmDiff = 2;
                switch (selectFromPool)
                {
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                }
            }
            else if (chanceToBite >= 85 && chanceToBite <= 94) //exotic pool
            {
                selectFromPool = Random.Range(9, 3);
                rhythmGame.rhythmDiff = 3;
                switch (selectFromPool)
                {
                    case 9:
                        break;
                    case 10:
                        break;
                    case 11:
                        break;
                }
            }
            else if (chanceToBite >= 95 && chanceToBite <= 99) //legendary
            {
                selectFromPool = 12;
                rhythmGame.rhythmDiff = 4;
            }
            if (chanceToBite > 29)
            {
                Debug.Log("There's a bite: " + chanceToBite);
                fishingAlert.SetActive(true);
                alertAnimator.Play("catchAlert_start");
                hasCatch = true;
                castWaiting = false;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
}
