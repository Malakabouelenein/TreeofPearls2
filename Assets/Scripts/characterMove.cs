using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class characterMove : MonoBehaviour
{
    private HpSystem hpSystem;

    private float horizontal;
    public float forceJump = 8f;



    public float speed = 7f;
    private Animator animator;
    private float x, y, z;
    private Rigidbody2D rb;
    private bool isAlive = true;
    private bool isGrounded = false;
    public GameObject hitbox;
    public float falling = 1f;

    private void Start()
    {
        hpSystem = GetComponent<HpSystem>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StoreScale();
        AudioManager.instance.Play("Level1BGM");
    }

    private void Update()
    {
        isGrounded = IsGrounded();
        FasterFall();

    }

    private void FixedUpdate()
    {
        FlipCharacter();
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (rb.velocity.y > 0f && !isGrounded)
        {
            animator.SetBool("Jump Up", true);
        }
        else
        {
            animator.SetBool("Jump Up", false);
        }

        if (rb.velocity.y < 0f && !isGrounded)
        {
            animator.SetBool("Jump Down", true);
        }
        else
        {
            animator.SetBool("Jump Down", false);
        }

    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!isAlive) return;

        if (context.started)
        {
            animator.SetBool("run", true);
            AudioManager.instance.Play("RunSFX");
        }
        else if (context.canceled)
        {
            animator.SetBool("run", false);
             AudioManager.instance.Stop("RunSFX");
        }

        horizontal = context.ReadValue<Vector2>().x;
    }



    public void Jump(InputAction.CallbackContext context)
    {


        if (!isAlive) return;

        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, forceJump);
            AudioManager.instance.Play("JumpUpSFX");

        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

        }

    }


    public void StopMoving()
    {
        if (!isAlive) return;

        animator.SetBool("run", false);
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!isAlive) return;

        if (context.started)
        {
            animator.SetBool("attack", true);
            hitbox.SetActive(true);
            hpSystem.SetIsAttacking(true);
            AudioManager.instance.Play("AttackSFX");
        }
        else if (context.canceled)
        {
            animator.SetBool("attack", false);
            hitbox.SetActive(false);
            hpSystem.SetIsAttacking(false);
        }
    }



    private bool IsGrounded()
    {
        // Check if the player is grounded here
        return rb.velocity.y == 0;
    }

    private void FasterFall()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.down * Physics2D.gravity.y * (falling - 1) * Time.deltaTime;
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

    private void StoreScale()
    {
        x = transform.localScale.x;
        y = transform.localScale.y;
        z = transform.localScale.z;
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

            //animator.SetBool("Jump Up", true);
            //animator.SetBool("Jump Down", false);
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