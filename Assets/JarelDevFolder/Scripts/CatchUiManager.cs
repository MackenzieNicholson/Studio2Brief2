using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatchUiManager : MonoBehaviour
{
    public TextMeshProUGUI dialogBoxText;
    public TextMeshProUGUI ratingBoxText;

    public GameObject dialogTextUI;
    public GameObject ratingTextUI;

    public GameObject carpImageUI;
    public GameObject goldfishImageUI;
    public GameObject koiImageUI;

    public GameObject fishResultImage;

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
        if ((FishLibrary.fishQuality > 2) || (FishLibrary.fishQuality < 0))
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
        if ((FishLibrary.fishQuality > 2) || (FishLibrary.fishQuality < 0))
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
    }

    void UpdateFishImage()
    {
        fishResultImage = Instantiate(FishLibrary.fishImages[FishLibrary.fishID], fishResultImage.transform);
    }
}
