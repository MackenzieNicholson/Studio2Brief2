using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int upgradeCap;

    public int upgradeCount1;
    public int upgradeCost1;

    public int upgradeCount2;
    public int upgradeCost2;

    public int upgradeCount3;
    public int upgradeCost3;

    public int upgradeCount4;
    public int upgradeCost4;

    public int upgradeCost5;

    public TextMeshProUGUI lureText;
    public TextMeshProUGUI rodText;
    public TextMeshProUGUI bucketText;
    public TextMeshProUGUI baitText;
    public TextMeshProUGUI areaText;
    public TextMeshProUGUI capText;

    // Update is called once per frame
    void Update()
    {
        lureText.text = "Rarer Fish " + upgradeCost1 + "$";
        rodText.text = "More Casts " + upgradeCost2 + "$";
        bucketText.text = "Bucket Capacity " + upgradeCost3 + "$";
        baitText.text = "Easy Catch " + upgradeCost4 + "$";
        areaText.text = "New Area N/A";
        capText.text = "More Upgrades " + upgradeCost5 + "$";
    }

    public void BetterFish() //lure
    {
        if (upgradeCount1 < upgradeCap)
        {
            if(upgradeCost1 <= PlayerData.playerMoney)
            {
                PlayerData.lureID += 1;
                PlayerData.playerMoney = PlayerData.playerMoney - upgradeCost1;
                upgradeCount1 += 1;
            }
        }
    }
    public void MoreCasts() //rod
    {
        if (upgradeCount2 < upgradeCap)
        {
            if (upgradeCost2 <= PlayerData.playerMoney)
            {
                PlayerData.rodID += 1;
                PlayerData.playerMoney = PlayerData.playerMoney - upgradeCost2;
                upgradeCount2 += 1;
            }
        }
    }
    public void BiggerBasket() //bucket
    {
        if (upgradeCount3 < upgradeCap)
        {
            if (upgradeCost3 <= PlayerData.playerMoney)
            {
                PlayerData.bucketID += 1;
                PlayerData.playerMoney = PlayerData.playerMoney - upgradeCost3;
                upgradeCount3 += 1;
            }
        }
    }
    public void EasierGame() //bait
    {
        if (upgradeCount4 < upgradeCap)
        {
            if (upgradeCost4 <= PlayerData.playerMoney)
            {
                PlayerData.baitID += 1;
                PlayerData.playerMoney = PlayerData.playerMoney - upgradeCost4;
                upgradeCount4 += 1;
            }
        }
    }
    public void NewArea()
    {

    }
    public void MoreUpgrades()
    {
        if (upgradeCost5 <= FishManager.earnedpoints)
        {
            upgradeCap += 1;
        }
    }
}
