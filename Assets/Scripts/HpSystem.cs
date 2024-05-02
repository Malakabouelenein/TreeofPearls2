using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HpSystem : MonoBehaviour
{

    private characterMove characterMove;
    public int playerHp = 3;
    public int damageAmount = 1;
    public int heal = 1;
    [SerializeField] private Image[] HpBar;
    private Animator playerAnimator;
    private bool isAttacking = false;
    public bool isHurt = false;

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
        if (collision.gameObject.CompareTag("Enemy") && !isAttacking && !isHurt)
        {
            playerAnimator.SetBool("hurt", true);
            isHurt = true;
            InvokeRepeating("TakeDamage", 1f, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
       {
            playerAnimator.SetBool("hurt", false);
           isHurt = false;
           CancelInvoke("TakeDamage");
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
       // if (collision.gameObject.CompareTag("Enemy") && !isAttacking && !isHurt)
       // {
           // playerAnimator.SetBool("hurt", true);
           // isHurt = true;
           // InvokeRepeating("TakeDamage", 1f, 1f);
       // }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
       // {
            //playerAnimator.SetBool("hurt", false);
           // isHurt = false;
            //CancelInvoke("TakeDamage");
       // }
    //}

    void TakeDamage()
    {
        playerHp -= damageAmount;
        UpdateHealth();
    }

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