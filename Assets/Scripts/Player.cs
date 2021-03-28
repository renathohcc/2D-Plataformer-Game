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

    public float timeBetweenAttacks;
    float nextAttackTime;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;

    public int damage;

    public SpriteRenderer weaponRenderer;

    public GameObject blood;

    AudioSource source;
    public AudioClip jumpSound;
    public AudioClip hurtSound;

    void Start()
    {
        //To start the Rigidbody2D component in the player gameObject;
        rb = GetComponent<Rigidbody2D>();
        //To start the animator component in the player gameObject;
        anim = GetComponent<Animator>();
        //To start the audio source component in the player gameObject;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //To make the player attack animation
        if(Time.time > nextAttackTime){
            if(Input.GetKey(KeyCode.Space)){

                FindObjectOfType<CameraShake>().Shake();
                anim.SetTrigger("attack");
                nextAttackTime = Time.time + timeBetweenAttacks;
            }
            
        }

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
            //To play the jump sound
            source.clip = jumpSound;
            source.Play();
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
            //To play the jump sound
            source.clip = jumpSound;
            source.Play();
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
        FindObjectOfType<CameraShake>().Shake();
        //To play the player hurt sound
        source.clip = hurtSound;
        source.Play();
        health -= damage;
        Instantiate(blood, transform.position, Quaternion.identity);
        print(health);
        if (health <= 0){
            Destroy(gameObject);
        }
    }

    //Function to see the attack range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    //Function to attack
    public void Attack(){
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
                foreach (Collider2D col in enemiesToDamage)
                {
                    col.GetComponent<Enemy>().TakeDamage(damage);
                }
    }

    //Function to switch the player Weapon
    public void Equip(Weapon weapon){
        damage = weapon.damage;
        attackRange = weapon.attackRange;
        weaponRenderer.sprite = weapon.GXF;
        Destroy(weapon.gameObject);
    }
}
