using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Search;

public class CatchUiManager : MonoBehaviour
{
    public TextMeshProUGUI dialogBoxText;
    public TextMeshProUGUI ratingBoxText;
    public TextMeshProUGUI fishingScoreText;
    public TextMeshProUGUI keptFishText;

    public GameObject dialogTextUI;
    public GameObject ratingTextUI;
    public GameObject fishOptionsUI;
    public GameObject continueButtonUI;
    public GameObject keepButtonUI;
    public GameObject returnButtonUI;

    public GameObject carpImageUI;
    public GameObject goldfishImageUI;
    public GameObject koiImageUI;

    public GameObject fishResultImage;

    public FishingSection fishingGame;

    List<GameObject> fishImageList = new List<GameObject>();

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dialogTextUI.SetActive(false);
        ratingTextUI.SetActive(false);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateDialogBox()
    {
        dialogTextUI.SetActive(true);
        if (FishLibrary.fishQuality == 3)
        {
            dialogBoxText.text = "Just some random junk...";
        }
        else
        {
            dialogBoxText.text = "You caught a " + FishLibrary.fishNames[FishLibrary.fishID] + "!\n";
        }
        
    }

    void UpdateFishRatingScale()
    {
        switch(FishLibrary.fishQuality)
        {
            case 0:
                animator.Play("fishQuality_bad");
                break;
            case 1:
                animator.Play("fishQuality_good");
                break;
            case 2:
                animator.Play("fishQuality_perfect");
                break;
            default:
                animator.Play("fishQuality_garbage");
                break;
        }
    }

    void UpdateRatingBox()
    {
        ratingTextUI.SetActive(true);
        if (FishLibrary.fishQuality == 3)
        {
            ratingBoxText.text = "Quality: " + FishLibrary.fishQualityText[FishLibrary.fishQuality] +
                                "\n\nWeight: --" +
                                "\n\nSize: --";
        }
        else
        {
            ratingBoxText.text = "Quality: " + FishLibrary.fishQualityText[FishLibrary.fishQuality] +
                                "\n\nWeight: " + FishLibrary.fishWeight.ToString() + "kg" +
                                "\n\nSize: " + FishLibrary.fishSize.ToString() + "cm" + "\n\nValue: " + FishLibrary.fishValue;
        }

        StartCoroutine(ShowOptions());
    }

    void UpdateFishImage()
    {
        fishResultImage = Instantiate(FishLibrary.fishImages[FishLibrary.fishID], fishResultImage.transform);
    }

    IEnumerator ShowOptions()
    {
        yield return new WaitForSeconds(1f);
        fishOptionsUI.SetActive(true);
        if (FishLibrary.fishQuality == 3)
        {
            continueButtonUI.SetActive(true);
            keepButtonUI.SetActive(false);
            returnButtonUI.SetActive(false);
        }
        else
        {
            continueButtonUI.SetActive(false);
            keepButtonUI.SetActive(true);
            returnButtonUI.SetActive(true);
        }
    }

    public void KeepFish()
    {
        PlayerData.playerScore += FishLibrary.fishValue;
        fishingScoreText.text = PlayerData.playerScore.ToString();
        keptFishText.text = PlayerData.playerFish.ToString() + "/" + PlayerData.fishLimit.ToString();
        Destroy(fishingGame.newCatch);
        CloseUI();
    }

    public void ReturnFish()
    {
        StartCoroutine(TossFish());
        CloseUI();
    }

    public void CloseUI()
    {
        if (FishLibrary.fishQuality == 3)
        {
            Destroy(fishingGame.newCatch);
        }
        dialogTextUI.SetActive(false);
        ratingTextUI.SetActive(false);
        animator.Play("catchUI_close");
    }

    void SetUItoFalse()
    {
        PlayerData.isInUI = false;
        PlayerData.isFishing = false;
        PlayerData.hasCatch = false;
        Destroy(fishResultImage);
        gameObject.SetActive(false);
    }

    IEnumerator TossFish()
    {
        while (fishingGame.newCatch.transform.position.y < 11.5f)
        {
            fishingGame.rb.AddForce(Vector3.up * 150f);
            fishingGame.rb.AddForce(Vector3.right * 50f);
            yield return null;
        }
    }
}
