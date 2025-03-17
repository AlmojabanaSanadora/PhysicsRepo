using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        print($"Projectile hit: {other.name}");  

        if (gameObject.CompareTag("PlayerProjectile"))
        {
            if (other.CompareTag("Enemy"))
            {
                print("Hit an Enemy! Attempting to deal damage...");  
                other.GetComponent<EnemyHealth>()?.TakeDamage(damage);

                print("Damage function called!");  
                Destroy(gameObject);  
            }
        }
        else if (gameObject.CompareTag("EnemyProjectile"))
        {
            if (other.CompareTag("Player"))
            {
                print("Hit the Player! Attempting to deal damage...");
                other.GetComponent<PlayerHealth>()?.TakeDamage(damage);

                print("Damage function called!");
                Destroy(gameObject);
            }
        }
    }
}
