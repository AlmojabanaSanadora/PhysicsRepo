using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public Slider healthBar;
    public GameObject gameOverCanvas; // 🔴 Referencia al Canvas de Game Over
    public float maxHealth = 100f;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        
        // Asegurar que el GameOverCanvas esté desactivado al inicio
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            ShowGameOver(); // 🔴 Muestra el Canvas en vez de destruir al jugador
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

    private void ShowGameOver()
    {
        // 🔴 Activa el Canvas de Game Over
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        // 🔴 Opcional: Desactivar al jugador para evitar que siga moviéndose
        gameObject.SetActive(false);
    }
}
