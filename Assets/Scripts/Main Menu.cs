using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public GameObject SplashScreen;
    private const string resumeKey = "resumeSceneIndex";
    private int defaultSceneIndex = 1;
    public void PlayGame()
    {
        AudioManager.instance.Stop("MainBGM");
        AudioManager.instance.Play("ConfirmSFX");
        SplashScreen.SetActive(true);
        
    }


    public void ResumeGame()
    {
      
        if (PlayerPrefs.HasKey(resumeKey))
        {
            int resumeSceneIndex = PlayerPrefs.GetInt(resumeKey);
            SceneManager.LoadScene(resumeSceneIndex);
        }
        else
        {
           
            SceneManager.LoadScene(defaultSceneIndex);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
        AudioManager.instance.Play("ConfirmSFX");
    }

    public void Control()
    {
        
        AudioManager.instance.Play("ConfirmSFX");
    }
    private void Start()
    {
        AudioManager.instance.Play("MainBGM");
    }
}