using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClubManager : MonoBehaviour
{
    public TextMeshProUGUI rodText;
    public TextMeshProUGUI clubText;
    public TextMeshProUGUI fishText;
    public TextMeshProUGUI doorText;

    public Image rodImage;
    public Image clubImage;
    public Image fishImage;
    public Image doorImage;

    public Image[] tutorialImages;
    public int activeImage;

    public TextMeshProUGUI[] tutorialText;
    public int activeText;

    public GameObject tutorialMenu;
    public static bool tutorialSeen;

    public GameObject playerClubStats;

    // Start is called before the first frame update
    void Start()
    {
        if (tutorialSeen == false)
        {
            playerClubStats.SetActive(false);
            tutorialMenu.SetActive(true);
            PlayerData.speed = 0;
        }

        rodImage.enabled = false;
        clubImage.enabled = false;
        fishImage.enabled = false;
        doorImage.enabled = false;

        rodText.enabled = false;
        clubText.enabled = false;
        fishText.enabled = false;
        doorText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Next()
    {
        if (activeImage != tutorialImages.Length-1)
        {
            tutorialImages[activeImage].enabled = false;
            activeImage += 1;
            tutorialImages[activeImage].enabled = true;
        }

        if (activeText != tutorialText.Length-1)
        {
            tutorialText[activeText].enabled = false;
            activeText += 1;
            tutorialText[activeText].enabled = true;
        }
    }

    public void Back()
    {
        if (activeImage != 0)
        {
            tutorialImages[activeImage].enabled = false;
            activeImage -= 1;
            tutorialImages[activeImage].enabled = true;
        }

        if (activeText != 0)
        {
            tutorialText[activeText].enabled = false;
            activeText -= 1;
            tutorialText[activeText].enabled = true;
        }
    }

    public void Close()
    {
        playerClubStats.SetActive(true);
        tutorialMenu.SetActive(false);
        tutorialSeen = true;
        PlayerData.speed = 150f;
    }
}
