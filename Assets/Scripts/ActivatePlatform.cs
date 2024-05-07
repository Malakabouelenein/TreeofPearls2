using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActivatePlatform : MonoBehaviour
{

    public GameObject objectToActivate;
    public GameObject enemy;
    public float delay = 30f;
    private float timer = 0f;
    private bool activated = false;

    public TextMeshProUGUI countdownText;

    void Start()
    {
        if (countdownText != null)
        {
            countdownText.text = delay.ToString();
        }
    }

    void Update()
    {
        if (!activated)
        {
            timer += Time.deltaTime;
            float timeRemaining = Mathf.Clamp(delay - timer, 0f, delay);
            UpdateCountdownText(timeRemaining);

            if (timer >= delay)
            {
                objectToActivate.SetActive(true);
                enemy.SetActive(false);
                activated = true;
                // Hide countdown text or do something else if needed
                if (countdownText != null)
                {
                    countdownText.gameObject.SetActive(false);
                }
            }
        }
    }

    void UpdateCountdownText(float timeRemaining)
    {
        if (countdownText != null)
        {
            countdownText.text = Mathf.CeilToInt(timeRemaining).ToString();
        }
    }
}