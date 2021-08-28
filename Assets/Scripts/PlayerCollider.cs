using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public bool isCollidedWithEnemy;

    public Collider2D enemyCollider;

    public void OnEnterEnemyCollision()
    {
        if(enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }
    }

    public void OnExitEnemyCollision()
    {
        if(enemyCollider != null)
        {
            enemyCollider.enabled = true;
        }
        enemyCollider = null;
        isCollidedWithEnemy = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.gameObject.CompareTag("Enemy"))
        {
            enemyCollider = col.collider;
            isCollidedWithEnemy = true;
        }
    }
}
