using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMover : MonoBehaviour
{
    private float _moveSpeed = 2f;
    private float _destroyHeight = 10f;
    
    public void SetMoveSpeed(float speed)
    {
        _moveSpeed = speed;
    }
    
    private void Update()
    {
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
        if (transform.position.y > _destroyHeight)
        {
            Destroy(gameObject);
        }
    }
}
