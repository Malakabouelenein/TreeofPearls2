using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HpSystem : MonoBehaviour
{
    private characterMove characterMove;
    public int playerHp = 3; // Initial health of the player
    public int damageAmount = 1; // Amount of damage to apply on collision
    public int heal = 1;
    [SerializeField] private Image[] HpBar;
    private Animator playerAnimator;
    private bool isAttacking = false;
    private void Start()
    {
        characterMove = GetComponent<characterMove>();
        playerAnimator = GetComponent<Animator>();
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        if (playerHp <= 0)
        {
            characterMove.speed = 0;
            characterMove.forceJump = 0;
            playerAnimator.SetBool("run", false);
            playerAnimator.SetBool("Jump Up", false);
            playerAnimator.SetBool("Jump Down", false);
            playerAnimator.SetBool("die", true);
            Invoke("RestartScene", 3);
        }

        for (int i = 0; i < HpBar.Length; i++)
        {
            if (i < playerHp)
            {
                HpBar[i].color = Color.white;
            }
            else
            {
                HpBar[i].color = Color.black;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isAttacking)
        {
            playerHp -= damageAmount;
            playerAnimator.SetBool("run", false);
            playerAnimator.SetBool("Jump Up", false);
            playerAnimator.SetBool("Jump Down", false);

            playerAnimator.SetBool("hurt", true);
          
            UpdateHealth();
        }
        else
        {
            playerAnimator.SetBool("hurt", false);
        }

    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isAttacking)
        {
            

            playerAnimator.SetBool("hurt", false);

            
        }
    }
    //private void OnTriggerStay(Collider other)
    //{


    //    if (other.gameObject.CompareTag("heal") && Input.GetKeyDown(KeyCode.Q))
    //    {
    //        playerHp += heal;


    //        UpdateHealth();
    //    }
    //}



    public void SetIsAttacking(bool value)
    {
      
        isAttacking = value;
    }



    void RestartScene()
    {
        // Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
