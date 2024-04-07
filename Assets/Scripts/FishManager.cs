using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FishManager : MonoBehaviour
{
    public List<string> fishBasket = new List<string> { };
    public List<string> fish = new List<string> { };
    public string fishCollection;
    private string lastGeneratedFish;

    //fish values 
    public int GoldfishSize;
    public int CarpSize;
    public int bassSize;
    
    //UI stuff
    public TextMeshProUGUI CurrentFish;
    public TextMeshProUGUI Instructions;
    public TextMeshProUGUI Basket;
    public TextMeshProUGUI CastsNum;

    public TextMeshProUGUI pointEarner;
    public TextMeshProUGUI Size;
    public TextMeshProUGUI Weight;

    //point collection
    public int totalPoints;
    public int points;

    public int Casts;

    // can and cant do actions
    private bool CanFish;
    private bool CanThrow;
    private bool CanKeep;

    //int randomValue = Random.Range(0,99);

    public FishScript fishLength;
    public FishScript fishWeight;
    public FishScript fishPoints;

    // Start is called before the first frame update
    void Start()
    {
        fish.Add("bass");
        fish.Add("Goldfish");
        fish.Add("Carp");

        Instructions.text = "Instructions: Press A to fish";

        Debug.Log("Press A to fish");

        CanFish = true;
        CanThrow = false;
        CanKeep = false;


        Casts = 12;

    }
    public void PointEarner()
    {
        totalPoints = totalPoints + points;
    }

    public void AddRareFish()
    {
        //fish.Add();
        //fish.Add();
        //fish.Add();

    }
    public void AddExoticFish()
    {
        //fish.Add();
        //fish.Add();
        //fish.Add();
    }

    // Update is called once per frame
    void Update()
    {
        CastsNum.text = "" + Casts; 



        if(Casts == 0)
        {
            CanFish = false;
            Debug.Log("finished fishing");
            Instructions.text = "The Day is over the fish are asleep";
        }
        
        pointEarner.text = "Points: " + totalPoints;

        CarpSize = Random.Range(40, 80);
        bassSize = Random.Range(48, 56);
        GoldfishSize = Random.Range(12, 22);

        if (fishBasket.Count >= 8)
        {
            Debug.Log("finished fishing");
            CanFish = false;
            Instructions.text = "The Day is over the fish are asleep";
        }


        foreach (var item in fishBasket)
        {
            //Debug.Log(item.ToString());
        }

        string result = " ";
        foreach (var item in fishBasket)
        {
            result += item.ToString() + ", ";

        }
        //Debug.Log(result);

        Basket.text = "Fish: " + result;
        

        if (Input.GetKeyDown(KeyCode.A))
        {
            while (CanFish)
            {
                int randomIndex = Random.Range(0, fish.Count);
                string randomFish = fish[randomIndex];
                lastGeneratedFish = randomFish;
                Debug.Log("look its a " + randomFish);
                CurrentFish.text = "Look you Got a: " + lastGeneratedFish;
                CanFish = false;
                CanKeep = true;
                CanThrow = true;

                Casts--;

                Instructions.text = "Instructions: Press Q to Put fish in basket   Press E to throw fish in pond";
                Debug.Log("Press Q to Put fish in basket");

                Debug.Log("Press E to throw fish in pond");


                //fish values 
                if (lastGeneratedFish == "bass")
                {
                    
                    Weight.text = "Weight: " + Random.Range(6, 12) + "Kg";
                    Size.text = "Size: " + bassSize + "CM";
                    
                }
                if (lastGeneratedFish == "Carp")
                {

                    Weight.text = "Weight: " + Random.Range(3.8f, 5.5f) + "Kg";
                    Size.text = "Size: " + CarpSize + "CM";
                    
                }
                if (lastGeneratedFish == "Goldfish")
                {
                    
                    Weight.text = "Weight: " + Random.Range(0.10f, 0.30f) + "Kg";
                    Size.text = "Size: " + GoldfishSize + "CM";
                    
                }
            }
        }
        //keep
        if (Input.GetKeyDown(KeyCode.Q))
        {
            while (CanKeep)
            {
                fishBasket.Add(lastGeneratedFish);
                if (lastGeneratedFish == "Goldfish")
                {
                    points = GoldfishSize * 12;
                    PointEarner();
                }
                if (lastGeneratedFish == "Carp")
                {
                    points = CarpSize * 12;
                    PointEarner();
                }
                if (lastGeneratedFish == "bass")
                {
                    points = bassSize * 12;
                    PointEarner();
                }
                    Debug.Log("u keep fish");
                CurrentFish.text = "Look you Got a: ";
                Weight.text = "Weight: ";
                Size.text = "Size: ";
                Instructions.text = "Instructions: Press A to fish";
                Debug.Log("Press A to fish");
                CanFish = true;
                CanKeep = false;
                CanThrow = false;
            }
        }
        //throw
        if (Input.GetKeyDown(KeyCode.E))
        {
            while (CanThrow)
            {
                //fishBasket.Remove(randomFish);
                Debug.Log("u Throw Fish");
                CurrentFish.text = "Look you Got a: ";
                Weight.text = "Weight: ";
                Size.text = "Size: ";
                Instructions.text = "Instructions: Press A to fish";
                Debug.Log("Press A to fish");
                CanFish = true;
                CanThrow = false;
                CanKeep = false;
            }
        }
    }

   
}
