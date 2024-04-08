using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClubManager : MonoBehaviour
{
    public TextMeshProUGUI points;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        points.text = "Points: " + FishManager.earnedpoints;
    }
}
