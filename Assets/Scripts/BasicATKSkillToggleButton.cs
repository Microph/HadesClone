using UnityEngine;
using UnityEngine.UI;

public class BasicATKSkillToggleButton : MonoBehaviour
{
    public PlayerColliderManager playerColliderManager;

    public BasicAttackCollider normalBasicAttackCollider;

    public BasicAttackCollider basicAttackColliderWithBurn;

    public Text textUI;

    public void AddBurnSkill()
    {
        playerColliderManager.basicAttackCollider.gameObject.SetActive(false);
        playerColliderManager.basicAttackCollider = basicAttackColliderWithBurn;
        textUI.text = "BURN";
    }
}
