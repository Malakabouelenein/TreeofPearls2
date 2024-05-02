using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyOnHit : MonoBehaviour
{
  
    private Collider2D col; 
    private Rigidbody2D rb;

    void Start()
    {
        col = GetComponent<Collider2D>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
        
            Destroy(gameObject, 2f);

            col.enabled = false;

            //rb.gravityScale = 0;
        }
    }
}