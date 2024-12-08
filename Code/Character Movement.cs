using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public float Speed;
    public Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask LayerGround;
    [SerializeField] private LayerMask LayerWall;
    private float WallJumpCooldown;
    private float CharacterInput;

    private bool isTouchingWall = false;

    public float coyoteTimejump = 0.2f;
    private float timeleft;
    private bool coyoteActive;

    public float power = 10f;
    private float powerbuffer = 0.1f;
    private float powerbeingpressed;

    public float fallingspeed = -10f;
    public float JumpModifier = 0.5f;

    public float maxJumpHoldTime = 0.2f;
    private float jumpHoldTimer;
    private bool isJumping;

    private bool canWallJump = true;

    private bool isPlayingRunSound = false;
    private bool isPlayingJumpSound = false;
    private bool isPlayingWallJumpSound = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        CharacterInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(CharacterInput * Speed, body.velocity.y);

        if (CharacterInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (CharacterInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        anim.SetBool("run", CharacterInput != 0);

        if (CharacterInput != 0 && ifGeokiGrounded())
        {
            if (!isPlayingRunSound)
            {
                SFXManager.instance.PlayRunSound();
            }
        }
        else
        {
            SFXManager.instance.StopRunSound();
        }

        if (isJumping && Input.GetKey(KeyCode.Space) && jumpHoldTimer < maxJumpHoldTime)
        {
            jumpHoldTimer += Time.deltaTime;
        }

        if (WallJumpCooldown > 0.2f)
        {
            if (ifGeokionThewall() && ifGeokiGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 1;

            if (Input.GetKeyDown(KeyCode.Space) && (ifGeokiGrounded() || ifGeokionThewall()))
            {
                powerbeingpressed = Time.time;
                jumpHoldTimer = 0f;
                Jump();
                isJumping = true;
            }
        }
        else WallJumpCooldown += Time.deltaTime;

        anim.SetBool("grounded", ifGeokiGrounded());

        if (ifGeokiGrounded())
        {
            timeleft = Time.time;
            coyoteActive = true;
        }
        else if (Time.time - timeleft > coyoteTimejump)
        {
            coyoteActive = false;
        }
    }

    private void FixedUpdate()
    {
        if (body.velocity.y < fallingspeed)
        {
            body.velocity = new Vector2(body.velocity.x, fallingspeed);
        }

        if (isJumping && body.velocity.y > 0 && (!Input.GetKey(KeyCode.Space) || jumpHoldTimer >= maxJumpHoldTime))
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * JumpModifier);
        }
    }

    private bool Jumpingwooosh()
    {
        bool bufferJump = Time.time - powerbeingpressed <= powerbuffer;
        bool Coyotetime = coyoteActive && !ifGeokiGrounded();

        return bufferJump || Coyotetime;
    }

    private void Jump()
    {
        if (ifGeokiGrounded() || Jumpingwooosh())
        {
            body.velocity = new Vector2(body.velocity.x, power);
            anim.SetTrigger("jump");
            isJumping = true;
            coyoteActive = false;

            if (!isPlayingJumpSound)
            {
                SFXManager.instance.PlayJumpSound();
                isPlayingJumpSound = true;
            }
        }
        else if (ifGeokionThewall() && !ifGeokiGrounded())
        {
            if (canWallJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (CharacterInput == 0)
                    {
                        body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                        transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    }
                    else
                    {
                        body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
                    }

                    WallJumpCooldown = 0;
                    canWallJump = false;
                    SFXManager.instance.PlayWallJumpSound();
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            anim.ResetTrigger("jump");
            canWallJump = true;
            isPlayingJumpSound = false; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

    private bool ifGeokiGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, LayerGround);
        return raycastHit.collider != null;
    }

    private bool ifGeokionThewall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            new Vector2(transform.localScale.x, 0),
            0.1f,
            LayerWall
        );

        bool currentlyTouchingWall = raycastHit.collider != null;

        if (currentlyTouchingWall && !isTouchingWall)
        {
            Debug.Log("Wall detected, playing sound.");
            SFXManager.instance.PlayWallJumpSound();
        }
        isTouchingWall = currentlyTouchingWall;

        return currentlyTouchingWall;
    }

    public bool BisaNyerang()
    {
        return CharacterInput == 0 && ifGeokiGrounded() && !ifGeokionThewall();
    }
}
