using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool isAlive = true;
    public GameObject projectilePrefab; 
    public Transform throwPoint; 
    public Transform player; 
    public float throwForce = 10f; 
    public float flipThreshold = 0.5f; 
    private Vector3 originalScale; 

    private void Start()
    {
        originalScale = transform.localScale;
      
        StartCoroutine(ThrowProjectilesRoutine());
    }

    private void Update()
    {
        // Calculate direction to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Flip orientation based on player's position
        if (directionToPlayer.x > flipThreshold)
        {
            Flip(false); // Face left
        }
        else if (directionToPlayer.x < -flipThreshold)
        {
            Flip(true); // Face right
        }
    }

    private void Flip(bool faceRight)
    {
        if (faceRight)
        {
            transform.localScale = originalScale;
        }
        else
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
    }

    private void ThrowProjectile(Vector3 direction)
    {
     
        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb != null)
        {
            projectileRb.AddForce(direction * throwForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("Projectile prefab is missing Rigidbody2D component!");
        }
        Destroy(projectile,3f);
    }

    private IEnumerator ThrowProjectilesRoutine()
    {
        while (true && isAlive )
        {
            
            Vector3 directionToPlayer = player.position - transform.position;

            ThrowProjectile(directionToPlayer.normalized);

            
            yield return new WaitForSeconds(1.17f);
        }
    }
}
