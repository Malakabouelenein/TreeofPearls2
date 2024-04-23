using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyOnHit : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
            Destroy(gameObject); 
        }
    }
}
