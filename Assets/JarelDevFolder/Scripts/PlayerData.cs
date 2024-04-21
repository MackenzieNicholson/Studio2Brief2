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

        public FishManager (int id, float weight, float size, int quality, int value)
        {
            fishID = id;
            fishWeight = weight;
            fishSize = size;
            fishQuality = quality;
            fishValue = value;
        }
    }

    public static int castLimit = 12;
    public static int playerSkinID = 0;
    public static int rodID = 0; //default bamboo rod - determines cast limit
    public static int bobberID = 0; //default no bobber - gives more time to catch
    public static int lureID = 0; //default no lure - increases catch chance
    public static int baitID = 0; //default no bait - reduces catch/rhythm difficulty
    public static int playerScore = 0;
    public static int playerMoney = 0;
    public static int fishLimit = 6;
    public static int bucketID = 0; //determines fish capacity per session

    public static List<FishManager> keptFishID = new List<FishManager>();

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
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static int CatchChanceMod(int modifier)
    {
        int newModifier = lureID * 5;
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
}
