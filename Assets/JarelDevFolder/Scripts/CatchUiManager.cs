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
            switch(FishLibrary.fishID)
            {
                case 3:
                    dialogBoxText.text = "A boot. Doesn't look your size...";
                    break;
                case 4:
                    dialogBoxText.text = "A controller that got thrown too far...";
                    break;
                case 5:
                    dialogBoxText.text = "You've been visited by a wandering axolotl!";
                    break;
                default:
                    dialogBoxText.text = "Just some random junk...";
                    break;
            }
        }
        else
        {
            switch(FishLibrary.fishID)
            {
                case 0:
                    dialogBoxText.text = "You encountered a wild majestic carp! Did it... just try to splash?";
                    break;
                case 1:
                    dialogBoxText.text = "A bass has dropped!";
                    break;
                case 2:
                    dialogBoxText.text = "You caught a silverfish! Get it? Silver & fish? Like goldfish...?";
                    break;
                case 6:
                    dialogBoxText.text = "A neon koi lights up your line!";
                    break;
                case 7:
                    dialogBoxText.text = "The doctor sturgeon is in! Because it... sounds like surgeon...";
                    break;
                case 8:
                    dialogBoxText.text = "Life has given you a lemon[ade] bluegill!";
                    break;
                case 9:
                    dialogBoxText.text = "You caught a goldfish! Something something first place...";
                    break;
                case 10:
                    dialogBoxText.text = "A fish with a glorious mustache! A distinguished gentleman!";
                    break;
                case 11:
                    dialogBoxText.text = "PSA: Red Bullrouts do not give you wings!";
                    break;
                default:
                    dialogBoxText.text = "You caught a " + FishLibrary.fishNames[FishLibrary.fishID] + "!\n";
                    break;
            }
            
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
