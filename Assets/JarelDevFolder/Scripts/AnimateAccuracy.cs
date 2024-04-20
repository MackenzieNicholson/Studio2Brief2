using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateAccuracy : MonoBehaviour
{
    Animator animator;
    Image imageRender;

    public Sprite ratingMiss;
    public Sprite ratingPerfect;
    public Sprite ratingGood;
    public Sprite ratingBad;

    public ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        imageRender = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScoreMiss()
    {
        imageRender.sprite = ratingMiss;
        scoreManager.scoreCount -= 10;
        scoreManager.score.text = scoreManager.scoreCount.ToString();
    }

    public void ScoreBad()
    {
        imageRender.sprite = ratingBad;
        scoreManager.scoreCount++;
        scoreManager.score.text = scoreManager.scoreCount.ToString();
    }

    public void ScoreGood()
    {
        imageRender.sprite = ratingGood;
        scoreManager.scoreCount += 3;
        scoreManager.score.text = scoreManager.scoreCount.ToString();
    }

    public void ScorePerfect()
    {
        imageRender.sprite = ratingPerfect;
        scoreManager.scoreCount += 6;
        scoreManager.score.text = scoreManager.scoreCount.ToString();
    }

    public void StopAnimating()
    {
        animator.Play("ratingAnim_empty");
        //animator.StopPlayback();
    }
}
