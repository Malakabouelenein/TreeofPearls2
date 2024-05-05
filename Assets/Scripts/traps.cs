using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class traps : MonoBehaviour
{
    private characterMove characterMove2;
    public Animator playerAnimator;

     private void Start()
    {
        characterMove2 = FindObjectOfType<characterMove>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            characterMove2.speed = 0;
            playerAnimator.SetBool("run", false);
            playerAnimator.SetBool("Jump Up", false);
            playerAnimator.SetBool("Jump Down", false);
            playerAnimator.SetBool("die", true);
            AudioManager.instance.Play("DeathSFX");
            AudioManager.instance.Stop("Level1BGM");
            Invoke("RestartScene", 3);


        }
    }


  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            characterMove2.speed = 0;
            playerAnimator.SetBool("run", false);
            playerAnimator.SetBool("Jump Up", false);
            playerAnimator.SetBool("Jump Down", false);
            playerAnimator.SetBool("die", true);
            AudioManager.instance.Play("DeathSFX");
            AudioManager.instance.Stop("Level1BGM");
            Invoke("RestartScene", 3);


        }
    }
    void RestartScene()
    {
        // Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
