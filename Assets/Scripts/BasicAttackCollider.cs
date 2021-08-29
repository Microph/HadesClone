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

    protected virtual void OnTriggerStay2D(Collider2D col)
    {
        if(
            col.GetComponent<IDamageable>() is IDamageable iDamageable
            && !alreadyAttackedList.Contains(iDamageable)
            && col.gameObject.tag != "Player"
        )
        {
            iDamageable.TakeDamage(currentAttackPoint);
            alreadyAttackedList.Add(iDamageable);
        }
    }
}
