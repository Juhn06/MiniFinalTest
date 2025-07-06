// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class SpawnerLeft : MonoBehaviour
// {
//     [SerializeField] private float _timeSpawn = 4f;
//     [SerializeField] private GameObject _fish;
//     [SerializeField] private Transform _topBound;
//     [SerializeField] private Transform _bottomBound;
//     [SerializeField] private float _spawnOffsetX = -10f; 
//     
//     private float _timeCounter;
//     private bool _canSpawn = true;
//     
//     private void Start()
//     {
//         _timeCounter = _timeSpawn;
//     }
//     
//     private void Update()
//     {
//         if (!_canSpawn) return;
//         
//         _timeCounter -= Time.deltaTime;
//         if (_timeCounter <= 0)
//         {
//             SpawnFromLeft();
//             _timeCounter = _timeSpawn;
//         }
//     }
//     
//     void SpawnFromLeft()
//     {
//         float randomY = Random.Range(_bottomBound.position.y, _topBound.position.y);
//         Vector3 spawnPosition = new Vector3(_spawnOffsetX, randomY, 0);
//         
//         GameObject newFish = Instantiate(_fish, spawnPosition, Quaternion.identity);
//         
//         // Thêm component để di chuyển sang phải
//         HorizontalMover mover = newFish.AddComponent<HorizontalMover>();
//         mover.SetMoveSpeed(3f);
//         
//         // Thêm component để tự hủy khi ra khỏi boundary
//       
//     }
//     
//     public void StopSpawning()
//     {
//         _canSpawn = false;
//     }
//     
//     public void StartSpawning()
//     {
//         _canSpawn = true;
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLeft : MonoBehaviour
{
    [SerializeField] private float _timeSpawn = 4f;
    [SerializeField] private GameObject _fish;
    [SerializeField] private Transform _topBound;
    [SerializeField] private Transform _bottomBound;
    [SerializeField] private float _spawnOffsetX = -10f;
    
    private float _timeCounter;
    private bool _canSpawn = true;
    
    private void Start()
    {
        _timeCounter = _timeSpawn;
        
        // Debug để kiểm tra giá trị của bounds
        if (_topBound != null && _bottomBound != null)
        {
            Debug.Log($"Top Bound Y: {_topBound.position.y}");
            Debug.Log($"Bottom Bound Y: {_bottomBound.position.y}");
            Debug.Log($"Spawn Range: {Mathf.Abs(_topBound.position.y - _bottomBound.position.y)}");
        }
        else
        {
            Debug.LogError("Top Bound hoặc Bottom Bound chưa được gán!");
        }
    }
    
    private void Update()
    {
        if (!_canSpawn) return;
        
        _timeCounter -= Time.deltaTime;
        if (_timeCounter <= 0)
        {
            SpawnFromLeft();
            _timeCounter = _timeSpawn;
        }
    }
    
    void SpawnFromLeft()
    {
        // Kiểm tra null reference
        if (_topBound == null || _bottomBound == null)
        {
            Debug.LogError("Top Bound hoặc Bottom Bound bị null!");
            return;
        }
        
        if (_fish == null)
        {
            Debug.LogError("Fish prefab chưa được gán!");
            return;
        }
        
        // Đảm bảo topBound luôn cao hơn bottomBound
        float minY = Mathf.Min(_topBound.position.y, _bottomBound.position.y);
        float maxY = Mathf.Max(_topBound.position.y, _bottomBound.position.y);
        
        // Tạo vị trí spawn ngẫu nhiên
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(_spawnOffsetX, randomY, 0);
        
        // Debug để kiểm tra vị trí spawn
        Debug.Log($"Spawn Position: {spawnPosition}");
        
        GameObject newFish = Instantiate(_fish, spawnPosition, Quaternion.identity);
        
        // Thêm component để di chuyển sang phải
        HorizontalMover mover = newFish.AddComponent<HorizontalMover>();
        mover.SetMoveSpeed(3f);
        
        // Thêm component để tự hủy khi ra khỏi boundary
        // AutoDestroy destroyer = newFish.AddComponent<AutoDestroy>();
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