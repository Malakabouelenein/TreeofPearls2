using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform player;
    public float moveSpeed = 2f;
    public float chaseSpeed = 2.5f;
    public float attackRange = 5f;
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
    }

    private void Update()
    {
        if (!isAttacking)
        {
            // Calculate movement direction
            Vector3 movementDirection = transform.position - previousPosition;

            if (movementDirection.x < 0)
            {
                // Move left: flip the enemy
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
            else if (movementDirection.x > 0)
            {
                // Move right: reset the scale to the original
                transform.localScale = originalScale;
            }

            previousPosition = transform.position;

            if (!isChasing)
            {
                // Move the enemy towards the target point
                transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

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
                // Chase the player
                transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

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
