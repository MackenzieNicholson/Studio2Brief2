using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LiamTriggerScript : MonoBehaviour
{
    public bool isDoor = false;
    public bool isRod = false;
    public bool isClub = false;
    public bool isFish = false;

    public Animator clubVendorAnimator;
    public Animator mapAnimator;
    public Animator aquariumAnimator;
    public GameObject vendorCanvasUI;
    public GameObject fishVendorUI;
    public GameObject clubVendorUI;
    public GameObject playerStatsUI;

    public TextMeshProUGUI promptUI;
    public Image promptBack;

    public GameObject promptButton;

    private void Start()
    {
        promptBack.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenFishVendor()
    {
        PlayerData.usingClubUI = true;
        PlayerData.speed = 0f;
        playerStatsUI.SetActive(false);
        clubVendorAnimator.Play("vendorCanvas_fish_open");
    }

    public void OpenAquarium()
    {
        PlayerData.usingClubUI = true;
        PlayerData.speed = 0f;
        playerStatsUI.SetActive(false);
        aquariumAnimator.Play("aquarium_open");
    }

    public void OpenClubVendor()
    {
        PlayerData.usingClubUI = true;
        PlayerData.speed = 0f;
        playerStatsUI.SetActive(false);
        clubVendorAnimator.Play("vendorCanvas_club_open");
    }

    public void OpenLocationSelect()
    {
        PlayerData.usingClubUI = true;
        PlayerData.speed = 0f;
        playerStatsUI.SetActive(false);
        mapAnimator.Play("mapCanvas_open");
    }

    public void PromptClick()
    {
        promptButton.SetActive(false);
        if (isDoor)
        {
            OpenLocationSelect();
        }
        else if (isRod)
        {
            OpenFishVendor();
        }
        else if (isClub)
        {
            OpenClubVendor();
        }
        else if (isFish)
        {
            OpenAquarium();
        }
    }
}
