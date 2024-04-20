using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI accuracy;

    public int scoreCount = 0;
    public int totalNotes;
    public int finishedNotes = 0;

    public int scoreMaxPerfect;
    public int scoreMaxGood;
    public int scoreMaxBad;

    public Animator ratingAnimator;
    public Animator playerAnimator;
    public RhythmBeats rhythmGame;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GameObject.Find("playerSprite").GetComponent<Animator>();
        score.text = scoreCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
