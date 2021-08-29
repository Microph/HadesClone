using UnityEngine;

public class ProjectileWithBurn : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if(
            col.GetComponent<IDamageable>() is IDamageable iDamageable
            && col.gameObject.tag != "Player"
        )
        {
            iDamageable.TakeDamage(attackPoint);
            if(col.GetComponent<IBurnable>() is IBurnable iBurnable)
            {
                iBurnable.GetBurned(3, 1); //TODO: Get from config instead
            }
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
