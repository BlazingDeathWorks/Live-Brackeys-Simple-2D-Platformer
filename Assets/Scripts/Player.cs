using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Object attributes
    [SerializeField] private float moveSpeed = 4f; 
    [SerializeField] private float jumpSpeed = 6f; 

    //Ground Check attributes
    [SerializeField] private Transform groundCheckPos; 
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private Vector2 groundCheckSize;

    //Respawn attributes
    [SerializeField] private float yLimit = -5f; 
    [SerializeField] private Vector2 spawnPoint; 

    private float movement; 
    private bool isGrounded;
    private bool isJumping;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");

        //Jump Input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            isJumping = true;
        }
    }

    private void FixedUpdate() 
    {
        rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);

        if (isJumping) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        //Respawn on Enemy Collision
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            transform.position = spawnPoint;
        }
    }
}