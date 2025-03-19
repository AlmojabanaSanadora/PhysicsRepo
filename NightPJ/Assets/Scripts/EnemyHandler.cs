using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;
    public LayerMask WhatIsGround, WhatIsPlayer;
    public GameObject bullet;

    public Vector3 walkArea;
    bool walkPointState;
    public float WalkPointRadius;

    public float attackCooldown;
    bool cooldownOn;

    public float sightRadius, attackRadius;
    bool playerInSightRadius, playerInAttackRad;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure your player GameObject is active and has the 'Player' tag.");
        }
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInSightRadius = Physics.CheckSphere(transform.position, sightRadius, WhatIsPlayer);

        if (playerInSightRadius)
        {
            playerInAttackRad = Physics.CheckSphere(transform.position, attackRadius, WhatIsPlayer);

            if (playerInAttackRad)
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
        {
            agent.SetDestination(walkArea);
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
        }

        Vector3 rangeToWalk = transform.position - walkArea;
        if (rangeToWalk.magnitude < 1f)
        {
            walkPointState = false;
            animator.SetBool("isWalking", false);
        }
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
        Vector3 targetPosition = player.position;

        targetPosition = AdjustForNearbyEnemies(targetPosition, 15f); 

        agent.SetDestination(targetPosition);

        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", true);
        animator.SetBool("isAttacking", false);
    }

    private void AttackingPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);

        if (!cooldownOn)
        {
            Rigidbody rb = Instantiate(bullet, transform.position + transform.forward, transform.rotation).GetComponent<Rigidbody>();
            rb.linearVelocity = transform.forward * 32f;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            Destroy(rb.gameObject, 2f);

            cooldownOn = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        cooldownOn = false;
    }

    private Vector3 AdjustForNearbyEnemies(Vector3 targetPosition, float minDistance)
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, minDistance, WhatIsPlayer);
        foreach (Collider collider in nearbyColliders)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("Enemy"))
            {
                Vector3 directionAway = (targetPosition - collider.transform.position).normalized;
                targetPosition += directionAway * minDistance;
            }
        }
        return targetPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 2f); 
    }
}