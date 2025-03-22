using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    private Animator animator;
    private bool isDead = false; 
    private float healing = 10f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return; 

        health -= damage;
        if (health <= 0)
        {
            HandleDeath();
        }
    }

    public void Heal()
    {
    PlayerHealth playerHealth = FindFirstObjectByType<PlayerHealth>();
    if (playerHealth != null)
    {
        playerHealth.Heal(healing); 
        Debug.Log($"Player healed by {healing} points from enemy death.");
    }
    }

private void HandleDeath()
{
    if (isDead) return; 

    isDead = true; 
    animator.SetTrigger("isDead");

    GetComponent<NavMeshAgent>().enabled = false;
    GetComponent<EnemyHandler>().enabled = false;

    Heal();

    Destroy(gameObject, 3f);
}

    private void OnDestroy()
{

    if (GameHandler.instance != null)
    {
        GameHandler.instance.DecrementEnemyCount();
    }

    if (EnemySpawner.instance != null)
    {
        EnemySpawner.instance.DecrementTotalEnemies();
    }
}
}