using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    
    [Header("Movement Boundaries")]
    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;
    
    [Header("Boundary References (Optional)")]
    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;
    [SerializeField] private Transform topBound;
    [SerializeField] private Transform bottomBound;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool facingRight = true;
    private Collider2D playerCollider;

    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        
        if (playerCollider == null)
        {
            Debug.LogError("Player không có Collider2D!");
        }
        if (leftBound != null) minX = leftBound.position.x;
        if (rightBound != null) maxX = rightBound.position.x;
        if (bottomBound != null) minY = bottomBound.position.y;
        if (topBound != null) maxY = topBound.position.y;
        
        Debug.Log($"Player Boundaries: X({minX} to {maxX}), Y({minY} to {maxY})");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (GameManager.Instance.gameState == GameManager.GameState.Ready)
            {
                GameManager.Instance.gameState = GameManager.GameState.Running;
            }
        }

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        HandleFacing();
        MovePlayerWithBounds();
    }

    void MovePlayerWithBounds()
    {
        if (playerCollider == null) return;
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.deltaTime;
        Bounds playerBounds = playerCollider.bounds;
        float playerWidth = playerBounds.size.x / 2f;
        float playerHeight = playerBounds.size.y / 2f;
        Vector2 boundsOffset = playerBounds.center - transform.position;
        
        newPosition.x = Mathf.Clamp(newPosition.x, 
            minX + playerWidth - boundsOffset.x, 
            maxX - playerWidth - boundsOffset.x);
            
        newPosition.y = Mathf.Clamp(newPosition.y, 
            minY + playerHeight - boundsOffset.y, 
            maxY - playerHeight - boundsOffset.y);
        
        rb.MovePosition(newPosition);
        
        Vector2 debugPos = newPosition + boundsOffset;
        Debug.Log($"Player Center: {newPosition}, Bounds Center: {debugPos}, Left Edge: {debugPos.x - playerWidth}, Right Edge: {debugPos.x + playerWidth}");
    }

    void HandleFacing()
    {
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    public void Dead()
    {
        Debug.Log("Dead");
        GameManager.Instance.PlayerDied();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.Instance.CollectCoin(); 
            Destroy(other.gameObject);
        }
    }
    
    public void SetBoundaries(float minX, float maxX, float minY, float maxY)
    {
        this.minX = minX;
        this.maxX = maxX;
        this.minY = minY;
        this.maxY = maxY;
    }
}