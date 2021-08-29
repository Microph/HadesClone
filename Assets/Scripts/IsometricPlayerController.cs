using System;
using UnityEngine;

public class IsometricPlayerController : MonoBehaviour
{
    public Enums.PlayerCharacterState currentPlayerCharacterState;

    public float maxMovementSpeed = 1f;

    [SerializeField]
    private PlayerColliderManager playerColliderManager;
    
    private InputManager inputManager;
    private IsometricCharacterRenderer isoRenderer;
    private Action currentStateFixedUpdateAction;

    private Vector2 currentFacingDirection;

    //Dashing
    private float dashSpeedModifier = 4;
    private float dashDuration = 0.15f;
    private float elaspedDashingTime = 0f;

    //Basic Attacking
    private float basicAttackDuration = 0.5f;
    private float elaspedBasicAttackTime = 0f;

    Rigidbody2D rbody;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        currentFacingDirection = Vector2.up;
        ChangeState(
            playerCharacterState: Enums.PlayerCharacterState.Normal,
            fixedUpdateAction: NormalState
        );
    }

    void FixedUpdate()
    {
        currentStateFixedUpdateAction?.Invoke();
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

    private void ChangeState(Enums.PlayerCharacterState playerCharacterState, Action fixedUpdateAction)
    {
        //TODO: wrap ColliderSetup, PlayerCharacterStateEnum, fixedUpdateMethod, etc. inside an object
        playerColliderManager.OnEnterState(playerCharacterState);
        currentPlayerCharacterState = playerCharacterState;
        currentStateFixedUpdateAction = fixedUpdateAction;
    }
    

    private void NormalState()
    {
        //check dash input
        if(inputManager.HasDashButtonOnDown())
        {
            inputManager.ResetDashButtonState();
            elaspedDashingTime = 0;
            ChangeState(
                playerCharacterState: Enums.PlayerCharacterState.Dashing,
                fixedUpdateAction: () => DashingState(currentFacingDirection)
            );
        }

        //Check basic attack input
        if(inputManager.HasBasicAttackButtonOnDown())
        {
            inputManager.ResetBasicAttackButtonState();
            elaspedBasicAttackTime = 0;
            ChangeState(
                playerCharacterState: Enums.PlayerCharacterState.BasicAttacking,
                fixedUpdateAction: () => BasicAttackingState(currentFacingDirection) //TODO: change currentFacingDirection to look at cursor direction
            );
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

    private void DashingState(Vector2 dir)
    {
        if (elaspedDashingTime >= dashDuration)
        {
            ChangeState(
                playerCharacterState: Enums.PlayerCharacterState.Normal,
                fixedUpdateAction: NormalState
            );
            return;
        }
        
        Vector2 currentPos = rbody.position;
        Vector2 movement = dashSpeedModifier * dir * GetDirectionMovementSpeed(dir, maxMovementSpeed);
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
        elaspedDashingTime += Time.fixedDeltaTime;
    }

    private void BasicAttackingState(Vector2 currentFacingDirection)
    {
        //TODO: enable basic attack collider
        throw new NotImplementedException();
    }
}
