using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public float interactRange;

    [Header("Controls")]
    public KeyCode accelerateKey;
    public KeyCode turnLeftKey;
    public KeyCode turnRightKey;
    public KeyCode interactKey;
    public KeyCode inventory = KeyCode.I;
    [HideInInspector] public bool isLocked;

    private Rigidbody2D body;
    private float moveSpeed;
    private float turnSpeed;

    private bool IsInventoryOpen() => GameManager.inventoryPanel.activeInHierarchy;

    private List<Interactable> interactables = new();

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Controls();
        Interaction();
    }

    private void LateUpdate()
    {
        if (playerCamera != null)
            playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, playerCamera.transform.position.z);
    }

    private void FixedUpdate()
    {
        Movement();
        SortInteractables();
    }

    private void Controls()
    {
        moveSpeed = !isLocked && Input.GetKey(accelerateKey) ?
            Mathf.MoveTowards(moveSpeed, maxSpeed, moveAcceleration) :
            Mathf.MoveTowards(moveSpeed, 0, moveAcceleration);

        if (!isLocked && Input.GetKey(turnLeftKey))
            turnSpeed = Mathf.MoveTowards(turnSpeed, maxTurnSpeed, turnAcceleration);
        else if (!isLocked && Input.GetKey(turnRightKey))
            turnSpeed = Mathf.MoveTowards(turnSpeed, -maxTurnSpeed, turnAcceleration);
        else
            turnSpeed = Mathf.MoveTowards(turnSpeed, 0, turnAcceleration);

        if (Input.GetKeyDown(inventory))
            ToggleInventory();
    }

    private void Movement()
    {
        body.MovePosition(body.position + (Vector2)transform.up * (moveSpeed * Time.fixedDeltaTime));
        body.MoveRotation(body.rotation + turnSpeed * Time.fixedDeltaTime);
    }

    private void SortInteractables()
    {
        interactables = GameManager.interactables.ToList();
        interactables.Sort((a, b) =>
            Vector2.Distance(a.transform.position, transform.position)
                .CompareTo(Vector2.Distance(b.transform.position, transform.position)));

        foreach (var interactable in interactables)
            interactable.ShowText(interactable == interactables[0] &&
                                  Vector2.Distance(interactable.transform.position, transform.position) <= interactRange);
    }

    private void Interaction()
    {
        if (isLocked || !Input.GetKeyDown(interactKey) || interactables.Count == 0)
            return;

        if (Vector2.Distance(interactables[0].transform.position, transform.position) <= interactRange)
            interactables[0].Trigger();
    }

    private void ToggleInventory()
    {
        GameManager.inventoryPanel.SetActive(!IsInventoryOpen());
    }

    private void OpenInventory()
    {
        GameManager.inventoryPanel.SetActive(true);
    }

    private void CloseInventory()
    {
        GameManager.inventoryPanel.SetActive(false);
    }
}
