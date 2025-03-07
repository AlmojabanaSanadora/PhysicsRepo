using System.Runtime.CompilerServices;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;


public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;
    public GameObject bullet;
    public float health;

    // Guard State

    public Vector3 walkArea;
    bool walkPointState;
    public float WalkPointRadius;

    // Attack State

    public float attackCooldown;
    bool cooldownOn;

    // States

    public float sightRadius, attackRadius;
    bool playerInSightRadius, playerInAttackRad;

    private void Awake ()
    {
        player = GameObject.Find("PlayerObject").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    private void Update() 
    {
        playerInSightRadius = Physics.CheckSphere(transform.position, sightRadius, WhatIsPlayer);

        if(playerInSightRadius)
        {
            playerInAttackRad = Physics.CheckSphere(transform.position, attackRadius, WhatIsPlayer);

            if(playerInAttackRad)
            {
                AttackingPlayer();
            }
            else
            {
                HuntingPlayer();
            }
        }
        else
        {
            Guarding();
        }
    }
    
        private void Guarding()
    {
        if (!walkPointState) SearchGuardArea();
        if (walkPointState)
        agent.SetDestination(walkArea);

        Vector3 rangeToWalk = transform.position - walkArea;
        if (rangeToWalk.magnitude < 1f)
        walkPointState = false;
    }
    private void SearchGuardArea()
    {
        float randomZ = UnityEngine.Random.Range(-WalkPointRadius, WalkPointRadius);
        float randomX = UnityEngine.Random.Range(-WalkPointRadius, WalkPointRadius);

        walkArea = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        if (Physics.Raycast(walkArea, -transform.up, 2f, WhatIsGround))
            walkPointState = true;
    }

    private void HuntingPlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackingPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!cooldownOn)
        {
            Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            Destroy(rb.gameObject, 2f);


            cooldownOn = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        cooldownOn = false;

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(ThanosEnemy), 0.5f);

    }

    private void ThanosEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
    
}
