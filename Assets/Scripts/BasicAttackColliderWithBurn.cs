using UnityEngine;

public class BasicAttackColliderWithBurn : BasicAttackCollider
{
    protected override void OnTriggerStay2D(Collider2D col)
    {
        if(
            col.GetComponent<IDamageable>() is IDamageable iDamageable
            && !alreadyAttackedList.Contains(iDamageable)
            && col.gameObject.tag != "Player"
        )
        {
            iDamageable.TakeDamage(currentAttackPoint);
            alreadyAttackedList.Add(iDamageable);
            if(col.GetComponent<IBurnable>() is IBurnable iBurnable)
            {
                iBurnable.GetBurned(3, 1); //TODO: Get from config instead
            }
        }
    }
}
