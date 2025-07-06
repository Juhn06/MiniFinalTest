using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMover : MonoBehaviour
{
    private float _moveSpeed = 2f;
    
    private void Update()
    {
        transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
    }
    
    public void SetMoveSpeed(float speed)
    {
        _moveSpeed = speed;
    }
}