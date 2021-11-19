using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;


    private Rigidbody2D body;
    private Vector2 currentSize;
    private Animator anim;
    public float horizontalInput;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private SpriteRenderer isFlipped;

    private bool gameOver = false;
    

    private void Awake()
    {
        // Attached to player and will check player sprite for RigidBody2D and Animation from object.
        body = GetComponent<Rigidbody2D>();
        currentSize = transform.localScale;
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        Debug.Log("Game Start");
        isFlipped = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        if (gameOver)
        {
            // TODO: Go to main menu.
            FindObjectOfType<GameManager>().GameOver();

        }
    }

    // Runs on each frame
    private void FixedUpdate()
    {
      

        horizontalInput = Input.GetAxis("Horizontal");
        
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip player to face left when moving left
        if (horizontalInput > 0.01f)
            //  If we're traveling to the right then keep the sprite as default state.
            transform.localScale = new Vector2(currentSize.x, currentSize.y);
        else if (horizontalInput < -0.01f)
            // Flip the spirte over the x axis if walking to the left.
            transform.localScale = new Vector2(-1 * currentSize.x, currentSize.y);

        //Set animation parameters.
        anim.SetBool("Walking", horizontalInput != 0);
        anim.SetBool("isGrounded", isGrounded());
        
        // This section of code codes for interacting with the enemy sprite
        // I want to go through this when the trigger is activated.

        // Wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 2.5f;

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
            wallJumpCooldown += Time.deltaTime;


    }

private void Jump()
    {
        if (isGrounded())
            {
            // Increase the Y velocity of the sprite by the numerical input from GetAxis multiplied by 
            // the speed variable.
            body.velocity = new Vector2(body.velocity.x, jumpPower);

            // Set the animation for the sprite to Jump
            anim.SetTrigger("Jump");

            // isGrounded bool is set to false after the jump to prevent double jump.
            Debug.Log("Jump Method called and grounded set to false");
        }
        
       
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Debug.Log("Grounded being set to false");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "Enemy")
        {
            // speed *= .5f;
            Debug.Log("Game Over");
            gameOver = true;
            anim.SetBool("Death_b", true);
        }
        else if(other.gameObject.tag == "Egg")
        {
            //Debug.Log("got Egg");
            //Destroy(other.gameObject);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        
        // Returns value of raycast collider beneath player is touching "ground" in a ground layer.
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);

        // Returns value of raycast collider beneath player is touching "ground" in a ground layer.
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

}
