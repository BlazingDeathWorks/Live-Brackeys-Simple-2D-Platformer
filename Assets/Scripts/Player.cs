using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Components
    [SerializeField] private Rigidbody2D rb; //Physics of the object, like falling and moving.

    //Object attributes
    [SerializeField] private float moveSpeed = 4f; //How fast the object moves.
    [SerializeField] private float jumpSpeed = 6f; //How high the object jumps.

    //Ground Check attributes
    [SerializeField] private Transform groundCheck; //Child object that detects ground.
    [SerializeField] private LayerMask groundLayer; //The layer detected as the ground layer.
    [SerializeField] private Vector2 groundCheckSize; //Area that detects the ground layer.

    //Respawn attributes
    [SerializeField] private float yLimit = -5f; //How low is too low.
    [SerializeField] private Vector2 spawnPoint; //Where the object respawns.

    //Unedited variables
    private float movement; //The direction the object is moving in.
    private bool isGrounded; //Can the object jump?

    private void Awake() //Happens before everything else in a scene. Good for initializing variables.
    {
        rb = GetComponent<Rigidbody2D>(); //If you do not want to assign the Rigidbody2D in the inspector.
    }

    private void Update() //Every frame, is affected by frame rate. Good for input, bad for physics.
    {
        movement = Input.GetAxisRaw("Horizontal"); //Use 'GetAxis' for smooth acceleration and 'GetAxisRaw' for immediate acceleration.

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed); //On that frame, the object goes up.
        }

        if (transform.position.y <= yLimit) //Checks if the object is too low.
        {
            transform.position = spawnPoint; //Respawns.
        }
    }

    private void FixedUpdate() //Every 0.02 seconds, or 50 times per second. Good for physics, bad for input.
    {
        rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y); //The Y is kept as it is so gravity can work.

        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer); //If the groundCheck area hits the groundLayer, isGrounded is true.
    }

    private void OnCollisionEnter2D(Collision2D collision) //Every time a collision happens.
    {
        if (collision.gameObject.CompareTag("Enemy")) //Checks if an enemy has been hit.
        {
            transform.position = spawnPoint; //Respawns.
        }
    }
}