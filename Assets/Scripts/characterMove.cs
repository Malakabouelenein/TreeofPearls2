using UnityEngine;

public class characterMove : MonoBehaviour
{
    private HpSystem hpSystem;

    public float forceJump = 8;
    public float speed = 7f;
    private Animator animator;
    private float x, y, z;
    private Rigidbody2D rb;
    private LayerMask groundLayer;
    public float falling = 1f;
    private bool isAlive = true;
    private bool isGrounded = false;
    public GameObject hitbox;
    private void Start()
    {
        hpSystem = GetComponent<HpSystem>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StoreScale();
    }

    private void PlayerMovement(bool isGrounded)
    {
        if (!isAlive) return;

        // Jumping (only if grounded)
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
            animator.SetBool("run", false);
        }
        else
        {
            animator.SetBool("Jump Down", false);

            // Walking (Right or Left)
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                animator.SetBool("run", true);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                animator.SetBool("run", true);
            }
            else
            {
                animator.SetBool("run", false);
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        // Attacking
        if (Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetBool("attack", true);
             hitbox.SetActive(true);
            hpSystem.SetIsAttacking(true);
        }
        else
        {
            animator.SetBool("attack", false);
             hitbox.SetActive(false);
            hpSystem.SetIsAttacking(false);
        }

        // Keep checking the player's jump on Y axis.
        // If player's velocity on Y axis becomes more than 0 or less than 0
        // then the player is jumping.
        if (rb.velocity.y > 0f && isGrounded)
        {
            animator.SetBool("Jump Up", true);
        }

        if (rb.velocity.y < 0f && !isGrounded)
        {
            animator.SetBool("Jump Up", false);
            animator.SetBool("Jump Down", true);

        }
    }

    private void FlipCharacter()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-x, y, z);
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(x, y, z);
        }
    }

    private bool IsGrounded()
    {
        // Check if the player is grounded here
        if (rb.velocity.y == 0) return true;
        else return false;
    }

    private void FasterFall()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.down * Physics2D.gravity.y * (falling - 1) * Time.deltaTime;
        }
    }

    private void StoreScale()
    {
        x = transform.localScale.x;
        y = transform.localScale.y;
        z = transform.localScale.z;
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = IsGrounded();

        PlayerMovement(isGrounded);
        FasterFall();
    }

    private void FixedUpdate()
    {
        FlipCharacter();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trap"))
        {
            isAlive = false;
        }
        if (collision.gameObject.CompareTag("MGround"))
        {
            this.transform.parent = collision.transform;
         
           
            isGrounded = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("trap"))
        {
            isAlive = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("MGround"))
        {
           

            this.transform.parent = null;

            
        }
    }
}
