using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVSEnemyCollider : MonoBehaviour
{
    [SerializeField]
    private Collider2D vsEnemyCollider;
    [SerializeField]
    private Collider2D triggerCollider;

    public void Reset()
    {
        vsEnemyCollider.enabled = true;
        triggerCollider.enabled = false;
    }

    public void OnEnterDashingState()
    {
        vsEnemyCollider.enabled = false;
        triggerCollider.enabled = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}
