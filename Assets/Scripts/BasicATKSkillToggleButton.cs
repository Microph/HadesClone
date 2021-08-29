using UnityEngine;
using UnityEngine.UI;

public class BasicATKSkillToggleButton : MonoBehaviour
{
    public PlayerColliderManager playerColliderManager;

    public BasicAttackCollider normalBasicAttackCollider;

    public BasicAttackCollider basicAttackColliderWithBurn;

    public Text textUI;

    private bool isBurnOn = false;

    public void ToggleSkill()
    {
        if(isBurnOn)
        {
            playerColliderManager.basicAttackCollider.gameObject.SetActive(false);
            playerColliderManager.basicAttackCollider = normalBasicAttackCollider;
            textUI.text = "";
            isBurnOn = false;
        }
        else
        {
            playerColliderManager.basicAttackCollider.gameObject.SetActive(false);
            playerColliderManager.basicAttackCollider = basicAttackColliderWithBurn;
            textUI.text = "BURN";
            isBurnOn = true;
        }
    }
}
