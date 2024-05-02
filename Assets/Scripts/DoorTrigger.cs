using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public string sceneToLoad;
    public float fadeDuration = 1.0f;
    public bool waitForFade = true;
    public Animator animator;
    public  GameObject gb;

    private void Start()
    {
      
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gb.SetActive(true);
                animator.SetBool("fade", true);
                Invoke("LoadSceneAfterDelay", fadeDuration);
            
        }
    }

    private void LoadSceneAfterDelay()
    {
        if (waitForFade)
        {
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad); 
        }
    }
}
