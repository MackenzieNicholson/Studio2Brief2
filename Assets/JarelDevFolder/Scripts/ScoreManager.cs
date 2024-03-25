using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI accuracy;

    public int scoreCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        score.text = scoreCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
