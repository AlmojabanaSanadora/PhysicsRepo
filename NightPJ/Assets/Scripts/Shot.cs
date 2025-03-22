using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public LayerMask hitLayers;
    public AudioSource gunAudio;
    public Transform spawnPoint; 
    public float shotRate = 0.5f; 
    public float rayRange = 100f; 
    public float damage = 101f;

    private float shotRateTime = 0;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > shotRateTime)
            {
                ShootRay(); 
                shotRateTime = Time.time + shotRate;
            }
        }
    }

    private void ShootRay()
    {
        Ray ray = new Ray(spawnPoint.position, spawnPoint.forward); 
        RaycastHit hit;

        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * rayRange, Color.red, 1f); 

        if (Physics.Raycast(ray, out hit, rayRange, hitLayers))
{
    Debug.Log($"Ray hit: {hit.collider.name}");

    if (gunAudio != null)
        {
            gunAudio.pitch = Random.Range(0.95f, 1.05f);
            gunAudio.Play();
        }

    if (hit.collider.CompareTag("Enemy"))
    {
        EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }

    StartCoroutine(ShowRayEffect(hit.point));
}
    }

    private IEnumerator ShowRayEffect(Vector3 hitPoint)
{
    LineRenderer line = GetComponent<LineRenderer>();
    line.SetPosition(0, spawnPoint.position); 
    line.SetPosition(1, hitPoint); 

    line.enabled = true;
    yield return new WaitForSeconds(0.1f); 
    line.enabled = false;
}
}