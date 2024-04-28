using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FishLibrary
{
    public static bool isCached = false;

    public static int fishID;
    public static int fishQuality = 0; //0 - bad; 1 - good; 2 - perfect; 3 - garbage
    public static float fishWeight;
    public static float fishSize;
    public static int fishValue;

    public static bool isJunk = false;

    /// <summary>
    /// Spawn chances; default for nothing is 16 (max)
    /// </summary>
    public static int nospawn = 116;

    public static int pondCarpChance = 127;
    public static int pondBassChance = 138;
    public static int pondSilverfishChance = 149;

    public static int pondBootChance = 154;
    public static int pondControllerChance = 159;
    public static int pondAxolotlChance = 164;

    public static int pondKoiChance = 172;
    public static int pondSturgeonChance = 180;
    public static int pondBluegillChance = 188;

    public static int pondGoldfishChance = 192;
    public static int pondStachefishChance = 196;
    public static int pondBullroutChance = 200;

    ////////////////////////////////

    static float newWeight;
    static float roundedWeight;
    static float newSize;
    static float roundedSize;


    public static List<string> fishQualityText = new List<string>
    {
        "Poor",
        "Good",
        "Super",
        "Garbage"
    };

    public static List<string> fishNames = new List<string>
    {
        "Majestic Carp",
        "Drop Bass",
        "Silverfish",
        "Leather Boot",
        "Broken Controller",
        "Wandering Axolotl",
        "Neon Koi",
        "Doctor Sturgeon",
        "Cobalt Bluegill",
        "Goldfish",
        "Pencilstache Fish",
        "Raging Bullrout"
    };

    public static List<GameObject> fishObjects = new List<GameObject>();
    public static List<GameObject> fishImages = new List<GameObject>();
    public static float GetWeight()
    {
        switch (fishID)
        {
            case 0: //carp
                switch (fishQuality)
                {
                    case 0:
                        newWeight = Random.Range(6.00f, 8.00f);
                        break;
                    case 1:
                        newWeight = Random.Range(8.00f, 10.00f);
                        break;
                    case 2:
                        newWeight = Random.Range(10.00f, 12.10f);
                        break;
                }
                break;
            case 1: //bass
                switch (fishQuality)
                {
                    case 0:
                        newWeight = Random.Range(3.80f, 4.50f);
                        break;
                    case 1:
                        newWeight = Random.Range(4.50f, 5.20f);
                        break;
                    case 2:
                        newWeight = Random.Range(5.20f, 5.91f);
                        break;
                }
                break;
            case 2: //silver goldfish
                switch (fishQuality)
                {
                    case 0:
                        newWeight = Random.Range(0.10f, 0.16f);
                        break;
                    case 1:
                        newWeight = Random.Range(0.16f, 0.22f);
                        break;
                    case 2:
                        newWeight = Random.Range(0.22f, 0.31f);
                        break;
                }
                break;
            case 6: //koi
                switch (fishQuality)
                {
                    case 0:
                        newWeight = Random.Range(10.00f, 12.00f);
                        break;
                    case 1:
                        newWeight = Random.Range(12.00f, 14.00f);
                        break;
                    case 2:
                        newWeight = Random.Range(14.00f, 16.10f);
                        break;
                }
                break;
            case 7: //sturgeon
                switch (fishQuality)
                {
                    case 0:
                        newWeight = Random.Range(180.00f, 195.67f);
                        break;
                    case 1:
                        newWeight = Random.Range(195.67f, 211.34f);
                        break;
                    case 2:
                        newWeight = Random.Range(211.34f, 227.1f);
                        break;
                }
                break;
            case 8: //bluegill
                switch (fishQuality)
                {
                    case 0:
                        newWeight = Random.Range(1.00f, 1.17f);
                        break;
                    case 1:
                        newWeight = Random.Range(1.17f, 1.34f);
                        break;
                    case 2:
                        newWeight = Random.Range(1.34f, 1.53f);
                        break;
                }
                break;
            case 9: //goldfish
                switch (fishQuality)
                {
                    case 0:
                        newWeight = Random.Range(10.00f, 12.00f);
                        break;
                    case 1:
                        newWeight = Random.Range(12.00f, 14.00f);
                        break;
                    case 2:
                        newWeight = Random.Range(14.00f, 16.1f);
                        break;
                }
                break;
            case 10: //mustache
                switch (fishQuality)
                {
                    case 0:
                        newWeight = Random.Range(15.00f, 16.67f);
                        break;
                    case 1:
                        newWeight = Random.Range(16.67f, 18.34f);
                        break;
                    case 2:
                        newWeight = Random.Range(18.34f, 20.1f);
                        break;
                }
                break;
            case 11: //bullrout
                switch (fishQuality)
                {
                    case 0:
                        newWeight = Random.Range(0.21f, 0.31f);
                        break;
                    case 1:
                        newWeight = Random.Range(0.31f, 0.41f);
                        break;
                    case 2:
                        newWeight = Random.Range(0.41f, 0.52f);
                        break;
                }
                break;
            default:
                break;
        }

        roundedWeight = Mathf.Round(newWeight * 100.0f) / 100.0f; //limit to hundredths decimal place if random value is too large

        return roundedWeight;
    }

    public static float GetSize()
    {
        switch (fishID)
        {
            case 0: //carp
                switch (fishQuality)
                {
                    case 0:
                        newSize = Random.Range(40.00f, 53.33f);
                        break;
                    case 1:
                        newSize = Random.Range(53.33f, 63.66f);
                        break;
                    case 2:
                        newSize = Random.Range(63.99f, 80.10f);
                        break;
                }
                break;
            case 1: //bass
                switch (fishQuality)
                {
                    case 0:
                        newSize = Random.Range(48.00f, 50.67f);
                        break;
                    case 1:
                        newSize = Random.Range(50.67f, 53.34f);
                        break;
                    case 2:
                        newSize = Random.Range(53.34f, 56.10f);
                        break;
                }
                break;
            case 2: //silver goldfish
                switch (fishQuality)
                {
                    case 0:
                        newSize = Random.Range(12.00f, 15.33f);
                        break;
                    case 1:
                        newSize = Random.Range(15.33f, 18.66f);
                        break;
                    case 2:
                        newSize = Random.Range(18.99f, 22.5f);
                        break;
                }
                break;
            case 6: //koi
                switch (fishQuality)
                {
                    case 0:
                        newSize = Random.Range(30.00f, 33.33f);
                        break;
                    case 1:
                        newSize = Random.Range(33.33f, 36.66f);
                        break;
                    case 2:
                        newSize = Random.Range(36.66f, 40.10f);
                        break;
                }
                break;
            case 7: //sturgeon
                switch (fishQuality)
                {
                    case 0:
                        newSize = Random.Range(200.00f, 233.33f);
                        break;
                    case 1:
                        newSize = Random.Range(233.33f, 266.66f);
                        break;
                    case 2:
                        newSize = Random.Range(266.66f, 300.1f);
                        break;
                }
                break;
            case 8: //bluegill
                switch (fishQuality)
                {
                    case 0:
                        newSize = Random.Range(15.20f, 16.50f);
                        break;
                    case 1:
                        newSize = Random.Range(16.50f, 17.80f);
                        break;
                    case 2:
                        newSize = Random.Range(17.80f, 19.2f);
                        break;
                }
                break;
            case 9: //goldfish
                switch (fishQuality)
                {
                    case 0:
                        newSize = Random.Range(12.00f, 15.33f);
                        break;
                    case 1:
                        newSize = Random.Range(15.33f, 18.66f);
                        break;
                    case 2:
                        newSize = Random.Range(18.99f, 22.5f);
                        break;
                }
                break;
            case 10: //mustache
                switch (fishQuality)
                {
                    case 0:
                        newSize = Random.Range(120.00f, 133.33f);
                        break;
                    case 1:
                        newSize = Random.Range(133.33f, 146.66f);
                        break;
                    case 2:
                        newSize = Random.Range(146.66f, 160.10f);
                        break;
                }
                break;
            case 11: //bullrout
                switch (fishQuality)
                {
                    case 0:
                        newSize = Random.Range(20.00f, 23.33f);
                        break;
                    case 1:
                        newSize = Random.Range(23.33f, 26.66f);
                        break;
                    case 2:
                        newSize = Random.Range(26.66f, 30.10f);
                        break;
                }
                break;
            default:
                break;
        }

        roundedSize = Mathf.Round(newSize * 100.0f) / 100.0f; //limit to hundredths decimal place if random decimal value is too large

        return roundedSize;
    }
    public static int GetValue()
    {
        float newScoreF = (fishWeight / fishSize) * 100;
        /*if (newScoreF < 0)
        {
            newScoreF = newScoreF * fishWeight;
        }*/
        Debug.Log("Calculated raw value: " + newScoreF);
        int fishScore = Mathf.CeilToInt(fishWeight * (newScoreF * fishQuality));
        Debug.Log("Calculated final value: " + fishScore);

        return fishScore;
    }

    public static void PondSpawnTable(int chance)
    {
        //0-16 is nothing
        //pond Range; the conditions on the right are the fish spawned
        if ((chance > nospawn) && (chance <= pondCarpChance))
        {
            fishID = 0;
        }
        else if ((chance > pondCarpChance) && (chance <= pondBassChance))
        {
            fishID = 1;
        }
        else if ((chance > pondBassChance) && (chance <= pondSilverfishChance))
        {
            fishID = 2;
        }
        else if ((chance > pondSilverfishChance) && (chance <= pondBootChance))
        {
            fishID = 3;
            isJunk = true;
            fishQuality = 3;
        }
        else if ((chance > pondBootChance) && (chance <= pondControllerChance))
        {
            fishID = 4;
            isJunk = true;
            fishQuality = 3;
        }
        else if ((chance > pondControllerChance) && (chance <= pondAxolotlChance))
        {
            fishID = 5;
            isJunk = true;
            fishQuality = 3;
        }
        else if ((chance > pondAxolotlChance) && (chance <= pondKoiChance))
        {
            fishID = 6;
        }
        else if ((chance > pondKoiChance) && (chance <= pondSturgeonChance))
        {
            fishID = 7;
        }
        else if ((chance > pondSturgeonChance) && (chance <= pondBluegillChance))
        {
            fishID = 8;
        }
        else if ((chance > pondBluegillChance) && (chance <= pondGoldfishChance))
        {
            fishID = 9;
        }
        else if ((chance > pondGoldfishChance) && (chance <= pondStachefishChance))
        {
            fishID = 10;
        }
        else if ((chance > pondStachefishChance) && (chance <= pondBullroutChance))
        {
            fishID = 11;
        }
        else
        {
            fishID = -1;
        }
    }
}
