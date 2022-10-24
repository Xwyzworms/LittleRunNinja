using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;
    
    public int health;
    [Header("Move Info")]
    public float speed = 3;
    public float jumpForce = 5;


    public Vector2 wallJumpDirection;


    public string playerName;
    public bool isDead;

    private bool facingRight=true;
    private int facingDirection = 1;

    private bool canMove = false;
    private bool canDoubleJump = true; 


    private float movingInput;

    [Header("Wall Info")]
    public LayerMask wallLayerMask;

    private bool isWallDetected = false;

    public bool canWallSlide = false;
    public bool isWallSliding = false;

    public float wallCheckDistance;

    [Header("Collision Info")]
    public LayerMask whatIsGround;
    public float groundCheckDistance;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {

        FlipController();
        AnimationControllers();
        CollisionChecks();
        InputChecks();
        if(isGrounded) 
        {
            canDoubleJump = true;
            canMove = true;
        }

        if(canWallSlide) 
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }

        if(!isWallDetected) 
        {
            isWallSliding = false;
        }

            Move();
    }

    private void WallChecks() 
    {
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
        if(isWallDetected && rb.velocity.y < 0 ) 
        {
            canWallSlide = true;
        }

        if(!isWallDetected) canWallSlide = false;
    }

    private void WallJump() 
    {
        canMove = false;
        rb.velocity = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y );

    }

    private void Flip() 
    {
        //Check if the player looking at, Rotation
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    
    }


    private void FlipController() 
    {
        if(facingRight && rb.velocity.x < 0) 
        {
            Flip();
        }
        else if (!facingRight && rb.velocity.x > 0) 
        {
            Flip();
        }

    }
    private void AnimationControllers() 
    {

        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    
    private void InputChecks() 
    {
        movingInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxis("Vertical") < 0) 
        {
            canWallSlide = false;
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            // do Jump
            JumpButton();
        }
    }

    private void JumpButton() 
    {

        if ( isWallSliding) 
        {
            WallJump();
        }
        else if(isGrounded) 
        {
            Jump();
        }
        else if(canDoubleJump) 
        {
            canDoubleJump = false;
            Jump();
        }

        canWallSlide = false;
    }


    private void Move() 
    {
        if(canMove) 
            rb.velocity = new Vector2(speed * movingInput, rb.velocity.y);
    }

    private void Jump() 
    {
        rb.velocity = new Vector2(rb.velocity.x , jumpForce);
    }

    private void CollisionChecks() 
    {

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        WallChecks();
    }


    private void OnDrawGizmos() 
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2( transform.position.x  + wallCheckDistance * facingDirection, transform.position.y));
    }
}
