using System;
using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
    [SerializeField]
    private Collider2D mainCollider;

    [SerializeField]
    private Collider2D playerVSEnemyCollider;

    [SerializeField]
    private RotateZToLookAtCursor attackRangeRotator;

    [SerializeField]
    private BasicAttackCollider basicAttackCollider;

    private void Reset()
    {
        playerVSEnemyCollider.isTrigger = false;
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
        playerVSEnemyCollider.isTrigger = true;
    }

    public void OnEnterBasicAttackingState(int attackPoint)
    {
        Reset();
        attackRangeRotator.enabled = false;
        basicAttackCollider.OnEnterBasicAttackingState(attackPoint);
    }
}
