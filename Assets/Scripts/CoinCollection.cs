using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    [Header("Coin Settings")]
    public int coinValue = 1;
    public GameObject collectEffect; 
    public AudioClip collectSound;  
    
    private bool isCollected = false;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            CollectCoin();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            CollectCoin();
        }
    }
    
    void CollectCoin()
    {
        isCollected = true;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CollectCoin();
        }
        
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
        
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, transform.rotation);
        }
        
        Destroy(gameObject);
    }
}
