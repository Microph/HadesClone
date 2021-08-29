using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVSEnemyCollider : MonoBehaviour
{
    private Collider2D col2D;

    private void Awake()
    {
        col2D = GetComponent<Collider2D>();
    }

    public void Reset()
    {
        col2D.isTrigger = false;
    }

    public void OnEnterDashingState()
    {
        col2D.isTrigger = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}
