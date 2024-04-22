using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class FishingSection : MonoBehaviour
{
    public TextMeshProUGUI playerCastsText;
    public TextMeshProUGUI playerFishText;

    public Animator catchUIanimator;
    public Animator alertAnimator;
    public ScoreManager scoreManager;
    public RhythmBeats rhythmGame;
    public GameObject rhythmSet;
    public GameObject catchCanvas;

    public GameObject fishingAlert;
    public float vertSpeed = 65000f;
    public float horzSpeed = 1000f;

    int chanceToBite;
    int catchDiff = 1;
    bool isJunk = false;
    bool castWaiting = false;
    bool inWater = false; //for when there is a catch alert; see WaitToCatch coroutine

    GameObject fishingHook;
    public GameObject newCatch;
    
    Animator playerAnimator;
    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GameObject.Find("playerSprite").GetComponent<Animator>();
        fishingHook = GameObject.Find("hookDetector");
        isJunk = false;
        fishingAlert.SetActive(false);
        rhythmSet.SetActive(false);
        PlayerData.playerFish = 0;
        playerFishText.text = PlayerData.playerFish.ToString() + "/" + PlayerData.fishLimit.ToString();
        PlayerData.playerCasts = 12;
        playerCastsText.text = PlayerData.playerCasts.ToString() + "/" + PlayerData.castLimit.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerData.beatPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!PlayerData.isInUI) //none of these trigger if true
                {
                    if (!PlayerData.isFishing)
                    {
                        if (PlayerData.playerCasts > 0)
                        {
                            if (PlayerData.playerFish < PlayerData.fishLimit)
                            {
                                PlayerData.isFishing = true;
                                playerAnimator.Play("player_cast");
                            }
                            else
                            {
                                Debug.Log("Carrying too much fish right now...");
                            }
                        }
                        else
                        {
                            Debug.Log("Need to fix the rod...");
                        }
                    }
                    else if (PlayerData.hasCatch)
                    {
                        inWater = false;
                        StopCoroutine(WaitToCatch());
                        fishingAlert.SetActive(false);
                        if (isJunk)
                        {
                            playerAnimator.Play("player_rod_catch");
                            StartCoroutine(GenerateCatch());
                        }
                        else
                        {
                            scoreManager.scoreCount = 100;
                            rhythmSet.SetActive(true);
                            playerAnimator.Play("player_rod_pull");
                            StartCoroutine(rhythmGame.RhythmGameStart());
                            StartCoroutine(RhythmScoreCheck());
                        }
                    }
                    else
                    {
                        castWaiting = false;
                        playerAnimator.Play("player_rod_catch");
                    }
                }
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
        else if (PlayerData.tossBack)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Fish"))
            {
                PlayerData.tossBack = false;
                Debug.Log("Fish Removed from scene");
                Destroy(other);
            }
        }
    }

    void NewCatch()
    {
        PlayerData.isInUI = true;
        playerAnimator.Play("player_rod_catch");
        PlayerData.beatPlaying = false;
        rhythmSet.SetActive(false);
        StartCoroutine(GenerateCatch());
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
                FishLibrary.fishID = Random.Range(0, 6);
                if (FishLibrary.fishID < 0) //in case random value becomes bugged, set to lowest; hopefully useful
                {
                    FishLibrary.fishID = 0;
                }
                catchDiff = 1;
                switch (FishLibrary.fishID)
                {
                    case 3:
                        isJunk = true;
                        FishLibrary.fishQuality = 3;
                        break;
                    case 4:
                        isJunk = true;
                        FishLibrary.fishQuality = 3;
                        break;
                    case 5:
                        isJunk = true;
                        FishLibrary.fishQuality = 3;
                        break;
                    default:
                        isJunk = false;
                        break;
                }
            }
            else if (chanceToBite >= 80 && chanceToBite <= 94) //rare fish
            {
                FishLibrary.fishID = Random.Range(6, 9);
                if (FishLibrary.fishID < 0) //in case random value becomes bugged, set to lowest; hopefully useful
                {
                    FishLibrary.fishID = 6;
                }
                catchDiff = 2;
                PlayerData.CatchDiffMod(catchDiff); //pass into PlayerData to cache difficulty for next rhythm minigame
            }
            else if (chanceToBite >= 95 && chanceToBite <= 99) //exotic fish
            {
                FishLibrary.fishID = Random.Range(9, 12);
                if (FishLibrary.fishID < 0) //in case random value becomes bugged, set to lowest; hopefully useful
                {
                    FishLibrary.fishID = 9;
                }
                catchDiff = 3;
                PlayerData.CatchDiffMod(catchDiff);
            }
            if (chanceToBite > 29) //as soon as there's a "bite", this includes junk
            {
                PlayerData.playerCasts--;
                playerCastsText.text = PlayerData.playerCasts.ToString() + "/" + PlayerData.castLimit.ToString();
                Debug.Log("There's a bite: " + chanceToBite + " Pool: " + FishLibrary.fishID);
                fishingAlert.SetActive(true); //alert UI pops up
                alertAnimator.Play("catchAlert_start");
                PlayerData.hasCatch = true; //player can press Q to reel in catch
                StartCoroutine(WaitToCatch()); //timer for player to reel in catch before it escapes
                castWaiting = false;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator WaitToCatch() 
    {
        playerAnimator.Play("player_rod_tension");
        float castTimer = 2.0f + PlayerData.bobberID;
        yield return new WaitForSeconds(castTimer);
        if (inWater)
        {
            PlayerData.hasCatch = false;
            inWater = false;
            fishingAlert.SetActive(false);
            playerAnimator.Play("player_rod_catch");
            Debug.Log("Fish got away!");
        }
    }

    IEnumerator GenerateCatch()
    {
        if (isJunk)
        {
            newCatch = Instantiate(FishLibrary.fishObjects[FishLibrary.fishID], fishingHook.transform.position, Quaternion.identity);
            rb = newCatch.GetComponent<Rigidbody>();
            isJunk = false;
        }
        else
        {
            newCatch = Instantiate(FishLibrary.fishObjects[FishLibrary.fishID], fishingHook.transform.position, Quaternion.identity);
            rb = newCatch.GetComponent<Rigidbody>();
            FishLibrary.fishWeight = FishLibrary.GetWeight();
            FishLibrary.fishSize = FishLibrary.GetSize();
            FishLibrary.fishValue = FishLibrary.GetValue();
        }

        while (newCatch.transform.position.y < transform.position.y)
        {
            rb.AddForce(Vector3.up * vertSpeed * Time.deltaTime);
            rb.AddForce(Vector3.left * horzSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        Debug.Log("Enabling canvas");
        catchCanvas.SetActive(true);
        PlayerData.isInUI = true;
        catchUIanimator.Play("catchUI_start");
    }

    IEnumerator RhythmScoreCheck()
    {
        while (PlayerData.beatPlaying)
        {
            if (scoreManager.finishedNotes >= (scoreManager.totalNotes / 2))
            {
                playerAnimator.Play("player_rod_pull_high");
            }

            if (scoreManager.finishedNotes == scoreManager.totalNotes)
            {
                if (scoreManager.scoreCount >= (scoreManager.scoreMaxPerfect - 100))
                {
                    FishLibrary.fishQuality = 2;
                    NewCatch();
                }
                else if ((scoreManager.scoreCount >= (scoreManager.scoreMaxGood - 100)) && (scoreManager.scoreCount < (scoreManager.scoreMaxPerfect - 100)))
                {
                    FishLibrary.fishQuality = 1;
                    NewCatch();
                }
                else if ((scoreManager.scoreCount > 0) && (scoreManager.scoreCount < (scoreManager.scoreMaxGood - 100)))
                {
                    FishLibrary.fishQuality = 0;
                    NewCatch();
                }
                scoreManager.scoreCount = 0;
            }
            else if (scoreManager.scoreCount <= 0)
            {
                playerAnimator.Play("player_rod_catch");
                scoreManager.scoreCount = 0;
                rhythmSet.SetActive(false);
                PlayerData.beatPlaying = false;
            }

            yield return null;
        }
    }
}
