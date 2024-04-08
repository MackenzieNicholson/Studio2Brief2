using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAccuracy : MonoBehaviour
{
    Animator animator;
    public ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreMiss()
    {
        scoreManager.scoreCount -= 5;
        scoreManager.score.text = scoreManager.scoreCount.ToString();
    }

    public void ScoreBad()
    {
        scoreManager.scoreCount--;
        scoreManager.score.text = scoreManager.scoreCount.ToString();
    }

    public void ScoreGood()
    {
        scoreManager.scoreCount++;
        scoreManager.score.text = scoreManager.scoreCount.ToString();
    }

    public void ScorePerfect()
    {
        scoreManager.scoreCount += 5;
        scoreManager.score.text = scoreManager.scoreCount.ToString();
    }

    public void StopAnimating()
    {
        animator.Play("ratingAnim_empty");
    }
}
