using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
    [SerializeField]
    private Collider2D mainCollider;

    [SerializeField]
    private Collider2D dashingCollider;

    public void OnEnterState(Enums.PlayerCharacterState playerCharacterState)
    {
        Reset();
        switch(playerCharacterState)
        {
            case Enums.PlayerCharacterState.Normal:
                break;
            case Enums.PlayerCharacterState.Dashing:
                dashingCollider.isTrigger = true;
                break;
        }
    }

    private void Reset()
    {
        dashingCollider.isTrigger = false;
    }
}
