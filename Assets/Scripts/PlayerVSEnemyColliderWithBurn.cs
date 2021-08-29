using UnityEngine;

public class PlayerVSEnemyColliderWithBurn : PlayerVSEnemyCollider
{
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if(
            col.GetComponent<IBurnable>() is IBurnable iBurnable
            && col.gameObject.tag != "Player"
        )
        {
            iBurnable.GetBurned(3, 1); //TODO: Get from config instead
        }
    }
}
