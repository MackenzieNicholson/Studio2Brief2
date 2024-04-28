using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEditor.Search;

public class CatchUiManager : MonoBehaviour
{
    public TextMeshProUGUI dialogBoxText;
    public TextMeshProUGUI ratingBoxText;
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

    GameObject newFishUI;

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
        newFishUI = Instantiate(FishLibrary.fishImages[FishLibrary.fishID], fishResultImage.transform);
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
        PlayerData.playerFish++;
        keptFishText.text = PlayerData.playerFish.ToString() + "/" + PlayerData.fishLimit.ToString();
        PlayerData.PassFishValues();
        Destroy(fishingGame.newCatch);
        CloseUI();
    }

    public void ReturnFish()
    {
        fishingGame.ReturnToSea();
        CloseUI();
    }

    public void CloseUI()
    {
        Debug.Log("Closing Canvas");
        if (FishLibrary.fishQuality == 3)
        {
            Destroy(fishingGame.newCatch);
        }
        continueButtonUI.SetActive(false);
        keepButtonUI.SetActive(false);
        returnButtonUI.SetActive(false);
        dialogTextUI.SetActive(false);
        ratingTextUI.SetActive(false);
        SetUItoFalse();
        //animator.Play("catchUI_close");
    }

    void SetUItoFalse()
    {
        PlayerData.isFishing = false;
        PlayerData.hasCatch = false;
        fishOptionsUI.SetActive(false);
        Destroy(newFishUI);
        gameObject.SetActive(false);
    }
}
