using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDestoryOnHit : MonoBehaviour
{
    private BossController bossController;
    private Animator animator;
    private Collider2D col; 
   

    void Start()
    {
        bossController = GetComponent<BossController>();
        animator = GetComponent<Animator>();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
           
            animator.SetBool("OmAliDeath", true);
            
            bossController.isAlive = false;

            AudioManager.instance.Stop("Level2BGM");
            AudioManager.instance.Play("SucessSFX");
Invoke("win",3f);
            
        }
    }
    void win()
    {
        SceneManager.LoadScene(5);
    }
}