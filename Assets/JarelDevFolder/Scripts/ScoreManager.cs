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

    public Animator ratingAnimator;

    // Start is called before the first frame update
    void Start()
    {
        score.text = scoreCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreMiss()
    {
        accuracy.text = "Miss!";
        scoreCount -= 10;
        score.text = scoreCount.ToString();
        ratingAnimator.Play("ratingAnim_miss");
    }

    public void ScoreBad()
    {
        accuracy.text = "Bad!";
        scoreCount += 1;
        score.text = scoreCount.ToString();
        ratingAnimator.Play("ratingAnim_bad");
    }

    public void ScoreGood()
    {
        accuracy.text = "Good!";
        scoreCount += 5;
        score.text = scoreCount.ToString();
        ratingAnimator.Play("ratingAnim_good");
    }

    public void ScorePerfect()
    {
        accuracy.text = "Perfect!";
        scoreCount += 10;
        score.text = scoreCount.ToString();
        ratingAnimator.Play("ratingAnim_perfect");
    }
}
