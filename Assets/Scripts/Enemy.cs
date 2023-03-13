using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed; 
    [SerializeField] private float turnRate; 

    private float turnTime;
    private Rigidbody2D rb;

    void Awake()
    {
        turnTime = turnRate; 
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        turnTime -= Time.deltaTime;

        if (turnTime <= 0) 
        {
            moveSpeed = -moveSpeed; 
            turnTime = turnRate;
        }
    }

    void FixedUpdate() 
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
}
