using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(ThanosEnemy), 0.1f);

    }

    private void ThanosEnemy()
    {
        Destroy(gameObject);
    }
}
