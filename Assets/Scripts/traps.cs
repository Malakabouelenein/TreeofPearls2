using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class traps : MonoBehaviour
{

    public Animator playerAnimator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {

            playerAnimator.SetBool("run", false);
            playerAnimator.SetBool("Jump Up", false);
            playerAnimator.SetBool("Jump Down", false);
            playerAnimator.SetBool("die", true);
            Invoke("RestartScene", 3);


        }
    }


  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            playerAnimator.SetBool("run", false);
            playerAnimator.SetBool("Jump Up", false);
            playerAnimator.SetBool("Jump Down", false);
            playerAnimator.SetBool("die", true);
            Invoke("RestartScene", 3);


        }
    }
    void RestartScene()
    {
        // Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
