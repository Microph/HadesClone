using UnityEngine;
using UnityEngine.UI;

public class DashSkillToggleButton : MonoBehaviour
{
    public PlayerColliderManager playerColliderManager;

    public PlayerVSEnemyCollider normalPlayerVSEnemyCollider;

    public PlayerVSEnemyCollider playerVSEnemyColliderWithBurn;

    public Text textUI;

    private bool isBurnOn = false;

    public void ToggleSkill()
    {
        if(isBurnOn)
        {
            playerColliderManager.playerVSEnemyCollider.gameObject.SetActive(false);
            playerColliderManager.playerVSEnemyCollider = normalPlayerVSEnemyCollider;
            playerColliderManager.playerVSEnemyCollider.gameObject.SetActive(true);
            textUI.text = "";
            isBurnOn = false;
        }
        else
        {
            playerColliderManager.playerVSEnemyCollider.gameObject.SetActive(false);
            playerColliderManager.playerVSEnemyCollider = playerVSEnemyColliderWithBurn;
            playerColliderManager.playerVSEnemyCollider.gameObject.SetActive(true);
            textUI.text = "BURN";
            isBurnOn = true;
        }
    }
}
