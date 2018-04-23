using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    bool jumpPressed;
    bool isGrounded;
    float velocity;
    Rigidbody2D body;


    float ySize;
    float xSize;

    [SerializeField] float jumpHeight = 4;

    float jumpAmount;
    float jumpStrength = 0.0f;
    [SerializeField] float jumpThrust = 0.05f;
    [SerializeField] float maxJumpStrength = 2;
    [SerializeField] UnityEvent playerLost = new UnityEvent();

    float gravity;
    bool doJump = false;
    float jumpTime;

    [SerializeField] float maxJumpTime = 0.2f;

    bool wantToJump;
    Vector3 initialPosition;

    GeneratorManager manager;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<GeneratorManager>();

        var size = GetComponent<SpriteRenderer>().bounds.extents;
        ySize = size.y;
        xSize = size.x;

        gravity = Mathf.Abs(Physics2D.gravity.y);
        initialPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            wantToJump = true;
        }

        jumpPressed = (isGrounded && wantToJump);

        if (jumpPressed)
        {
            jumpTime = maxJumpTime;
        }

        doJump = false;

        if (jumpTime > 0)
        {
            jumpTime -= Time.deltaTime;
            if (Input.GetButton("Jump") || wantToJump)
            {
                jumpStrength += jumpThrust;
                jumpStrength = Mathf.Clamp(jumpStrength, 0, maxJumpStrength);
                jumpAmount = Mathf.Sqrt(jumpStrength * jumpHeight * gravity);

                doJump = true;
                wantToJump = false;
            }
            // TODO: Dampen it once released? 
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

        // Check if lost
        if (transform.position.y + ySize * 2 < -manager.ScreenAmountY / 2 ||
           transform.position.x + xSize * 2 < -manager.ScreenAmountX / 2)
        {
            playerLost.Invoke();
        }
    }

    public void SetXPosition(float x)
    {
        var pos = transform.position;
        pos.x = x;
        transform.position = pos;
    }

    public void Reset()
    {
        body.velocity = Vector3.zero;
        transform.position = initialPosition;
    }
}