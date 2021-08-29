using UnityEngine;
using System.Collections.Generic;

public class BasicAttackCollider : MonoBehaviour
{
    private int currentAttackPoint = 0;
    private List<IDamageable> alreadyAttackedList = new List<IDamageable>();

    public void Reset()
    {
        gameObject.SetActive(false);
        currentAttackPoint = 0;
        alreadyAttackedList.Clear();
    }

    public void OnEnterBasicAttackingState(int attackPoint)
    {
        gameObject.SetActive(true);
        currentAttackPoint = attackPoint;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.GetComponent<IDamageable>() is IDamageable iDamageable
            && !alreadyAttackedList.Contains(iDamageable)
        )
        {
            iDamageable.OnBeingDamaged(currentAttackPoint);
            alreadyAttackedList.Add(iDamageable);
        }
    }
}
