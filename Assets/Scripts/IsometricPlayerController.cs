using System;
using UnityEngine;

public class IsometricPlayerController : MonoBehaviour
{
    public Enums.PlayerCharacterState currentPlayerCharacterState;

    private ButtonInputManager buttonInputManager;
    private IsometricCharacterRenderer isoRenderer;
    private Action currentStateFixedUpdateAction;
    private Vector2 currentFacingDirection;
    private Rigidbody2D rbody;

    [SerializeField]
    private PlayerColliderManager playerColliderManager;
    [SerializeField]
    private float maxMovementSpeed;

    //Dashing
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashSpeedModifier;
    private float elaspedDashingTime = 0;

    //Basic Attacking
    [SerializeField]
    private int basicAttackPoint;
    [SerializeField]
    private float basicAttackDuration;
    [SerializeField]
    private float basicAttackCooldown;
    private float elaspedBasicAttackTime = 0;
    private float elaspedBasicAttackCoolDown = 0;

    //Projectile Attacking
    public GameObject projectilePrefab;
    [SerializeField]
    private Transform projectileAttackRangeCenterPivotTransform;
    [SerializeField]
    private int projectileAttackPoint;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private float projectileAttackDuration;
    [SerializeField]
    private float projectileAttackCooldown;
    private float elaspedProjectileAttackTime = 0;
    private float elaspedProjectileAttackCoolDown = 0;

    private void Awake()
    {
        buttonInputManager = GetComponent<ButtonInputManager>();
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        currentFacingDirection = Vector2.up;
    }

    private void Start()
    {
        ChangeState(
            playerCharacterState: Enums.PlayerCharacterState.Normal,
            fixedUpdateAction: NormalState
        );
        playerColliderManager.OnEnterNormalState();
    }

    void FixedUpdate()
    {
        currentStateFixedUpdateAction?.Invoke();
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
        currentPlayerCharacterState = playerCharacterState;
        currentStateFixedUpdateAction = fixedUpdateAction;
    }

    private void NormalState()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        if (DetermineDashingState(inputVector))
        {
            return;
        }
        if (DetermineBasicAttackState())
        {
            return;
        }
        if (DetermineProjectileAttackState())
        {
            return;
        }
        
        MoveCharacter(inputVector, 1);
    }

    private bool DetermineDashingState(Vector2 inputVector)
    {
        if (buttonInputManager.HasDashButtonOnDown())
        {
            buttonInputManager.ResetDashButtonState();
            elaspedDashingTime = 0;
            ChangeState(
                playerCharacterState: Enums.PlayerCharacterState.Dashing,
                fixedUpdateAction: () => DashingState(inputVector.magnitude >= 0.01f ? inputVector : currentFacingDirection)
            );
            playerColliderManager.OnEnterDashingState();
            return true;
        }

        return false;
    }

    private bool DetermineBasicAttackState()
    {
        if(
            buttonInputManager.HasBasicAttackButtonOnDown()
            && elaspedBasicAttackCoolDown >= basicAttackCooldown
        )
        {
            buttonInputManager.ResetBasicAttackButtonState();
            elaspedBasicAttackTime = 0;
            Vector2 faceToCursorDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            isoRenderer.SetDirection(faceToCursorDir.normalized, true);
            ChangeState(
                playerCharacterState: Enums.PlayerCharacterState.BasicAttacking,
                fixedUpdateAction: () => BasicAttackingState()
            );
            playerColliderManager.OnEnterBasicAttackingState(basicAttackPoint);
            return true;
        }
        else
        {
            elaspedBasicAttackCoolDown += Time.fixedDeltaTime;
        }

        return false;
    }

    private bool DetermineProjectileAttackState()
    {
        if(
            buttonInputManager.HasProjectileAttackButtonOnDown()
            && elaspedProjectileAttackCoolDown >= projectileAttackCooldown
        )
        {
            buttonInputManager.ResetProjectileAttackButtonState();
            elaspedProjectileAttackTime = 0;
            Vector2 faceToCursorDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            isoRenderer.SetDirection(faceToCursorDir.normalized, true);
            ShootProjectile(faceToCursorDir.normalized);
            ChangeState(
                playerCharacterState: Enums.PlayerCharacterState.ProjectileAttacking,
                fixedUpdateAction: () => ProjectileAttackingState()
            );
            return true;
        }
        else
        {
            elaspedProjectileAttackCoolDown += Time.fixedDeltaTime;
        }

        return false;
    }

    private void MoveCharacter(Vector2 dir, float speedModifier)
    {
        Vector2 currentPos = rbody.position;
        Vector2 movement = ScaleVectorToIsoView(dir) * maxMovementSpeed * speedModifier;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        SetCurrentFacingDirection(movement);
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }

    private Vector2 ScaleVectorToIsoView(Vector2 dir)
    {
        return Vector2.Scale(dir, new Vector2(1, 0.5f)); //TODO: Get value from iso config
    }

    private void DashingState(Vector2 dir)
    {
        if (elaspedDashingTime >= dashDuration)
        {
            ChangeState(
                playerCharacterState: Enums.PlayerCharacterState.Normal,
                fixedUpdateAction: NormalState
            );
            playerColliderManager.OnEnterNormalState();
            return;
        }

        if(DetermineBasicAttackState())
        {
            return;
        }
        if(DetermineProjectileAttackState())
        {
            return;
        }

        MoveCharacter(dir, dashSpeedModifier);
        elaspedDashingTime += Time.fixedDeltaTime;
    }

    private void BasicAttackingState()
    {
        if (elaspedBasicAttackTime >= basicAttackDuration)
        {
            ChangeState(
                playerCharacterState: Enums.PlayerCharacterState.Normal,
                fixedUpdateAction: NormalState
            );
            elaspedBasicAttackCoolDown = 0; 
            playerColliderManager.OnEnterNormalState();
            return;
        }
        
        elaspedBasicAttackTime += Time.fixedDeltaTime;
    }

    private void ShootProjectile(Vector2 faceToCursorDir)
    {
        GameObject obj = Instantiate(projectilePrefab, projectileAttackRangeCenterPivotTransform);
        Projectile projectile = obj.GetComponent<Projectile>();
        projectile.Setup(faceToCursorDir, projectileSpeed, projectileAttackPoint);
    }

    private void ProjectileAttackingState()
    {
        if (elaspedProjectileAttackTime >= projectileAttackDuration)
        {
            ChangeState(
                playerCharacterState: Enums.PlayerCharacterState.Normal,
                fixedUpdateAction: NormalState
            );
            elaspedProjectileAttackTime = 0; 
            return;
        }
        
        elaspedProjectileAttackTime += Time.fixedDeltaTime;
    }
}
