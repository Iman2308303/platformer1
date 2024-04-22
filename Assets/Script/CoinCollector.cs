using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public AudioSource coinSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            GameManager.Instance.CollectCoin();

            PlayCoinSound();

            Destroy(gameObject);
        }
    }
    void PlayCoinSound()
    {
        if (coinSound != null)
        {
            coinSound.Play();
        }
    }
}