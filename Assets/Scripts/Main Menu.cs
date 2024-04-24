using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string resumeKey = "resumeSceneIndex";
    private int defaultSceneIndex = 1;
    public void PlayGame()
    {

        SceneManager.LoadScene(1);
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
    }
}