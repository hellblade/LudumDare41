using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool jumpPressed;
    bool isGrounded;
    float velocity;
    Rigidbody2D body;
    

    float ySize;

    [SerializeField] float jumpHeight = 4;

    float jumpAmount;
    float jumpStrength = 0.0f;
    [SerializeField] float jumpThrust = 0.05f;
    [SerializeField] float maxJumpStrength = 2;

    float gravity;
    bool doJump = false;
    float jumpTime;

    [SerializeField] float maxJumpTime = 0.2f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ySize = GetComponent<SpriteRenderer>().bounds.extents.y;

        gravity = Mathf.Abs(Physics2D.gravity.y);
    }

    void Update()
    {
        jumpPressed = (isGrounded && Input.GetButtonDown("Jump"));

        if (jumpPressed)
        {
            jumpTime = maxJumpTime;
        }

        doJump = false;

        if (jumpTime > 0)
        {
            jumpTime -= Time.deltaTime;
            if (Input.GetButton("Jump"))
            {
                jumpStrength += jumpThrust;
                jumpStrength = Mathf.Clamp(jumpStrength, 0, maxJumpStrength);
                jumpAmount = Mathf.Sqrt(jumpStrength * jumpHeight * gravity);

                Debug.Log("Amount: " + (jumpStrength * jumpHeight * gravity));
                doJump = true;
            }
        }
        else
        {
            jumpStrength = 0f;
            jumpAmount = 0f;
        }

    }

    void FixedUpdate()
    {
        var hit = Physics2D.Raycast(transform.position, -Vector2.up, ySize + 0.1f, LayerMask.GetMask("Not Player"));
        isGrounded = (hit.collider != null);
             
        if (doJump)
        {
            body.velocity = new Vector2(0, jumpAmount);
        }
    }

    public void SetXPosition(float x)
    {
        var pos = transform.position;
        pos.x = x;
        transform.position = pos;
    }    
}