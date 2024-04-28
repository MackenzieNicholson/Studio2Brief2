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

    int chanceToBite;
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
        FishLibrary.isJunk = false;
        fishingAlert.SetActive(false);
        rhythmSet.SetActive(false);
        playerFishText.text = PlayerData.playerFish.ToString() + "/" + PlayerData.fishLimit.ToString();
        PlayerData.playerCasts = PlayerData.castLimit;
        playerCastsText.text = PlayerData.playerCasts.ToString() + "/" + PlayerData.castLimit.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(!PlayerData.isFishing)
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
                if (FishLibrary.isJunk)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hook"))
        {
            Debug.Log("Hook in the water");
            StartCoroutine(SpawnTablePond());
        }
    }

    void NewCatch()
    {
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
            chanceToBite = Random.Range(0, 201);
            if (chanceToBite < 0) //in case random value becomes bugged; hopefully useful
            {
                chanceToBite = 117;
            }
            else if (chanceToBite > 200)
            {
                chanceToBite = 199;
            }
            yield return new WaitForSeconds(5f);
            Debug.Log("Waiting for catch. Rolled chance: " + chanceToBite);
            FishLibrary.PondSpawnTable(chanceToBite);
            if (chanceToBite > FishLibrary.nospawn) //as soon as there's a "bite", this includes junk
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
        if (FishLibrary.isJunk)
        {
            newCatch = Instantiate(FishLibrary.fishObjects[FishLibrary.fishID], fishingHook.transform.position, Quaternion.identity);
            rb = newCatch.GetComponent<Rigidbody>();
            FishLibrary.isJunk = false;
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
            rb.AddForce(Vector3.up * PlayerData.vertSpeed * Time.deltaTime);
            rb.AddForce(Vector3.left * PlayerData.horzSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        Debug.Log("Enabling canvas");
        catchCanvas.SetActive(true);
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

    public void ReturnToSea()
    {
        StartCoroutine(TossFish());
    }
    IEnumerator TossFish()
    {
        Debug.Log("Sending fish back to sea");
        GameObject rejectFish = newCatch;
        while (rejectFish.transform.position.y < 11f)
        {
            rejectFish.GetComponent<Rigidbody>().AddForce(Vector3.up * PlayerData.vertSpeed * Time.deltaTime);
            rejectFish.GetComponent<Rigidbody>().AddForce(Vector3.right * PlayerData.horzSpeed * Time.deltaTime);
            yield return null;
        }
        while (rejectFish.transform.position.y > -10f)
        {
            rejectFish.GetComponent<Rigidbody>().AddForce(Vector3.right * PlayerData.horzSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(rejectFish);
    }
}
