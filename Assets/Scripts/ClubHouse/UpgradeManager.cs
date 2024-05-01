using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public int upgradeCap;

    public TextMeshProUGUI clubScore;
    public TextMeshProUGUI clubMoney;
    public TextMeshProUGUI fishHaul;
    public TextMeshProUGUI renownText;
    public TextMeshProUGUI memberNum;
    public TextMeshProUGUI clubUpgradeMoney;

    public TextMeshProUGUI lureText;
    public TextMeshProUGUI rodText;
    public TextMeshProUGUI bucketText;
    public TextMeshProUGUI baitText;

    public GameObject vendorModeUI;
    public GameObject gearModeUI;
    public GameObject fishModeUI;
    public GameObject exitVendorUI;
    public GameObject noFishHelp;
    public GameObject cycleUI;
    public GameObject fishOptionsUI;
    public GameObject playerStatsUI;

    public TextMeshProUGUI clubScoreStat;
    public TextMeshProUGUI clubMoneyStat;
    public TextMeshProUGUI clubFishStat;

    public bool fishCanvasOpen = false;
    public bool clubCanvasOpen = false;

    int fishInventory = 0;
    int currentFishSelect = 0;

    public GameObject fishIconSpawn;
    public GameObject fishgearCanvas;
    public GameObject clubhouseCanvas;

    public TextMeshProUGUI fishDetails;
    public TextMeshProUGUI fishName;
    public TextMeshProUGUI clubOptions;
    public TextMeshProUGUI gearOptions;
    public TextMeshProUGUI vendorModeText;

    public Button selectHookUpgradeButton;
    public Button selectRodUpgradeButton;
    public Button selectBucketUpgradeButton;
    public Button selectBobberUpgradeButton;

    public Button wallButton;
    public Button floorButton;
    public Button aquariumButton;
    public Button membershipButton;
    public Button transpoButton;

    public TextMeshProUGUI transpoButtonText;

    public Material wallMaterial;
    public Texture wallBrokenTxtr;
    public Texture wallFixedTxtr;
    public Texture wallNewTxtr;

    public Material floorMaterial;
    public Texture floorBrokenTxtr;
    public Texture floorFixedTxtr;
    public Texture floorNewTxtr;

    public AudioSource soundpoint;

    public LiamTriggerScript triggerScript;

    GameObject fishIcon;

    Animator animator;

    int hookCost = 200;
    int rodCost = 1500;
    int bucketCost = 200;
    int bobberCost = 100;

    int wallCost = 750;
    int floorCost = 250;
    int tankCost = 300;
    int memberCost = 450;
    int transportCost = 1000;
    void Start()
    {
        animator = GetComponent<Animator>();
        fishModeUI.SetActive(false);
        gearModeUI.SetActive(false);
        noFishHelp.SetActive(false);
        fishgearCanvas.SetActive(false);
        clubhouseCanvas.SetActive(false);

        PlayerData.PlayerRenownUpdate();

        clubScoreStat.text = PlayerData.playerScore.ToString();
        clubMoneyStat.text = PlayerData.playerMoney.ToString();
        fishHaul.text = PlayerData.playerFish.ToString() + "/" + PlayerData.fishLimit.ToString();
        memberNum.text = PlayerData.memberCount.ToString();
        renownText.text = PlayerData.clubRenown.ToString();
        clubFishStat.text = PlayerData.clubFishData.Count.ToString() + "/" + PlayerData.clubFishCap.ToString();

        CheckIdTags(); //needed for UI updates when scene is reloaded
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            if (fishCanvasOpen)
            {
                CloseFishVendor();
            }
            else if (clubCanvasOpen)
            {
                CloseClubVendor();
            }
        }
    }

    public void PlayAudioPaperRollingClose()
    {
        PlayerData.usingClubUI = false;
        soundpoint.PlayOneShot(AudioContainer.paperRolling_close);
    }
    public void PlayAudioPaperRollingOpen()
    {
        PlayerData.usingClubUI = true;
        soundpoint.PlayOneShot(AudioContainer.paperRolling_open);
    }

    public void PlayAudioPaperClose()
    {
        PlayerData.usingClubUI = false;
        soundpoint.PlayOneShot(AudioContainer.paperClose);
    }
    public void PlayAudioPaperOpen()
    {
        PlayerData.usingClubUI = true;
        soundpoint.PlayOneShot(AudioContainer.paperOpen);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///Functions for Upgrades UI Canvas
    public void ManageFishUI()
    {
        fishModeUI.SetActive(true);
        vendorModeUI.SetActive(false);

        fishInventory = PlayerData.fishData.Count;
        currentFishSelect = 0;
        fishDetails.text = "";
        fishName.text = "";

        if (fishInventory <= 0)
        {
            cycleUI.SetActive(false);
            fishOptionsUI.SetActive(false);
            noFishHelp.SetActive(true);
        }
        else
        {
            cycleUI.SetActive(true);
            fishOptionsUI.SetActive(true);
            noFishHelp.SetActive(false);

            DisplayFishItem();
        }
    }

    void DisplayFishItem()
    {
        if (fishIcon != null)
        {
            Destroy(fishIcon);
        }
        fishIcon = Instantiate(FishLibrary.fishImages[PlayerData.fishData[currentFishSelect].fishID], fishIconSpawn.transform);

        fishDetails.text = "Quality: " + FishLibrary.fishQualityText[PlayerData.fishData[currentFishSelect].fishQuality] +
                                "\n\nWeight: " + PlayerData.fishData[currentFishSelect].fishWeight.ToString() + "kg" +
                                "\n\nSize: " + PlayerData.fishData[currentFishSelect].fishSize.ToString() + "cm" + "\n\nValue: " + PlayerData.fishData[currentFishSelect].fishValue;

        fishName.text = FishLibrary.fishNames[PlayerData.fishData[currentFishSelect].fishID];
    }

    public void CycleNextFish()
    {
        if (currentFishSelect < (fishInventory - 1))
        {
            currentFishSelect++;
            DisplayFishItem();
        }
    }

    public void CyclePreviousFish()
    {
        if (currentFishSelect > 0)
        {
            currentFishSelect--;
            DisplayFishItem();
        }
    }

    public void ReturnToModes()
    {
        fishModeUI.SetActive(false);
        gearModeUI.SetActive(false);
        vendorModeUI.SetActive(true);
    }

    public void ManageGearUI()
    {
        gearModeUI.SetActive(true);
        vendorModeUI.SetActive(false);
    }

    public void CloseFishVendor()
    {
        PlayerData.speed = 150f;
        playerStatsUI.SetActive(true);
        clubScoreStat.text = PlayerData.playerScore.ToString();
        clubMoneyStat.text = PlayerData.playerMoney.ToString();
        animator.Play("vendorCanvas_fish_close");
        triggerScript.promptButton.SetActive(true);
    }

    void SetFishCanvasOpen()
    {
        soundpoint.PlayOneShot(AudioContainer.paperOpen);
        fishCanvasOpen = true;
    }

    void SetFishCanvasFalse()
    {
        ReturnToModes();
        fishCanvasOpen = false;
        fishgearCanvas.SetActive(false);
    }

    public void CloseClubVendor()
    {
        PlayerData.speed = 150f;
        playerStatsUI.SetActive(true);
        clubScoreStat.text = PlayerData.playerScore.ToString();
        clubMoneyStat.text = PlayerData.playerMoney.ToString();
        animator.Play("vendorCanvas_club_close");
        triggerScript.promptButton.SetActive(true);
    }
    void SetClubCanvasOpen()
    {
        soundpoint.PlayOneShot(AudioContainer.paperRolling_open);
        clubCanvasOpen = true;
    }

    void SetClubCanvasFalse()
    {
        clubCanvasOpen = false;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///Keep fish or sell
    public void SellFish()
    {
        soundpoint.PlayOneShot(AudioContainer.money);
        PlayerData.playerMoney += PlayerData.fishData[currentFishSelect].fishValue;
        clubMoney.text = PlayerData.playerMoney.ToString();
        PlayerData.fishData.Remove(PlayerData.fishData[currentFishSelect]);

        PlayerData.playerFish--;
        fishHaul.text = PlayerData.playerFish.ToString() + "/" + PlayerData.fishLimit.ToString();
        
        fishInventory = PlayerData.fishData.Count;
        currentFishSelect = 0;
        fishDetails.text = "";
        fishName.text = "";
        Destroy(fishIcon);

        if (fishInventory == 0)
        {
            cycleUI.SetActive(false);
            fishOptionsUI.SetActive(false);
            noFishHelp.SetActive(true);
        }
        else
        {
            DisplayFishItem();
        }
    }

    public void KeepFish()
    {

        PlayerData.playerScore += PlayerData.fishData[currentFishSelect].fishValue;
        clubScore.text = PlayerData.playerScore.ToString();

        PlayerData.PlayerRenownUpdate();
        renownText.text = PlayerData.clubRenown.ToString();

        PlayerData.clubFishData.Add(PlayerData.fishData[currentFishSelect]);
        PlayerData.fishData.Remove(PlayerData.fishData[currentFishSelect]);

        PlayerData.playerFish--;
        fishHaul.text = PlayerData.playerFish.ToString() + "/" + PlayerData.fishLimit.ToString();
        fishInventory = PlayerData.fishData.Count;

        clubFishStat.text = PlayerData.clubFishData.Count.ToString() + "/" + PlayerData.clubFishCap.ToString();
        currentFishSelect = 0;
        fishDetails.text = "";
        fishName.text = "";
        Destroy(fishIcon);

        if (fishInventory == 0)
        {
            cycleUI.SetActive(false);
            fishOptionsUI.SetActive(false);
            noFishHelp.SetActive(true);
        }
        else
        {
            DisplayFishItem();
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///Gear Upgrade button functions
    public void UpgradeHook() //lure
    {
        if ((PlayerData.lureID < 3) && (hookCost <= PlayerData.playerMoney))
        {
            switch (PlayerData.lureID)
            {
                case 0:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.lureID++;
                    PlayerData.playerMoney -= hookCost;
                    PlayerData.HookUpdateOne();
                    hookCost = 400;
                    gearOptions.text = "Increase chances of catching a fish instead of junk or a whole lot of nothing. Or a whole lot of junk.\n\n\nFish chance++\n\n\nCost: $" + hookCost.ToString();
                    break;
                case 1:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.lureID++;
                    PlayerData.playerMoney -= hookCost;
                    PlayerData.HookUpdateTwo();
                    hookCost = 600;
                    gearOptions.text = "Increase chances of catching a fish instead of junk or a whole lot of nothing. Or a whole lot of junk.\n\n\nFish chance+++\n\n\nCost: $" + hookCost.ToString();
                    break;
                case 2:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.lureID++;
                    PlayerData.playerMoney -= hookCost;
                    PlayerData.HookUpdateThree();
                    gearOptions.text = "All upgrades purchased";
                    selectHookUpgradeButton.interactable = false;
                    break;
                default:
                    break;
            }
            clubMoney.text = PlayerData.playerMoney.ToString();
        }
        else if (hookCost > PlayerData.playerMoney)
        {
            soundpoint.PlayOneShot(AudioContainer.noMoney);
            gearOptions.text = "No money! Maybe sell more fish?";
        }
    }

    public void UpgradeBobber() //bobber
    {
        if ((PlayerData.bobberID < 2) && (bobberCost <= PlayerData.playerMoney))
        {
            switch(PlayerData.bobberID)
            {
                case 0:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.bobberID++;
                    PlayerData.playerMoney -= bobberCost;
                    bobberCost = 200;
                    gearOptions.text = "Increase delay before fish escapes by +1 second\n\n\nCost: $" + bucketCost.ToString();
                    break;
                case 1:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.bobberID++;
                    PlayerData.playerMoney -= bobberCost;
                    gearOptions.text = "All upgrades purchased";
                    selectBobberUpgradeButton.interactable = false;
                    break;
                default:
                    break;
            }
            clubMoney.text = PlayerData.playerMoney.ToString();
        }
        else if (bobberCost > PlayerData.playerMoney)
        {
            soundpoint.PlayOneShot(AudioContainer.noMoney);
            gearOptions.text = "No money! Maybe sell more fish?";
        }
    }

    public void UpgradeRod() //rod
    {
        if ((PlayerData.rodID < 2) && (rodCost <= PlayerData.playerMoney))
        {
            switch (PlayerData.rodID)
            {
                case 0:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.rodID++;
                    PlayerData.playerMoney -= rodCost;
                    PlayerData.castLimit = 14;
                    rodCost = 3000;
                    gearOptions.text = "Do more with your rod!\n\n\nCast limit: 16\n\n\nCost: $" + rodCost.ToString();
                    break;
                case 1:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.rodID++;
                    PlayerData.playerMoney -= rodCost;
                    PlayerData.castLimit = 16;
                    rodCost = 5000;
                    gearOptions.text = "Do more with your rod!\n\n\nCast limit: 18\n\n\nCost: $" + rodCost.ToString();
                    break;
                case 2:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.rodID++;
                    PlayerData.playerMoney -= rodCost;
                    PlayerData.castLimit = 18;
                    gearOptions.text = "All upgrades purchased";
                    selectRodUpgradeButton.interactable = false;
                    break;
                default:
                    break;
            }
            clubMoney.text = PlayerData.playerMoney.ToString();
        }
        else if (rodCost > PlayerData.playerMoney)
        {
            soundpoint.PlayOneShot(AudioContainer.noMoney);
            gearOptions.text = "No money! Maybe sell more fish?";
        }
    }
    public void UpgradeBucket() //bucket
    {
        if ((PlayerData.bucketID < 5) && (bucketCost <= PlayerData.playerMoney))
        {
            switch (PlayerData.bucketID)
            {
                case 0:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.bucketID++;
                    PlayerData.playerMoney -= bucketCost;
                    PlayerData.fishLimit += 2;
                    fishHaul.text = PlayerData.playerFish.ToString() + "/" + PlayerData.fishLimit.ToString();
                    bucketCost = 400;
                    gearOptions.text = "Carry more fish with you!\n\n\nBucket size: 10\n\n\nCost: $" + bucketCost.ToString();
                    break;
                case 1:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.bucketID++;
                    PlayerData.playerMoney -= bucketCost;
                    PlayerData.fishLimit += 2;
                    fishHaul.text = PlayerData.playerFish.ToString() + "/" + PlayerData.fishLimit.ToString();
                    bucketCost = 600;
                    gearOptions.text = "Carry more fish with you!\n\n\nBucket size: 12\n\n\nCost: $" + bucketCost.ToString();
                    break;
                case 2:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.bucketID++;
                    PlayerData.playerMoney -= bucketCost;
                    PlayerData.fishLimit += 2;
                    fishHaul.text = PlayerData.playerFish.ToString() + "/" + PlayerData.fishLimit.ToString();
                    bucketCost = 800;
                    gearOptions.text = "Carry more fish with you!\n\n\nBucket size: 14\n\n\nCost: $" + bucketCost.ToString();
                    break;
                case 3:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.bucketID++;
                    PlayerData.playerMoney -= bucketCost;
                    PlayerData.fishLimit += 2;
                    fishHaul.text = PlayerData.playerFish.ToString() + "/" + PlayerData.fishLimit.ToString();
                    bucketCost = 1000;
                    gearOptions.text = "Carry more fish with you!\n\n\nBucket size: 16\n\n\nCost: $" + bucketCost.ToString();
                    break;
                case 4:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.bucketID++;
                    PlayerData.playerMoney -= bucketCost;
                    PlayerData.fishLimit += 2;
                    fishHaul.text = PlayerData.playerFish.ToString() + "/" + PlayerData.fishLimit.ToString();
                    selectBucketUpgradeButton.interactable = false;
                    gearOptions.text = "All upgrades purchased";
                    break;
                default:
                    break;
            }
            clubMoney.text = PlayerData.playerMoney.ToString();
        }
        else if (bucketCost > PlayerData.playerMoney)
        {
            soundpoint.PlayOneShot(AudioContainer.noMoney);
            gearOptions.text = "No money! Maybe sell more fish?";
        }
    }
    public void BaitSelect() //bait
    {
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Club Upggrade button functions
    public void UpgradeWalls()
    {
        if ((PlayerData.clubWallID < 2) && (PlayerData.playerMoney >= wallCost))
        {
            switch (PlayerData.clubWallID)
            {
                case 0:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.clubWallID++;
                    PlayerData.playerMoney -= wallCost;
                    wallCost = 1500;
                    PlayerData.baseRenownMod += 1;
                    PlayerData.PlayerRenownUpdate();
                    renownText.text = PlayerData.clubRenown.ToString();
                    wallMaterial.mainTexture = wallFixedTxtr;
                    clubOptions.text = "Replace the wall with better material\n\n\nCost: $" + wallCost.ToString();
                    break;
                case 1:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.clubWallID++;
                    PlayerData.playerMoney -= wallCost;
                    PlayerData.baseRenownMod += 3;
                    PlayerData.PlayerRenownUpdate();
                    renownText.text = PlayerData.clubRenown.ToString();
                    wallMaterial.mainTexture = wallNewTxtr;
                    clubOptions.text = "All upgrades purchased";
                    wallButton.interactable = false;
                    break;
                default:
                    break;
            }
            clubMoney.text = PlayerData.playerMoney.ToString();
            clubUpgradeMoney.text = PlayerData.playerMoney.ToString();
        }
        else if (wallCost > PlayerData.playerMoney)
        {
            soundpoint.PlayOneShot(AudioContainer.noMoney);
            clubOptions.text = "No money! Maybe sell more fish?";
        }
    }
    public void UpgradeFloor()
    {
        if ((PlayerData.clubFloorID < 2) && (PlayerData.playerMoney >= floorCost))
        {
            switch (PlayerData.clubFloorID)
            {
                case 0:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.clubFloorID++;
                    PlayerData.playerMoney -= floorCost;
                    floorCost = 500;
                    PlayerData.baseRenownMod += 1;
                    PlayerData.PlayerRenownUpdate();
                    renownText.text = PlayerData.clubRenown.ToString();
                    floorMaterial.mainTexture = floorFixedTxtr;
                    clubOptions.text = "Replace the floor with better material\n\n\nCost: $" + floorCost.ToString();
                    break;
                case 1:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.clubFloorID++;
                    PlayerData.playerMoney -= floorCost;
                    PlayerData.baseRenownMod += 3;
                    PlayerData.PlayerRenownUpdate();
                    renownText.text = PlayerData.clubRenown.ToString();
                    floorMaterial.mainTexture = floorNewTxtr;
                    clubOptions.text = "All upgrades purchased";
                    floorButton.interactable = false;
                    break;
                default:
                    break;
            }
            clubMoney.text = PlayerData.playerMoney.ToString();
            clubUpgradeMoney.text = PlayerData.playerMoney.ToString();
        }
        else if (floorCost > PlayerData.playerMoney)
        {
            soundpoint.PlayOneShot(AudioContainer.noMoney);
            clubOptions.text = "No money! Maybe sell more fish?";
        }
    }

    public void UpgradeAquarium()
    {
        if ((PlayerData.clubAquariumID < 4) && (PlayerData.playerMoney >= tankCost))
        {
            switch (PlayerData.clubAquariumID)
            {
                case 0:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.clubAquariumID++;
                    PlayerData.playerMoney -= tankCost;
                    PlayerData.clubFishCap = 12;
                    clubFishStat.text = PlayerData.clubFishData.Count.ToString() + "/" + PlayerData.clubFishCap.ToString();
                    tankCost = 800;
                    clubOptions.text = "Increase aquarium capacity to 20.\n\n\nCost: $" + tankCost.ToString();
                    break;
                case 1:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.clubAquariumID++;
                    PlayerData.playerMoney -= tankCost;
                    PlayerData.clubFishCap = 20;
                    clubFishStat.text = PlayerData.clubFishData.Count.ToString() + "/" + PlayerData.clubFishCap.ToString();
                    tankCost = 1600;
                    clubOptions.text = "Increase aquarium capacity to 36.\n\n\nCost: $" + tankCost.ToString();
                    break;
                case 2:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.clubAquariumID++;
                    PlayerData.playerMoney -= tankCost;
                    PlayerData.clubFishCap = 36;
                    clubFishStat.text = PlayerData.clubFishData.Count.ToString() + "/" + PlayerData.clubFishCap.ToString();
                    tankCost = 2400;
                    clubOptions.text = "Increase aquarium capacity to 60.\n\n\nCost: $" + tankCost.ToString();
                    break;
                case 3:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.clubAquariumID++;
                    PlayerData.playerMoney -= tankCost;
                    PlayerData.clubFishCap = 60;
                    clubFishStat.text = PlayerData.clubFishData.Count.ToString() + "/" + PlayerData.clubFishCap.ToString();
                    clubOptions.text = "All upgrades purchased.";
                    aquariumButton.interactable = false;
                    break;
                default:
                    break;
            }
            clubMoney.text = PlayerData.playerMoney.ToString();
            clubUpgradeMoney.text = PlayerData.playerMoney.ToString();
        }
        else if (tankCost > PlayerData.playerMoney)
        {
            soundpoint.PlayOneShot(AudioContainer.noMoney);
            clubOptions.text = "No money! Maybe sell more fish?";
        }
    }

    public void UpgradeMembership()
    {
        if ((PlayerData.clubLimitID < 4) && (PlayerData.playerMoney >= memberCost))
        {
            switch (PlayerData.clubLimitID)
            {
                case 0:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.clubLimitID++;
                    PlayerData.clubMemberCap += 3;
                    PlayerData.playerMoney -= memberCost;
                    memberCost = 900;
                    clubOptions.text = "Increase the clubhouse's membership capacity from 6 -> 10.\n\n\n\nCost: $" + memberCost.ToString();
                    break;
                case 1:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.clubLimitID++;
                    PlayerData.clubMemberCap += 6;
                    PlayerData.playerMoney -= memberCost;
                    memberCost = 1500;
                    clubOptions.text = "Increase the clubhouse's membership capacity from 10 -> 20.\n\n\n\nCost: $" + memberCost.ToString();
                    break;
                case 2:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.clubLimitID++;
                    PlayerData.clubMemberCap += 10;
                    PlayerData.playerMoney -= memberCost;
                    memberCost = 2000;
                    clubOptions.text = "Increase the clubhouse's membership capacity from 20 -> 30.\n\n\n\nCost: $" + memberCost.ToString();
                    break;
                case 3:
                    PlayerData.clubLimitID++;
                    PlayerData.clubMemberCap += 10;
                    PlayerData.playerMoney -= memberCost;
                    clubOptions.text = "Membership limit reached: " + PlayerData.clubMemberCap.ToString();
                    membershipButton.interactable = false;
                    break;
                default:
                    break;
            }
            clubMoney.text = PlayerData.playerMoney.ToString();
            clubUpgradeMoney.text = PlayerData.playerMoney.ToString();
        }
        else if (memberCost > PlayerData.playerMoney)
        {
            soundpoint.PlayOneShot(AudioContainer.noMoney);
            clubOptions.text = "No money! Maybe sell more fish?";
        }
    }

    public void UnlockNewArea()
    {
        if ((PlayerData.areaID < 3) && (PlayerData.playerMoney >= transportCost))
        {
            switch (PlayerData.areaID)
            {
                case 0:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.areaID++;
                    PlayerData.playerMoney -= transportCost;
                    transpoButtonText.text = "Transport Service: Ocean";
                    transportCost = 2000;
                    clubOptions.text = "Extends the transportation service to the Ocean Park\n\n\n\nCost: $" + transportCost.ToString();
                    break;
                case 1:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.areaID++;
                    PlayerData.playerMoney -= transportCost;
                    transpoButtonText.text = "Transport Service: Storage Upgrade";
                    transportCost = 4000;
                    clubOptions.text = "Adds a fish tank to the transportation vehicle to increase fish that can be carried (+4)\n\n\n\nCost: $" + transportCost.ToString();
                    break;
                case 2:
                    soundpoint.PlayOneShot(AudioContainer.money_register);
                    PlayerData.areaID++;
                    PlayerData.playerMoney -= transportCost;
                    PlayerData.fishLimit += 4;
                    fishHaul.text = PlayerData.playerFish.ToString() + "/" + PlayerData.fishLimit.ToString();
                    transpoButtonText.text = "Transport Service";
                    clubOptions.text = "All upgrades purchased";
                    transpoButton.interactable = false;
                    break;
                default:
                    break;
            }
            clubMoney.text = PlayerData.playerMoney.ToString();
            clubUpgradeMoney.text = PlayerData.playerMoney.ToString();
        }
        else if (transportCost > PlayerData.playerMoney)
        {
            soundpoint.PlayOneShot(AudioContainer.noMoney);
            clubOptions.text = "No money! Maybe sell more fish?";
        }
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///Flavor and descriptor texts that go into the helper text boxes
    public void FishModeText()
    {
        vendorModeText.text = "Check the fish you've caught from your last trip and see which ones you want to keep for the clubhouse or sell.";
    }
    public void GearModeText()
    {
        vendorModeText.text = "If you have the funds, upgrade your fishing gear to make yourself a better fisherman.";
    }
    public void ClearModeText()
    {
        vendorModeText.text = "";
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Fishing Gear
    public void LureText()
    {
        switch (PlayerData.lureID)
        {
            case 0:
                gearOptions.text = "Increase chances of catching a fish instead of junk or a whole lot of nothing. Or a whole lot of junk.\n\n\nFish chance+\n\n\nCost: $" + hookCost.ToString();
                break;
            case 1:
                gearOptions.text = "Increase chances of catching a fish instead of junk or a whole lot of nothing. Or a whole lot of junk.\n\n\nFish chance++\n\n\nCost: $" + hookCost.ToString();
                break;
            case 2:
                gearOptions.text = "Increase chances of catching a fish instead of junk or a whole lot of nothing. Or a whole lot of junk.\n\n\nFish chance+++\n\n\nCost: $" + hookCost.ToString();
                break;
            default:
                gearOptions.text = "All upgrades purchased";
                break;
        }
        
    }
    public void BobberText()
    {
        switch (PlayerData.bobberID)
        {
            case 0:
                gearOptions.text = "Keep fish hooked a bit longer!\n\n\n+1 second before fish escapes\n\n\nCost: $" + bobberCost.ToString();
                break;
            case 1:
                gearOptions.text = "Keep fish hooked a bit longer!\n\n\n+1 second before fish escapes\n\n\nCost: $" + bobberCost.ToString();
                break;
            default:
                gearOptions.text = "All upgrades purchased";
                break;
        }
    }
    public void BucketText()
    {
        switch (PlayerData.bucketID)
        {
            case 0:
                gearOptions.text = "Carry more fish with you!\n\n\nBucket size: 8\n\n\nCost: $" + bucketCost.ToString();
                break;
            case 1:
                gearOptions.text = "Carry more fish with you!\n\n\nBucket size: 10\n\n\nCost: $" + bucketCost.ToString();
                break;
            case 2:
                gearOptions.text = "Carry more fish with you!\n\n\nBucket size: 12\n\n\nCost: $" + bucketCost.ToString();
                break;
            case 3:
                gearOptions.text = "Carry more fish with you!\n\n\nBucket size: 14\n\n\nCost: $" + bucketCost.ToString();
                break;
            case 4:
                gearOptions.text = "Carry more fish with you!\n\n\nBucket size: 16\n\n\nCost: $" + bucketCost.ToString();
                break;
            default:
                gearOptions.text = "All upgrades purchased";
                break;
        }
    }
    public void BaitText()
    {
        gearOptions.text = "Feeling ignored by the one you desire most? Lead with a snack!\n\n\nSee bait selection";
    }
    public void RodText()
    {
        switch (PlayerData.rodID)
        {
            case 0:
                gearOptions.text = "Do more with your rod!\n\n\nCast limit: 14\n\n\nCost: $" + rodCost.ToString();
                break;
            case 1:
                gearOptions.text = "Do more with your rod!\n\n\nCast limit: 16\n\n\nCost: $" + rodCost.ToString();
                break;
            case 2:
                gearOptions.text = "Do more with your rod!\n\n\nCast limit: 18\n\n\nCost: $" + rodCost.ToString();
                break;
            default:
                gearOptions.text = "All upgrades purchased";
                break;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Clubhouse
    public void FloorText()
    {
        switch (PlayerData.clubFloorID)
        {
            case 0:
                clubOptions.text = "Repair the club floor\n\n\nCost: $" + floorCost.ToString();
                break;
            case 1:
                clubOptions.text = "Replace the floor with better material\n\n\nCost: $" + floorCost.ToString();
                break;
            default:
                clubOptions.text = "All upgrades purchased.";
                break;
        }
    }
    public void WallsText()
    {
        switch (PlayerData.clubWallID)
        {
            case 0:
                clubOptions.text = "Repair the club wall\n\n\nCost: $" + wallCost.ToString();
                break;
            case 1:
                clubOptions.text = "Replace the wall with better material\n\n\nCost: $" + wallCost.ToString();
                break;
            default:
                clubOptions.text = "All upgrades purchased.";
                break;
        }
    }
    public void AquariumText()
    {
        switch (PlayerData.clubAquariumID)
        {
            case 0:
                clubOptions.text = "Increase aquarium capacity to 12.\n\n\nCost: $" + tankCost.ToString();
                break;
            case 1:
                clubOptions.text = "Increase aquarium capacity to 20.\n\n\nCost: $" + tankCost.ToString();
                break;
            case 2:
                clubOptions.text = "Increase aquarium capacity to 36.\n\n\nCost: $" + tankCost.ToString();
                break;
            case 3:
                clubOptions.text = "Increase aquarium capacity to 60.\n\n\nCost: $" + tankCost.ToString();
                break;
            default:
                clubOptions.text = "All upgrades purchased.";
                break;
        }
    }
    public void MembershipText()
    {
        switch (PlayerData.clubLimitID)
        {
            case 0:
                clubOptions.text = "Increase the clubhouse's membership capacity from 1 -> 4.\n\n\n\nCost: $" + memberCost.ToString();
                break;
            case 1:
                clubOptions.text = "Increase the clubhouse's membership capacity from 4 -> 10.\n\n\n\nCost: $" + memberCost.ToString();
                break;
            case 2:
                clubOptions.text = "Increase the clubhouse's membership capacity from 10 -> 20.\n\n\n\nCost: $" + memberCost.ToString();
                break;
            case 3:
                clubOptions.text = "Increase the clubhouse's membership capacity from 20 -> 30.\n\n\n\nCost: $" + memberCost.ToString();
                break;
            default:
                clubOptions.text = "Membership limit reached: " + PlayerData.clubMemberCap.ToString();
                break;
        }
    }
    public void TransportText()
    {
        switch (PlayerData.areaID)
        {
            case 0:
                clubOptions.text = "Unlocks a transportation service to Riverside\n\n\n\nCost: $" + transportCost.ToString();
                break;
            case 1:
                clubOptions.text = "Extends the transportation service to the Ocean Park\n\n\n\nCost: $" + transportCost.ToString();
                break;
            case 2:
                clubOptions.text = "Adds a fish tank to the transportation vehicle to increase fish that can be carried (+4)\n\n\n\nCost: $" + transportCost.ToString();
                break;
            default:
                clubOptions.text = "All upgrades purchased";
                break;
        }
    }

    public void EmptyText()
    {
        clubOptions.text = "";
        gearOptions.text = "";
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///

    void CheckIdTags()
    {
        clubMoney.text = PlayerData.playerMoney.ToString();
        clubScore.text = PlayerData.playerScore.ToString();
        switch (PlayerData.areaID)
        {
            case 0:
                transpoButtonText.text = "Transport Service: River";
                transportCost = 1000;
                break;
            case 1:
                transpoButtonText.text = "Transport Service: Ocean";
                transportCost = 2000;
                break;
            case 2:
                transpoButtonText.text = "Transport Service: Storage Upgrade";
                transportCost = 4000;
                break;
            default:
                transpoButtonText.text = "Transport Service";
                transpoButton.interactable = false;
                break;
        }

        switch (PlayerData.lureID)
        {
            case 0:
                hookCost = 200;
                break;
            case 1:
                hookCost = 400;
                break;
            case 2:
                hookCost = 600;
                break;
            default:
                selectHookUpgradeButton.interactable = false;
                break;
        }

        switch (PlayerData.rodID)
        {
            case 0:
                rodCost = 1500;
                break;
            case 1:
                rodCost = 3000;
                break;
            case 2:
                rodCost = 5000;
                break;
            default:
                selectRodUpgradeButton.interactable = false;
                break;
        }

        switch (PlayerData.bucketID)
        {
            case 0:
                bucketCost = 200;
                break;
            case 1:
                bucketCost = 400;
                break;
            case 2:
                bucketCost = 600;
                break;
            case 3:
                bucketCost = 800;
                break;
            case 4:
                bucketCost = 1000;
                break;
            default:
                selectBucketUpgradeButton.interactable = false;
                break;
        }

        switch(PlayerData.bobberID)
        {
            case 0:
                bucketCost = 100;
                break;
            case 1:
                bucketCost = 200;
                break;
            default:
                selectBucketUpgradeButton.interactable = false;
                break;
        }

        switch (PlayerData.clubLimitID)
        {
            case 0:
                memberCost = 450;
                break;
            case 1:
                memberCost = 900;
                break;
            case 2:
                memberCost = 1500;
                break;
            case 3:
                memberCost = 2500;
                break;
            default:
                membershipButton.interactable = false;
                break;
        }

        switch (PlayerData.clubWallID)
        {
            case 0:
                wallCost = 750;
                wallMaterial.mainTexture = wallBrokenTxtr;
                break;
            case 1:
                wallCost = 1500;
                wallMaterial.mainTexture = wallFixedTxtr;
                break;
            default:
                wallMaterial.mainTexture = wallNewTxtr;
                wallButton.interactable = false;
                break;
        }

        switch (PlayerData.clubFloorID)
        {
            case 0:
                floorCost = 250;
                floorMaterial.mainTexture = floorBrokenTxtr;
                break;
            case 1:
                floorCost = 500;
                floorMaterial.mainTexture = floorFixedTxtr;
                break;
            default:
                floorMaterial.mainTexture = floorNewTxtr;
                floorButton.interactable = false;
                break;
        }

        switch (PlayerData.clubAquariumID)
        {
            case 0:
                tankCost = 300;
                break;
            case 1:
                tankCost = 800;
                break;
            case 2:
                tankCost = 1600;
                break;
            case 3:
                tankCost = 2400;
                break;
            default:
                aquariumButton.interactable = false;
                break;
        }
    }
}
