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

    public void ScoreUpdate(int rating)
    {
        switch (rating)
        {
            case 0:     //miss
                accuracy.text = "Miss!";
                ratingAnimator.Play("ratingAnim_bad");
                break;
            case 1:     //bad
                accuracy.text = "Bad!";
                ratingAnimator.Play("ratingAnim_bad");
                break;
            case 2:     //good
                accuracy.text = "Good!";
                ratingAnimator.Play("ratingAnim_good");
                break;
            case 3:     //perfect
                accuracy.text = "Perfect!";
                ratingAnimator.Play("ratingAnim_perfect");
                break;
            default:
                break;
        }
        
    }
}
