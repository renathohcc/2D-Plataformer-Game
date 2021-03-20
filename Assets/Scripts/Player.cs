using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    Rigidbody2D rb;
    bool facingRight = true;

    bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    Animator anim;

    public int health;

    void Start()
    {
        //To start the Rigidbody2D component in the player gameObject;
        rb = GetComponent<Rigidbody2D>();
        //To start the animator component in the player gameObject;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Player movement
        float input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (input*speed, rb.velocity.y);

        //To flip the player when he turn into left and right;
        if (input > 0 && facingRight == false){
            Flip();
        }else if (input < 0 && facingRight == true){
            Flip();
        }

        //Code to control the "isRunning" variable to make the idle and run animations
        if(input != 0){
            anim.SetBool("isRunning", true);
        }else{
            anim.SetBool("isRunning", false);
        }

        //Check if player is in the Ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //Code to control the "isJumping" variable to make the player jump animation
        if(isGrounded == true){
            anim.SetBool("isJumping", false);
        }else{
            anim.SetBool("isJumping", true);
        }

        //Make the player jump
        if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true){
            rb.velocity = Vector2.up * jumpForce;
        }

        //Check if player is in the wall
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        //Make the player slide in the wall
        if(isTouchingFront == true && isGrounded == false && input != 0){
            wallSliding = true;
        }else{
            wallSliding = false;
        }

        if(wallSliding){
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        //Make the player jump in the wall
        if(Input.GetKeyDown(KeyCode.UpArrow) && wallSliding == true){
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if(wallJumping == true){
            rb.velocity = new Vector2(xWallForce * -input, yWallForce);
        }


    }
    //Function to flip the player
    void Flip(){
        transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    //Function to make the player back to slide after JumpWall
    void SetWallJumpingToFalse(){
        wallJumping = false;
    }

    //Function to make damage in the player (still working on that)
    public void TakeDamage(int damage){
        health -= damage;
        print(health);
        if (health <= 0){
            Destroy(gameObject);
        }
    }
}
