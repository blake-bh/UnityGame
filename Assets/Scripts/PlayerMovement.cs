using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   private Rigidbody2D rb;
   private BoxCollider2D col;
   private Animator anim;
   [SerializeField] private LayerMask jumpableGround;
   private float dirX= 0f;
   private SpriteRenderer sprite;
   [SerializeField]private float moveSpeed = 7f;
   [SerializeField]private float jumpForce = 14f;
   private enum MovementState {idle, running, jumping, falling, sliding};
   [SerializeField] private AudioSource jumpSoundEffect;

   private bool isWallSliding = false;
   private float wallSlidingSpeed = 2f;

   private bool isWallJumping;
   private bool wallJumpingDirection;
   private float wallJumpingTime= 0.2f;
   private float wallJumpingCounter;
   private float wallJumpingDuration = 0.4f;
   private Vector2 wallJumpingPower = new Vector2(8f,16f);

   [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform wallCheck2;

   [SerializeField] private LayerMask wallLayer;


    
    // Start is called before the first frame update
   private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite  = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
   private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


        if( Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        WallSlide();
        WallJump();
        UpdateAnimationState();
       

        
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if(dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else 
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f && isWallSliding == false)
        {
            state = MovementState.falling;
        }
        else if(isWallSliding == true)
        {
            state = MovementState.sliding;
        }

        anim.SetInteger("State", (int)state);
    }


    private bool IsGrounded()
    {
       return Physics2D.BoxCast(col.bounds.center,col.bounds.size,0f,Vector2.down,.1f, jumpableGround);
    }

    private bool isWalled()
    { 
        if(sprite.flipX == false)
        {
            return Physics2D.OverlapCircle(wallCheck.position, 0.2f,wallLayer);
        }
        else
        {
            return Physics2D.OverlapCircle(wallCheck2.position, 0.2f,wallLayer);
        }
        
       
    }

    private void WallSlide()
    {
        if( isWalled() )
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void WallJump()
    {
        if(isWallSliding)
        {
            isWallJumping = false;
           /*  if(sprite.flipX == false)
            {
                wallJumpingDirection = true;
            }
            else
            {
                wallJumpingDirection = false;
            } */
            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(stopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        
        if(Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
           // sprite.flipX = wallJumpingDirection;
            rb.velocity = new Vector2(wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
        }
        Invoke(nameof(stopWallJumping), wallJumpingDuration);
    }

    private void stopWallJumping()
    {
        isWallJumping = false;
    }

}
