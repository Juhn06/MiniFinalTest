using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   [SerializeField]  Text scoreText;
   [SerializeField] private GameObject gameOverPanel;
   [SerializeField] private Button restartButton;

   private void Start()
   {
      if (GameManager.Instance != null)
      {
         GameManager.Instance.OnScoreUpdated += UpdateScoreText;
      }
      
      if (restartButton != null)
      {
         restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
      }
   }

   private void UpdateScoreText(int score)
   {
      if (scoreText != null)
      {
         scoreText.text = "Score: " + score.ToString();
      }
   } 
   private void OnDestroy()
   {
      if (GameManager.Instance != null)
      {
         GameManager.Instance.OnScoreUpdated -= UpdateScoreText;
      }
   }
   
}
