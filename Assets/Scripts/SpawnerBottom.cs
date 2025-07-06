using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBottom : MonoBehaviour
{
    [SerializeField] private float _timeSpawn = 4f;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _leftBound;
    [SerializeField] private Transform _rightBound;
    [SerializeField] private float _spawnHeight = -5f;
    
    private float _timeCounter;
    private bool _canSpawn = true;
    
    private void Start()
    {
        _timeCounter = _timeSpawn;
    }

    private void Update()
    {
        if (!_canSpawn) return;
        
        _timeCounter -= Time.deltaTime;
        if (_timeCounter <= 0)
        {
            SpawnFromBottom();
            _timeCounter = _timeSpawn;
        }
    } 
    void SpawnFromBottom()
    {
        float randomX = Random.Range(_leftBound.position.x, _rightBound.position.x);
        Vector3 spawnPosition = new Vector3(randomX, _spawnHeight, 0);
        
        GameObject newEnemy = Instantiate(_enemy, spawnPosition, Quaternion.identity);
        VerticalMover mover = newEnemy.AddComponent<VerticalMover>();
        mover.SetMoveSpeed(2f);
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
