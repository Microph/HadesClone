using UnityEngine;

public class BasicAttackCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<IDamageable>() is IDamageable iDamageable)
        {
            iDamageable.OnBeingDamaged(2); //TODO: remove this testing line
        }
    }
}
