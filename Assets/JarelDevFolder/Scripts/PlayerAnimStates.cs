using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimStates : MonoBehaviour
{

    Animator animator;
    
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AnimCastLoop()
    {
        animator.Play("player_cast_wait");
    }

    public void EnableFishing()
    {
        playerMovement.isFishing = true;
    }

    public void DisableFishing()
    {
        playerMovement.isFishing = false;
    }
}
