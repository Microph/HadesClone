using UnityEngine;
using System.Collections.Generic;

public class BasicAttackCollider : MonoBehaviour
{
    private Collider2D basicAttackCollider;
    private int currentAttackPoint = 0;
    private List<IDamageable> alreadyAttackedList = new List<IDamageable>();

    private void Awake()
    {
        basicAttackCollider = GetComponent<Collider2D>();
    }

    public void Reset()
    {
        basicAttackCollider.enabled = false;
        currentAttackPoint = 0;
        alreadyAttackedList.Clear();
    }

    public void OnEnterBasicAttackingState(int attackPoint)
    {
        basicAttackCollider.enabled = true;
        currentAttackPoint = attackPoint;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log($"{col.gameObject.name}");
        if(col.GetComponent<IDamageable>() is IDamageable iDamageable
            && !alreadyAttackedList.Contains(iDamageable)
        )
        {
            iDamageable.OnBeingDamaged(currentAttackPoint);
            alreadyAttackedList.Add(iDamageable);
        }
    }
}
