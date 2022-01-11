using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerMode
{
    Civic,
    Casing,
    Robbing
};

public class PlayerMovement : MonoBehaviour
{
    public playerMode playerMode;

    //CivicSpeed/CasingModeSpeed = 235f;

    public float moveSpeed = 7.5f;
    public float runSpeed = 10f;

    public float movementSpeed;

    private Rigidbody2D rb;

    private PlayerControls playerControls;

    private Vector2 movement;
    private float ShiftInput;

    public Vector2 MousePosition;

    private Camera cam;

    private playerInteraction _playerInteraction;

    public bool isInteracting = false;

    Vector3 movementDirection;
    Vector2 mousePosWorld;
    Vector2 direction;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Enable();
        rb = GetComponent<Rigidbody2D>();
        _playerInteraction = GetComponent<playerInteraction>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void playerFaceMousePosition()
    {
        if (isInteracting == false)
        {
            mousePosWorld = cam.ScreenToWorldPoint(MousePosition);
            direction = new Vector2(mousePosWorld.x - transform.position.x, mousePosWorld.y - transform.position.y);
            transform.up = direction;
        }
    }

    private void playerInput()
    {
        MousePosition = playerControls.KeyboardInputs.MousePosition.ReadValue<Vector2>();
        playerFaceMousePosition();

        playerControls.KeyboardInputs.MovementHorizontal.performed += ctx => movement.x = ctx.ReadValue<float>();
        playerControls.KeyboardInputs.MovementHorizontal.canceled += ctx => movement.x = 0;

        playerControls.KeyboardInputs.MovementVertical.performed += ctx => movement.y = ctx.ReadValue<float>();
        playerControls.KeyboardInputs.MovementVertical.canceled += ctx => movement.y = 0;

        playerControls.KeyboardInputs.Shift.performed += ctx => ShiftInput = ctx.ReadValue<float>();
        playerControls.KeyboardInputs.Shift.canceled += ctx => ShiftInput = 0;
    }

    private void Update()
    {
        playerInput();

        if (ShiftInput == 1 && _playerInteraction._playerCarry == playerCarryingState.Nothing)
        {
            movementSpeed = runSpeed;
        }
        else
            movementSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        if(isInteracting == false)
        {
            movementDirection = new Vector3(movement.x, movement.y, 0);
            movementDirection.Normalize();

            rb.velocity = movementDirection * movementSpeed * Time.deltaTime;
        }

        if(isInteracting == true)
        {
            movementDirection = Vector3.zero;
            movementDirection.Normalize();

            rb.velocity = movementDirection * movementSpeed * Time.deltaTime;
        }
    }
}
