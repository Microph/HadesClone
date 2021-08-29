using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
    [SerializeField]
    private Collider2D mainCollider;

    [SerializeField]
    private Collider2D playerVSEnemyCollider;

    public void OnEnterState(Enums.PlayerCharacterState playerCharacterState)
    {
        Reset();
        switch(playerCharacterState)
        {
            case Enums.PlayerCharacterState.Normal:
                break;
            case Enums.PlayerCharacterState.Dashing:
                //This will leave only mainCollider to block movement on collision, making player temporary pass through only enemies
                playerVSEnemyCollider.isTrigger = true;
                break;
        }
    }

    private void Reset()
    {
        playerVSEnemyCollider.isTrigger = false;
    }
}
