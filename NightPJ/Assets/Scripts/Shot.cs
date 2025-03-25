using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameObject bullet; 
    public Transform spawnPoint;
    public float projectileSpeed = 25f; 
    public float projectileLifeTime = 2f; 
    public float shotRate = 0.5f; 
    public float rayRange = 100f; 
    public LayerMask hitLayers;
    public int damage = 101;

    private float shotRateTime = 0;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > shotRateTime)
            {
                Shoot();
                shotRateTime = Time.time + shotRate;
            }
        }
    }

    private void Shoot()
    {
        Ray ray = new Ray(spawnPoint.position, spawnPoint.forward);
        RaycastHit hit;
        Vector3 targetPoint = spawnPoint.position + spawnPoint.forward * rayRange; 

        if (Physics.Raycast(ray, out hit, rayRange, hitLayers))
        {
            targetPoint = hit.point; 

            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }
        SpawnBullet(targetPoint);
    }

    private void SpawnBullet(Vector3 targetPoint)
    {
        GameObject bulletInstance = Instantiate(bullet, spawnPoint.position, Quaternion.identity);

        Vector3 direction = (targetPoint - spawnPoint.position).normalized;

        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
        else
        {
            StartCoroutine(MoveBullet(bulletInstance, targetPoint));
        }

        Destroy(bulletInstance, projectileLifeTime);
    }

    private IEnumerator MoveBullet(GameObject bulletInstance, Vector3 targetPoint)
    {
        while (bulletInstance != null && Vector3.Distance(bulletInstance.transform.position, targetPoint) > 0.1f)
        {
            bulletInstance.transform.position = Vector3.MoveTowards(bulletInstance.transform.position, targetPoint, projectileSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(bulletInstance);
    }
}