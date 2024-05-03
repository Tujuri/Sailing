using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [Header("Components")] 
    public Camera playerCamera;
    public GameObject starterShip;
    public GameObject upgradedShip;
    
    [Header("Parameters")]
    public float maxSpeed;
    public float maxTurnSpeed;
    public float moveAcceleration;
    public float turnAcceleration;
    
    [Header("Controls")]
    public KeyCode accelerateKey;
    public KeyCode turnLeftKey;
    public KeyCode turnRightKey;
    
    private Rigidbody2D body;
    private float moveSpeed;
    private float turnSpeed;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Controls();
    }

    private void LateUpdate()
    {
        if(playerCamera != null)
            playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, playerCamera.transform.position.z);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Controls()
    {
        moveSpeed = Input.GetKey(accelerateKey) ? 
            Mathf.MoveTowards(moveSpeed, maxSpeed, moveAcceleration) : 
            Mathf.MoveTowards(moveSpeed, 0, moveAcceleration);
        
        if(Input.GetKey(turnLeftKey))
            turnSpeed = Mathf.MoveTowards(turnSpeed, maxTurnSpeed, turnAcceleration);
        else if(Input.GetKey(turnRightKey))
            turnSpeed = Mathf.MoveTowards(turnSpeed, -maxTurnSpeed, turnAcceleration);
        else
            turnSpeed = Mathf.MoveTowards(turnSpeed, 0, turnAcceleration);
    }

    private void Movement()
    {
        body.MovePosition(body.position + (Vector2)transform.up * (moveSpeed * Time.fixedDeltaTime));
        body.MoveRotation(body.rotation + turnSpeed * Time.fixedDeltaTime);
    }
}
