using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    
    [Header("UI References")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winPanel;  
    [SerializeField] private Text winText;
    [SerializeField] private Button nextSceneButton;
    
    [Header("Game Settings")]
    [SerializeField] private int winScore = 20; 
    [SerializeField] private string nextSceneName = "Level2";
    
    public enum GameState : byte
    {
        Ready,
        Running,
        GameOver,
        Won
    }
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public GameState gameState = GameState.Ready;
    public int Score = 0;
    
    private int totalCoins = 0;
    private int coinsCollected = 0;
    private bool gameWon = false;
    
    private SpawnerLeft[] spawnerLefts;
    private SpawnerBottom[] spawnerBottoms;
    private SpawnerCoin spawnerCoin;
    private bool gameOverTriggered = false;

    private void Start()
    {
        spawnerLefts = FindObjectsOfType<SpawnerLeft>();
        spawnerBottoms = FindObjectsOfType<SpawnerBottom>();
        spawnerCoin = FindObjectOfType<SpawnerCoin>();
        gameOverTriggered = false;
        
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
        if (nextSceneButton != null)
        {
            nextSceneButton.onClick.AddListener(LoadNextScene);
        }
        Debug.Log($"WIN SCORE SET TO: {winScore}");
        Debug.Log($"CURRENT SCORE: {Score}");
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Ready:
                Time.timeScale = 0;
                break;
                
            case GameState.Running:
                Time.timeScale = 1;
                break;
                
            case GameState.GameOver:
                if (!gameOverTriggered)
                {
                    GameOver();
                }
                break;
            
            case GameState.Won:
                if (!gameWon)
                {
                    ShowWinScreen();
                }
                break;
        }
        if (!gameWon && Score >= winScore && gameState != GameState.GameOver)
        {
            Debug.Log($"WIN CONDITION MET! Score: {Score}, WinScore: {winScore}");
            gameState = GameState.Won;
        }
    }
    
    public void CollectCoin()
    {
        if (gameWon || gameState == GameState.GameOver) return;
        
        coinsCollected++;
        AddScore();
        
        Debug.Log($"COIN COLLECTED! Score: {Score}/{winScore}, GameState: {gameState}");
        if (Score >= winScore)
        {
            Debug.Log("WIN CONDITION TRIGGERED!");
            gameState = GameState.Won;
        }
    }
    
    public void AddCoin()
    {
        totalCoins++;
        Debug.Log($"Total coins: {totalCoins}");
    }

    private void ShowWinScreen()
    {
        gameWon = true;
        
        Debug.Log("SHOWING WIN SCREEN!");
        
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Debug.Log("WIN PANEL ACTIVATED!");
        }
        else
        {
            Debug.LogError("WIN PANEL IS NULL!");
        }
        
        if (winText != null)
        {
            winText.text = $"YOU WIN!\nScore: {Score}";
        }
        if (spawnerLefts != null)
        {
            foreach (var spawner in spawnerLefts)
            {
                spawner.StopSpawning();
            }
        }
        
        if (spawnerBottoms != null)
        {
            foreach (var spawner in spawnerBottoms)
            {
                spawner.StopSpawning();
            }
        }
        
        if (spawnerCoin != null)
        {
            spawnerCoin.StopSpawning();
        }
        
        Time.timeScale = 0;
        Debug.Log("YOU WIN!");
    }
    
    public void LoadNextScene()
    {
        Time.timeScale = 1; 
        
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
    
    void GameOver()
    {
        gameOverTriggered = true;
      
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        Time.timeScale = 0;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1;
        Score = 0;
        totalCoins = 0;
        coinsCollected = 0;
        gameWon = false;
        gameState = GameState.Ready;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void PlayerDied()
    {
        gameState = GameState.GameOver;
        Destroy(gameObject);
        RestartGame();
    }
    
    public void AddScore()
    {
        Score++;
        OnScoreUpdated?.Invoke(Score);
        Debug.Log($"SCORE INCREASED TO: {Score}");
    }
    
    public System.Action<int> OnScoreUpdated;
    [ContextMenu("Test Win Condition")]
    public void TestWinCondition()
    {
        Debug.Log($"Testing Win: Score={Score}, WinScore={winScore}, GameWon={gameWon}");
        if (Score >= winScore && !gameWon)
        {
            gameState = GameState.Won;
        }
    }
}