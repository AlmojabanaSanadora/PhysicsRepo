using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 🔴 Importar SceneManager para cambiar de escena

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
            Invoke(nameof(LoadGameOverScene), 0.1f); // 🔴 Cargar la escena número 2
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

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene(2); // 🔴 Cargar la escena número 2
    }
}
