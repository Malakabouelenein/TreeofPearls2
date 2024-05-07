using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HpSystem2 : MonoBehaviour
{
    private characterMove2 characterMove;
    public int playerHp = 3;
    public int damageAmount = 1;
    [SerializeField] private Image[] HpBar;
    private Animator playerAnimator;
    private bool isAttacking = false;
    private bool isHurt = false;

    private void Start()
    {
        characterMove = GetComponent<characterMove2>();
        playerAnimator = GetComponent<Animator>();
        isHurt = false;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        if (playerHp <= 0)
        {
            // Disable movement and attack
            characterMove.enabled = false;
            // Optionally, disable other components or functionalities related to attack
            AudioManager.instance.Stop("Level2BGM");
            AudioManager.instance.Play("GameOverSFX");
            // Trigger death animation
            playerAnimator.SetBool("run", false);
            playerAnimator.SetBool("Jump Up", false);
            playerAnimator.SetBool("Jump Down", false);
            playerAnimator.SetBool("die", true);

            AudioManager.instance.Play("DeathSFX");
            AudioManager.instance.Stop("Level1BGM");
            AudioManager.instance.Stop("HitSFX");
            Invoke("lose", 3);
            Invoke("StopDeathSFX", 0.7f);
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
            AudioManager.instance.Play("HitSFX");
            isHurt = true;
            InvokeRepeating("TakeDamage", 1f, 1f);
        }
        if (collision.gameObject.CompareTag("oba2b") )
        {
            playerAnimator.SetBool("hurt", true);
            AudioManager.instance.Play("HitSFX");
            isHurt = true;
            TakeDamage();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAnimator.SetBool("hurt", false);
            AudioManager.instance.Stop("HitSFX");
            isHurt = false;
        }
         if (collision.gameObject.CompareTag("oba2b") )
        {
            Invoke("playAnimationLate",0.7f);
            AudioManager.instance.Stop("HitSFX");
            isHurt = false;
            
        }
    }
public void SetIsAttacking(bool value)
    {
        isAttacking = value;
    }
    private void playAnimationLate()
    {
    playerAnimator.SetBool("hurt", false);
    }

    void TakeDamage()
    {
        playerHp -= damageAmount;
        UpdateHealth();
    }

    void lose()
    {
        SceneManager.LoadScene(4);
    }

    void StopDeathSFX()
    {
        AudioManager.instance.Stop("DeathSFX");
    }
}