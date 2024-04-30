using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class AquariumManager : MonoBehaviour
{
    Animator animator;

    public GameObject statsUI;
    public GameObject nofishtext;
    public GameObject infoText;
    public GameObject aquarium;
    public GameObject cycleButtons;

    GameObject aquariumFish;
    public GameObject aquariumSpawn;

    public TextMeshProUGUI fishName;
    public TextMeshProUGUI fishText;

    int currentFish = 0;
    int aquariumInv = 0;

    bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        aquarium.SetActive(false);
        infoText.SetActive(false);
        nofishtext.SetActive(false);
        cycleButtons.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            if (isOpen)
            {
                CloseUI();
            }
        }
    }

    void SetCanvasFalse()
    {
        aquarium.SetActive(false);
    }

    void SetCanvasTrue()
    {
        isOpen = true;
        aquarium.SetActive(true);
    }

    void DisplayFishItem()
    {
        if (aquariumFish != null)
        {
            Destroy(aquariumFish);
        }
        aquariumFish = Instantiate(FishLibrary.fishImages[PlayerData.clubFishData[currentFish].fishID], aquariumSpawn.transform);

        fishText.text = "Quality: " + FishLibrary.fishQualityText[PlayerData.clubFishData[currentFish].fishQuality] +
                                "\n\nWeight: " + PlayerData.clubFishData[currentFish].fishWeight.ToString() + "kg" +
                                "\n\nSize: " + PlayerData.clubFishData[currentFish].fishSize.ToString() + "cm" + "\n\nValue: " + PlayerData.clubFishData[currentFish].fishValue;

        fishName.text = FishLibrary.fishNames[PlayerData.clubFishData[currentFish].fishID];
    }

    public void CycleNextFish()
    {
        if (currentFish < (aquariumInv - 1))
        {
            currentFish++;
            DisplayFishItem();
        }
    }

    public void CyclePreviousFish()
    {
        if (currentFish > 0)
        {
            currentFish--;
            DisplayFishItem();
        }
    }

    public void ShowUI()
    {
        aquariumInv = PlayerData.clubFishData.Count;

        if (aquariumInv > 0)
        {
            infoText.SetActive(true);
            DisplayFishItem();
            cycleButtons.SetActive(true);
        }
        else
        {
            infoText.SetActive(false);
            nofishtext.SetActive(true);
        }
    }

    public void CloseUI()
    {
        PlayerData.speed = 150f;
        isOpen = false;
        nofishtext.SetActive(false);
        fishText.text = "";
        fishName.text = "";
        cycleButtons.SetActive(false);
        if (aquariumFish != null)
        {
            Destroy(aquariumFish);
        }
        statsUI.SetActive(true);
        animator.Play("aquarium_close");
    }
}
