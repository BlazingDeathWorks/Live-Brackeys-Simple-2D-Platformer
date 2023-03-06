using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Components
    [SerializeField] private Rigidbody2D rb; //Physics of the object, like falling and moving.

    //Object attributes
    [SerializeField] private float moveSpeed; //How fast the object moves.
    [SerializeField] private float turnTimeMax; //Amount of time it takes for the object to turn around.

    //Unedited variables
    private float turnTime; //Amount of time left before the object turns around.

    void Awake() //Happens before everything else in a scene. Good for initializing variables.
    {
        turnTime = turnTimeMax; //Initialize timer.
        rb = GetComponent<Rigidbody2D>(); //If you do not want to assign the Rigidbody2D in the inspector.
    }

    void Update() //Every frame, is affected by frame rate. This can be fixed with Time.deltaTime in most cases.
    {
        turnTime -= Time.deltaTime; //Variable decreases by an amount independent of frame rate.

        if (turnTime <= 0) //If the timer reaches 0...
        {
            moveSpeed = -moveSpeed; //...the variable is set to the opposite (ex: 1 to -1, -1 to 1)...
            turnTime = turnTimeMax; //...and the timer resets.
        }
    }

    void FixedUpdate() //Every 0.02 seconds, or 50 times per second. Good for physics.
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y); //The object moves horizontally at a set speed and vertically based on gravity.
    }
}
