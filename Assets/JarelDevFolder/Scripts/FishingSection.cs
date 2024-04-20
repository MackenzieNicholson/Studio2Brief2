using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingSection : MonoBehaviour
{
    
    public Animator alertAnimator;
    public ScoreManager scoreManager;
    public RhythmBeats rhythmGame;
    public GameObject rhythmSet;

    public GameObject carp;
    public GameObject koi;
    public GameObject goldfish;
    public GameObject junkBoot;

    public GameObject fishingAlert;

    public float diffSpeed = 50f;

    int chanceToBite;
    int fishID;
    int selectFromPool = 0;
    int catchDiff = 1;
    bool hasCatch = false;
    bool isJunk = false;
    bool castWaiting = false;
    bool inWater = false; //for when there is a catch alert; see WaitToCatch coroutine

    GameObject fishingHook;
    GameObject newCatch;
    Animator playerAnimator;
    Rigidbody rb;

    PlayerMovement playerMovement;

    List<GameObject> pondFishList = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GameObject.Find("playerSprite").GetComponent<Animator>();
        playerMovement = GameObject.Find("player").GetComponent<PlayerMovement>();
        fishingHook = GameObject.Find("hookDetector");
        isJunk = false;
        fishingAlert.SetActive(false);
        alertAnimator.StopPlayback();
        rhythmSet.SetActive(false);

        pondFishList.Add(carp);
        pondFishList.Add(carp);
        pondFishList.Add(carp);
        pondFishList.Add(junkBoot);
        pondFishList.Add(junkBoot);
        pondFishList.Add(junkBoot);
        pondFishList.Add(koi);
        pondFishList.Add(koi);
        pondFishList.Add(koi);
        pondFishList.Add(goldfish);
        pondFishList.Add(goldfish);
        pondFishList.Add(goldfish);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(!playerMovement.isFishing)
            {
                Debug.Log("Fishing start!");
                playerMovement.isFishing = true; //reset to false via keyframe event at the end of "player_rod_catch" animation
                playerAnimator.Play("player_cast");
            }
            else if (hasCatch)
            {
                inWater = false;
                StopCoroutine(WaitToCatch());
                alertAnimator.StopPlayback();
                fishingAlert.SetActive(false);
                if (isJunk)
                {
                    playerAnimator.Play("player_rod_catch");
                    StartCoroutine(GenerateCatch());
                }
                else
                {
                    scoreManager.scoreCount = 100;
                    diffSpeed = 50f * rhythmGame.rhythmDiff;
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
        if (PlayerData.beatPlaying)
        {
            if (scoreManager.finishedNotes == scoreManager.totalNotes)
            {
                /*if (scoreManager.scoreCount >= scoreManager.scoreMaxPerfect)
                {
                    playerAnimator.Play("player_rod_catch");
                    rhythmGame.beatsStart = false;
                }
                else if (scoreManager.scoreCount >= scoreManager.scoreMaxGood)
                {
                    playerAnimator.Play("player_rod_catch");
                    rhythmGame.beatsStart = false;
                }*/
                if (scoreManager.scoreCount >= scoreManager.scoreMaxBad)
                {
                    playerAnimator.Play("player_rod_catch");
                    StartCoroutine(GenerateCatch());
                    PlayerData.beatPlaying = false;
                    rhythmSet.SetActive(false);
                }
                else if (scoreManager.scoreCount >= 0)
                {
                    playerAnimator.Play("player_rod_catch");
                    PlayerData.beatPlaying = false;
                    rhythmSet.SetActive(false);
                }
                scoreManager.scoreCount = 0;
            }
            else if (scoreManager.scoreCount <= 0)
            {
                playerAnimator.Play("player_rod_catch");
                scoreManager.scoreCount = 0;
                PlayerData.beatPlaying = false;
                rhythmSet.SetActive(false);
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
        inWater = true;
        castWaiting = true;
        while (castWaiting)
        {
            Debug.Log("Waiting for catch");
            chanceToBite = Random.Range(0, 100);
            chanceToBite = PlayerData.CatchChanceMod(chanceToBite); //dependent on active lure
            yield return new WaitForSeconds(5f);
            if (chanceToBite >= 45 && chanceToBite <= 79) //common fish and junk pool
            {
                selectFromPool = Random.Range(0, 6);
                catchDiff = 1;
                switch (selectFromPool)
                {
                    case 3:
                        isJunk = true;
                        break;
                    case 4:
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
            else if (chanceToBite >= 80 && chanceToBite <= 94) //rare fish
            {
                selectFromPool = Random.Range(6, 9);
                catchDiff = 3;
                PlayerData.CatchDiffMod(catchDiff); //pass into PlayerData to cache difficulty for next rhythm minigame
            }
            else if (chanceToBite >= 95 && chanceToBite <= 99) //exotic fish
            {
                selectFromPool = Random.Range(9, 12);
                catchDiff = 5;
                PlayerData.CatchDiffMod(catchDiff);
            }
            if (chanceToBite > 29) //as soon as there's a "bite", this includes junk
            {
                Debug.Log("There's a bite: " + chanceToBite + " Pool: " + selectFromPool);
                fishingAlert.SetActive(true); //alert UI pops up
                alertAnimator.Play("catchAlert_start");
                hasCatch = true; //player can press Q to reel in catch
                StartCoroutine(WaitToCatch()); //timer for player to reel in catch before it escapes
                castWaiting = false;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator WaitToCatch() 
    {
        float castTimer = 2.0f + PlayerData.bobberID;
        yield return new WaitForSeconds(castTimer);
        if (inWater)
        {
            hasCatch = false;
            inWater = false;
            alertAnimator.StopPlayback();
            fishingAlert.SetActive(false);
            playerAnimator.Play("player_rod_catch");
            Debug.Log("Fish got away!");
        }
    }

    IEnumerator GenerateCatch()
    {
        if (selectFromPool < 0) //in case random value becomes less than 0 because of bugs; hopefully useful
        {
            selectFromPool = 0;
        }
        if (isJunk)
        {
            newCatch = Instantiate(junkBoot, fishingHook.transform.position, Quaternion.identity);
            rb = newCatch.GetComponent<Rigidbody>();
            isJunk = false;
        }
        else
        {
            newCatch = Instantiate(pondFishList[selectFromPool], fishingHook.transform.position, Quaternion.identity);
            rb = newCatch.GetComponent<Rigidbody>();
            
        }
        while (newCatch.transform.position.y < transform.position.y)
        {
            rb.AddForce(Vector3.up * 150f);
            rb.AddForce(Vector3.left * 30f);
            yield return null;
        }
        hasCatch = false;
    }
}
