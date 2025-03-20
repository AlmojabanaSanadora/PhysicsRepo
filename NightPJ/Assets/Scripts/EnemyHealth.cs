using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    private Animator animator;
    private bool isDead = false; 

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

private void HandleDeath()
{
    if (isDead) return; 

    isDead = true; 
    animator.SetTrigger("isDead");

    GetComponent<NavMeshAgent>().enabled = false;
    GetComponent<EnemyController>().enabled = false;

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