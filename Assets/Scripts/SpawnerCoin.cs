using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCoin : MonoBehaviour
{
    [SerializeField] private float _timeSpawn = 3f;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _leftBound;
    [SerializeField] private Transform _rightBound;
    [SerializeField] private Transform _topBound;
    [SerializeField] private Transform _bottomBound;
    
    private float _timeCounter;
    private bool _canSpawn = true;
    
    private void Start()
    {
        _timeCounter = _timeSpawn;
        for (int i = 0; i < 5; i++)
        {
            SpawnRandomCoin();
        }
    }

    private void Update()
    {
        if (!_canSpawn) return;
        
        _timeCounter -= Time.deltaTime;
        if (_timeCounter <= 0)
        {
            SpawnRandomCoin();
            _timeCounter = _timeSpawn;
        }
    }
    
    void SpawnRandomCoin()
    {
        float randomX = Random.Range(_leftBound.position.x, _rightBound.position.x);
        float randomY = Random.Range(_bottomBound.position.y, _topBound.position.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);
        
        GameObject newCoin = Instantiate(_coinPrefab, spawnPosition, Quaternion.identity);
   
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddCoin();
        }
    }
    
    public void StopSpawning()
    {
        _canSpawn = false;
    }
    
    public void StartSpawning()
    {
        _canSpawn = true;
    }
}