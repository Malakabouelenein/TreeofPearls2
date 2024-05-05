using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isAlive = true;
    private Animator animator;
    public Transform pointA;
    public Transform pointB;
    public Transform player;
    public float moveSpeed = 2f;
    public float chaseSpeed = 2.5f;
    public float attackRange = 5f;
    public float minAttackDistance = 2f; 
    public float attackDelay = 1f;
    private bool isAttacking = false;
    private bool isChasing = false;
    private Transform targetPoint;
    private Vector3 originalScale;
    private Vector3 previousPosition;

    private void Start()
    {
        targetPoint = pointA;
        originalScale = transform.localScale;
        previousPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isAttacking && isAlive)
        {
            // Calculate movement direction
            Vector3 movementDirection = transform.position - previousPosition;
          
            if (movementDirection.x > 0)
            {
                // Move left: flip the enemy
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
            else if (movementDirection.x < 0)
            {
                // Move right: reset the scale to the original
                transform.localScale = originalScale;
            }

            previousPosition = transform.position;

            if (!isChasing && isAlive)
            {
                // Move the enemy towards the target point
                transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
                animator.SetBool("EneWalk", true);
                animator.SetBool("EneAttack", false);

                // If the enemy reaches the target point, switch to the other point
                if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
                {
                    if (targetPoint == pointA)
                    {
                        targetPoint = pointB;
                    }
                    else
                    {
                        targetPoint = pointA;
                    }
                }
            }
            else
            {
                // Calculate direction towards the player
                Vector3 directionToPlayer = player.position - transform.position;
                directionToPlayer.y = 0f; 

                // Normalize the direction vector
                directionToPlayer.Normalize();

                // Move the enemy towards the player on the X-axis
                transform.position += directionToPlayer * chaseSpeed * Time.deltaTime;

                // Ensure minimum attack distance
                if (Vector3.Distance(transform.position, player.position) < minAttackDistance)
                {
                    animator.SetBool("EneAttack", true);
                    animator.SetBool("EneWalk", false);
                    // Move away from the player
                    transform.position -= directionToPlayer * (minAttackDistance - Vector3.Distance(transform.position, player.position));
                }

                // If player in attack range, attack
                if (Vector3.Distance(transform.position, player.position) <= attackRange)
                {
                   
                    Attack();
                }
            }

            // Check if player is between the points
            if (IsPlayerBetweenPoints())
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            //attack the player
            isAttacking = true;
            Attack();
        }
    }

    private void Attack()
    {
       
        // Flip the enemy sprite to face the player
        if (player.position.x > transform.position.x)
        {
            // Player is on the left side of the enemy
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            // Player is on the right side of the enemy
            transform.localScale = originalScale;
        }
        // Debug.Log("Attacking player!");
        StartCoroutine(ResetAttackFlag());
    }

    private IEnumerator ResetAttackFlag()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }

    private bool IsPlayerBetweenPoints()
    {
        Vector3 AB = pointB.position - pointA.position;
        Vector3 AP = player.position - pointA.position;

        float dotProduct = Vector3.Dot(AB, AP);

        if (dotProduct > 0 && dotProduct < Vector3.Dot(AB, AB))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}