using UnityEngine;
using UnityEngine.AI;


public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(ThanosEnemy), 0.1f);

    }

    private void ThanosEnemy()
    {
        animator.SetTrigger("isDead");

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyController>().enabled = false;

        Destroy(gameObject, 3f);
    }
}
