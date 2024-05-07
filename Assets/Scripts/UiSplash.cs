using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiSplash : MonoBehaviour
{
    

  public void Cut()
    {
        SceneManager.LoadScene(2);
    }
    public void win()
    {
        SceneManager.LoadScene(0);
    }
    public void lose()
    {
        SceneManager.LoadScene(0);
    }

    public void rest()
    {
        SceneManager.LoadScene(2);
        AudioManager.instance.Play("Level2BGM");
    }
}
