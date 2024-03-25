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

    public TextMeshProUGUI pointEarner;
    public TextMeshProUGUI CurrentFish;
    public TextMeshProUGUI Instructions;
    public TextMeshProUGUI Basket;


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

        

    }

    // Update is called once per frame
    void Update()
    {
        

        if(fishBasket.Count >= 8)
        {
            Debug.Log("finished fishing");
            CanFish = false;
            Instructions.text = "The Day is over the fish are asleep";
        }


        foreach (var item in fishBasket)
        {
            Debug.Log(item.ToString());
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

                Instructions.text = "Instructions: Press Q to Put fish in basket   Press E to throw fish in pond";
                Debug.Log("Press Q to Put fish in basket");

                Debug.Log("Press E to throw fish in pond");
            }
        }
        //keep
        if (Input.GetKeyDown(KeyCode.Q))
        {
            while (CanKeep)
            {
                fishBasket.Add(lastGeneratedFish);
                Debug.Log("u keep fish");
                CurrentFish.text = "Look you Got a: ";
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
                Instructions.text = "Instructions: Press A to fish";
                Debug.Log("Press A to fish");
                CanFish = true;
                CanThrow = false;
                CanKeep = false;
            }
        }
    }

   
}
