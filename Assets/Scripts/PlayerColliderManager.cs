using System;
using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
    public PlayerVSEnemyCollider playerVSEnemyCollider;
    
    public BasicAttackCollider basicAttackCollider;

    [SerializeField]
    private Collider2D mainCollider;

    [SerializeField]
    private RotateZToLookAtCursor attackRangeRotator;

    private void Reset()
    {
        playerVSEnemyCollider.Reset();
        basicAttackCollider.Reset();
        attackRangeRotator.enabled = true;
    }

    public void OnEnterNormalState()
    {
        Reset();
    }
    
    public void OnEnterDashingState()
    {
        Reset();
        //This will leave only mainCollider to block movement on collision, making player temporary pass through only enemies
        playerVSEnemyCollider.OnEnterDashingState();
    }

    public void OnEnterBasicAttackingState(int attackPoint)
    {
        Reset();
        attackRangeRotator.enabled = false;
        basicAttackCollider.OnEnterBasicAttackingState(attackPoint);
    }
}
