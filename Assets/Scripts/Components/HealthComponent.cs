/* Created by Wilson World Games, August 2022 */
/* The Health Component class provides objects it's attached to with a way of affecting and managing health variables and if an object is dead/destroyed */

using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Player UI")]
    public HealthDisplay HealthDisplayUI;

    [Header("Health Settings")]
    public float MaxHealth = 100.0f;
    public float MaxArmor = 50.0f;
    [SerializeField] float CurrentHealth = 0.0f;
    [SerializeField] float CurrentArmor = 0.0f;

    bool isDead = false;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float dmgAmount)
    {
        // Check if the player has armor
        if (CurrentArmor > 0.0f) {
            // Split damage between armor and health values
            float armorDmg = dmgAmount * 0.5f;
            float healthDmg = dmgAmount * 0.5f;

            // If the damage done to the armor exceeds what remains, carry that damage over to health reduction
            if (CurrentArmor - armorDmg < 0.0f )
            {
                float carryDmg = armorDmg - CurrentArmor;
                healthDmg += carryDmg;
            }

            // Reduce armor and then health by caluclated values
            ReduceArmor(armorDmg);
            ReduceHealth(healthDmg);
        }
        else {
            ReduceHealth(dmgAmount);
        }
    }

    // Adds to the current health count by the amount specified or sets it back to the maximum amount if the amount would bring the current total higher than the max total.
    public void RestoreHealth(float amount)
    {
        if (isDead == true)
            return;

        CurrentHealth += amount;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        UpdateHealthUI();
    }

    // Removes from the current health count by the amount specified or sets it to 0 if the amount would bring the current total lower than 0.
    public void ReduceHealth(float amount)
    {
        if (isDead == true)
            return;

        CurrentHealth -= amount;

        if (CurrentHealth <= 0.0f) {
            CurrentHealth = 0.0f;
            isDead = true;
        }

        UpdateHealthUI();
    }

    public void RestoreArmor(float amount)
    {
        if (isDead == true)
            return;

        CurrentArmor += amount;

        if (CurrentArmor > MaxArmor)
            CurrentArmor = MaxArmor;

        UpdateArmorUI();
    }

    public void ReduceArmor(float amount)
    {
        if (isDead == true)
            return;

        CurrentArmor -= amount;

        if (CurrentArmor <= 0.0f)
            CurrentArmor = 0.0f;

        UpdateArmorUI();
    }
    
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    private void UpdateHealthUI()
    {
        if (this.gameObject.tag == "Player")
            HealthDisplayUI.UpdateHealthDisplay(CurrentHealth);
    }

    private void UpdateArmorUI()
    {
        if (this.gameObject.tag == "Player")
            HealthDisplayUI.UpdateArmorDisplay(CurrentArmor);
    }
}
