using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This says PlayerData but i kind of changed my mind halfway through so this pretty much just holds a lot of the game data i can think of that are constant-ish
/// </summary>
public static class PlayerData
{

    [System.Serializable]
    public class FishManager
    {
        public int fishID;
        public float fishWeight;
        public float fishSize;
        public int fishQuality;
        public int fishValue;

        public FishManager(int id, float weight, float size, int quality, int value)
        {
            fishID = id;
            fishWeight = weight;
            fishSize = size;
            fishQuality = quality;
            fishValue = value;
        }
    }

    public static float speed = 150.0f;
    public static int castLimit = 12;
    public static int playerSkinID = 0;
    public static int playerScore = 0;
    public static int playerMoney = 10000;
    public static int fishLimit = 6;
    public static int clubFishCap = 12;
    public static int clubMemberCap = 1;
    public static int memberCount = 1;
    public static int clubRenown = 0;
    public static int baseRenownMod = 10;

    public static int bucketID = 0; //determines fish capacity per trip
    public static int rodID = 0; //default bamboo rod - determines cast limit
    public static int bobberID = 0; //default no bobber - gives more time before fish escapes
    public static int lureID = 0; //default no lure - increases catch chance
    public static int baitID = 0; //default no bait - increases chance for specific fish

    public static int areaID = 0;
    public static int clubWallID = 0;
    public static int clubFloorID = 0;
    public static int clubAquariumID = 0;
    public static int clubLimitID = 0;

    public static List<FishManager> fishData = new List<FishManager>();
    public static List<FishManager> clubFishData = new List<FishManager>();

    //for game logic
    public static bool beatPlaying = false;
    public static int noteSpeed = 1;
    public static int rhythmDiff = 1;
    public static bool isFishing = false;
    public static bool isInUI = false;
    public static bool hasCatch = false;
    public static bool tossBack = false;
    public static int playerCasts = 0;
    public static int playerFish = 0;
    public static float vertSpeed = 65000f;
    public static float horzSpeed = 10000f;
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static int CatchChanceMod(int modifier)
    {
        int newModifier = 100 * (lureID / 10);
        modifier += newModifier; //adjust as needed
        if (modifier > 99)
        {
            modifier = 99;
        }
        return modifier;
    }

    public static void CatchDiffMod(int modifier)
    {
        rhythmDiff = modifier - baitID;
        if (rhythmDiff < 1)
        {
            rhythmDiff = 1;
        }
    }

    public static void PassFishValues()
    {
        FishManager addFish = new FishManager(FishLibrary.fishID, FishLibrary.fishWeight, FishLibrary.fishSize, FishLibrary.fishQuality, FishLibrary.fishValue);
        fishData.Add(addFish);
    }

    public static void HookUpdateOne()
    {
        FishLibrary.nospawn = 113;

        FishLibrary.pondCarpChance = 126;
        FishLibrary.pondBassChance = 139;
        FishLibrary.pondSilverfishChance = 152;

        FishLibrary.pondBootChance = 155;
        FishLibrary.pondControllerChance = 158;
        FishLibrary.pondAxolotlChance = 161;

        FishLibrary.pondKoiChance = 169;
        FishLibrary.pondSturgeonChance = 177;
        FishLibrary.pondBluegillChance = 185;

        FishLibrary.pondGoldfishChance = 190;
        FishLibrary.pondStachefishChance = 195;
    }

    public static void HookUpdateTwo()
    {
        FishLibrary.nospawn = 110;

        FishLibrary.pondCarpChance = 125;
        FishLibrary.pondBassChance = 139;
        FishLibrary.pondSilverfishChance = 153;

        FishLibrary.pondBootChance = 155;
        FishLibrary.pondControllerChance = 157;
        FishLibrary.pondAxolotlChance = 161;

        FishLibrary.pondKoiChance = 168;
        FishLibrary.pondSturgeonChance = 175;
        FishLibrary.pondBluegillChance = 182;

        FishLibrary.pondGoldfishChance = 188;
        FishLibrary.pondStachefishChance = 194;
    }

    public static void HookUpdateThree()
    {
        FishLibrary.nospawn = 108;

        FishLibrary.pondCarpChance = 123;
        FishLibrary.pondBassChance = 137;
        FishLibrary.pondSilverfishChance = 151;

        FishLibrary.pondBootChance = 152;
        FishLibrary.pondControllerChance = 153;
        FishLibrary.pondAxolotlChance = 155;

        FishLibrary.pondKoiChance = 163;
        FishLibrary.pondSturgeonChance = 171;
        FishLibrary.pondBluegillChance = 179;

        FishLibrary.pondGoldfishChance = 186;
        FishLibrary.pondStachefishChance = 193;
    }

    public static void PlayerRenownUpdate()
    {
        clubRenown = playerScore * ((memberCount + baseRenownMod) /100);
    }
}
