using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable, IBurnable
{
    [SerializeField]
    private int maxHp;

    [SerializeField]
    private TextUI hpUI;

    [SerializeField]
    private TextUI burnStatusUI;
    
    private int currentHp;

    [SerializeField]
    private int burnInterval = 1;
    private float currentBurnIntervalCounter = 0;
    private int currentBurnDuration = 0;
    private int currentDamagePerInterval = 0;

    public void TakeDamage(int damageAmount)
    {
        currentHp -= damageAmount;
        UpdateHpUI();
    }
    
    public void GetBurned(int burnDuration, int damagePerInterval)
    {
        currentDamagePerInterval = damagePerInterval;
        currentBurnDuration = burnDuration;
    }

    private void Start()
    {
        currentHp = maxHp;
        UpdateHpUI();
    }

    private void FixedUpdate()
    {
        if(currentBurnDuration <= 0)
        {
            currentBurnIntervalCounter = 0;
            currentBurnDuration = 0;
            return;
        }
        else if(currentBurnIntervalCounter >= burnInterval)
        {
            currentBurnIntervalCounter -= burnInterval;
            currentBurnDuration -= burnInterval;
            TakeDamage(currentDamagePerInterval);
        }
        
        currentBurnIntervalCounter += Time.fixedDeltaTime;
    }

    private void Update()
    {
        UpdateBurnStatusUI();
    }

    private void UpdateBurnStatusUI()
    {
        burnStatusUI?.UpdateText(currentBurnDuration > 0 ? $"BURNING! {currentBurnDuration.ToString()}" : "");
    }

    private void UpdateHpUI()
    {
        hpUI?.UpdateText($"{currentHp.ToString()}/{maxHp.ToString()}");
    }
}
