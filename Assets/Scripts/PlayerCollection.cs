using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerCollection : MonoBehaviour
{
    public int coinCount = 0;
    public TMP_Text CoinCounter;

 

    private void Update()
    {

        UpdateCoinCounter();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("coin"))
        {
            CollectCoin(other.gameObject);
            AudioManager.instance.Play("PearlSFX");
        }
    }

    private void CollectCoin(GameObject coin)
    {
        Destroy(coin);
        coinCount++;
    }

    private void UpdateCoinCounter()
    {

        CoinCounter.text = "" + coinCount;
    }
}
