using System;
using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
    [SerializeField]
    private Collider2D mainCollider;

    [SerializeField]
    private Collider2D playerVSEnemyCollider;

    [SerializeField]
    private Collider2D basicAttackCollider;

    private void InitialSetup()
    {
        playerVSEnemyCollider.isTrigger = false;
        basicAttackCollider.enabled = false;
    }

    public void OnEnterNormalState()
    {
        InitialSetup();
    }
    
    public void OnEnterDashingState()
    {
        InitialSetup();
        //This will leave only mainCollider to block movement on collision, making player temporary pass through only enemies
        playerVSEnemyCollider.isTrigger = true;
    }

    public void OnEnterBasicAttackingState()
    {
        InitialSetup();
        basicAttackCollider.enabled = true;
    }
}
