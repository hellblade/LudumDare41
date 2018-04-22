using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool jumpPressed;
    Rigidbody2D body;
    Collider2D collider2d;

    float ySize;

    [SerializeField] float jumpAmount = 7;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ySize = GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void Update()
    {
        if (!jumpPressed && Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
        
    }

    void FixedUpdate()
    {
        var hit = Physics2D.Raycast(transform.position, -Vector2.up, ySize + 0.1f, LayerMask.GetMask("Not Player"));
        bool isOnGround = false;

        if (hit.collider != null)
        {
            isOnGround = true;
        }

     
        if (jumpPressed && isOnGround)
        {
            body.velocity = new Vector2(0, jumpAmount);
            jumpPressed = false;
        }


    }
}