using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sprite;
    public BoxCollider2D coll;
    public LayerMask jumpableGround;

    public float jumpForce;
    public float moveForce;
    //private bool isGrounded;
    
    private float moveHorizontal;
    //private float moveVertical;

    private enum MovementState { idle, runnning, jumping, falling}

    [SerializeField] private AudioSource jumpSoundEffect;

    private void Start()
    {
        //isGrounded = false;
    }
    
    private void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * moveForce, rb.velocity.y);

        if (Input.GetButtonDown("Vertical") && isGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
           
        }
        UpdateAnimationState();
    }

   

    //Flip character
    private void UpdateAnimationState()
    {
        MovementState state;

        if(moveHorizontal>0)
        {
            state = MovementState.runnning;
            sprite.flipX = false;
        }
        else if(moveHorizontal<0)
        {
            state = MovementState.runnning;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y>0.1)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y<-0.1)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down,0.1f,jumpableGround);
    }
 
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Platform")
        {
            //anim.SetBool("isJumping", false);
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            //anim.SetBool("isJumping", true);
            isGrounded = false;
        }
    }*/
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = false;
        }
    }*/
}

