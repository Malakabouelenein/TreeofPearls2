using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public MonoBehaviour[] componentsToDisableOnPause; 

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        AudioManager.instance.Play("Level1BGM");

        foreach (var component in componentsToDisableOnPause)
        {
            component.enabled = true;
        }
    }

public void BackToMainMenu()
    {
        
        SceneManager.LoadScene(0);
        AudioManager.instance.Stop("Level1BGM");
    }

    public void ButtonsSound()
    {
        
        AudioManager.instance.Play("ConfirmSFX");

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;
        AudioManager.instance.Stop("Level1BGM");
        AudioManager.instance.Stop("HitSFX");
        AudioManager.instance.Stop("DeathSFX");


        foreach (var component in componentsToDisableOnPause)
        {
            component.enabled = false;
        }
    }

}
