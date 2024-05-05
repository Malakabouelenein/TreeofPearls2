using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyOnHit : MonoBehaviour
{
    private Animator animator;
    private Collider2D col; 
   private EnemyController enemyController;

    void Start()
    {
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
           enemyController.isAlive = false;
            animator.SetBool("EneDeath", true);
            animator.SetBool("EneWalk", false);
            animator.SetBool("EneAttack", false);
            Destroy(gameObject, 2f);

            col.enabled = false;

            
        }
    }
}