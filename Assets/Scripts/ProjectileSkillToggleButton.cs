using UnityEngine;
using UnityEngine.UI;

public class ProjectileSkillToggleButton : MonoBehaviour
{
    public IsometricPlayerController isometricPlayerController;

    public GameObject projectilePrefab;

    public GameObject projectilePrefabWithBurn;

    public Text textUI;

    private bool isBurnOn = false;

    public void ToggleSkill()
    {
        if(isBurnOn)
        {
            isometricPlayerController.projectilePrefab = projectilePrefab;
            textUI.text = "";
            isBurnOn = false;
        }
        else
        {
            isometricPlayerController.projectilePrefab = projectilePrefabWithBurn;
            textUI.text = "BURN";
            isBurnOn = true;
        }
    }
}
