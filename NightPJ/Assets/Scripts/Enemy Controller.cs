using System.Runtime.CompilerServices;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;


public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;

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


    private void Update() {
        playerInSightRadius = Physics.CheckSphere(transform.position, sightRadius, WhatIsPlayer);
        playerInAttackRad = Physics.CheckSphere(transform.position, attackRadius, WhatIsPlayer);
        
        if (!playerInSightRadius && !playerInAttackRad) Guarding();
        if (playerInSightRadius && !playerInAttackRad) HuntingPlayer();
        if (playerInSightRadius && playerInAttackRad) AttackingPlayer();
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
        float randomZ = Random.Range(-WalkPointRadius, WalkPointRadius);
        float randomX = Random.Range(-WalkPointRadius, WalkPointRadius);

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

        // if (!cooldownOn)
        // {
        //     cooldownOn = true;
        //     Invoke(nameof(ResetAttack), attackCooldown);
        // }
    }

    
}
