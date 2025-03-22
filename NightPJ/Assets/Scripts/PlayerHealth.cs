using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public Slider healthBar;
    public float maxHealth = 100f;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Invoke(nameof(ThanosPlayer), 0.1f);
        }
    }
    public void Heal(float amount)
{
    currentHealth += amount;
    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
    Debug.Log($"Player healed by {amount}. Current health: {currentHealth}");
}
    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;
    }

    private void ThanosPlayer()
    {
        Destroy(gameObject);
    }
}
