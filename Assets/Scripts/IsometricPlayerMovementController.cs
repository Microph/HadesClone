using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{
    public float maxMovementSpeed = 1f;

    [SerializeField]
    private PlayerCollider playerCollider;
    private InputManager inputManager;
    private IsometricCharacterRenderer isoRenderer;
    private Action currentState; //TODO: rename to something like currentMainFixedUpdateMethod

    private Vector2 currentFacingDirection;
    private float dashSpeedModifier = 4;
    private float dashDuration = 0.15f;
    private float elaspedDashingTime = 0f;

    Rigidbody2D rbody;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        currentState = NormalState;
        currentFacingDirection = Vector2.up;
    }

    void FixedUpdate()
    {
        currentState?.Invoke();
    }

    private void NormalState()
    {
        bool isDashButtonOnDownLastFrame = inputManager.HasDashButtonOnDown();
        if(isDashButtonOnDownLastFrame)
        {
            inputManager.ResetDashButtonState();
            elaspedDashingTime = 0;
            currentState = () => DashState(currentFacingDirection);
        }

        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * GetDirectionMovementSpeed(inputVector, maxMovementSpeed);
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        SetCurrentFacingDirection(movement);
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }

    private void DashState(Vector2 dir)
    {
        if (elaspedDashingTime >= dashDuration)
        {
            playerCollider.OnExitEnemyCollisionDashState();
            currentState = NormalState;
            return;
        }

        if(playerCollider.isCollidedWithEnemy)
        {
            playerCollider.OnEnterEnemyCollisionDashState();
        }

        Vector2 currentPos = rbody.position;
        Vector2 movement = dashSpeedModifier * dir * GetDirectionMovementSpeed(dir, maxMovementSpeed);
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
        elaspedDashingTime += Time.fixedDeltaTime;
    }

    private float GetDirectionMovementSpeed(Vector2 dir, float speed)
    {
        Vector2 normDir = dir.normalized;
        float angle = Vector2.Angle(Vector2.up, normDir) * Mathf.Deg2Rad;
        float finalSpeed = speed * (0.5f + (0.5f * Mathf.Sin(angle)) ); //TODO: replace 0.5 with current Y iso scales setting
        return finalSpeed;
    }
    
    private void SetCurrentFacingDirection(Vector2 movement)
    {
        if(movement.magnitude < 0.01f)
        {
            return;
        }

        currentFacingDirection = movement.normalized;
    }
}
