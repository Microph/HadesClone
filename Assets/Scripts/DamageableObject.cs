using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int maxHp;

    [SerializeField]
    private TextUI hpUI;
    
    private int currentHp;

    public void OnBeingDamaged(int damageAmount)
    {
        currentHp -= damageAmount;
        UpdateHpUI();
    }

    private void Awake()
    {
        currentHp = maxHp;
        UpdateHpUI();
    }

    private void UpdateHpUI()
    {
        hpUI?.UpdateText($"{currentHp.ToString()}/{maxHp.ToString()}");
    }
}
