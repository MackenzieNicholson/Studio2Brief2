using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 150.0f;
    public bool isFishing = false;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    bool isFacingLeft = false;
    Animator animator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("playerSprite").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        /*moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        transform.position += moveDirection * speed * Time.deltaTime;*/
        if (!PlayerData.isFishing)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            controller.Move(moveDirection * Time.deltaTime);

            if (horizontalInput > 0) //face right
            {
                animator.Play("player_move");
                if (isFacingLeft)
                {
                    isFacingLeft = false;
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
            else if (horizontalInput < 0)//face left
            {
                animator.Play("player_move");
                if (!isFacingLeft)
                {
                    isFacingLeft = true;
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
            else if ((verticalInput > 0) || (verticalInput < 0))
            {
                animator.Play("player_move");
            }
            else //idle
            {
                animator.Play("player_idle");
            }
        }
    }
}
