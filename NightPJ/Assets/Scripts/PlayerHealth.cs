using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(ThanosPlayer), 0.1f);

    }

    private void ThanosPlayer()
    {
        Destroy(gameObject);
    }
}
