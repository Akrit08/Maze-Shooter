using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    GameObject bossChild;
    public float  projectileSpeed = 100f;
    public float damageToPlayerOnCollision = 0.5f;
    public string enemyType;
    HealthBarController healthBar;
    public Animator enemyAnim;
    public float enemyHealth;
    public GameObject projectile;
    RaycastHit hit;
    public static bool isBossALive = true;
    //AI
    Transform player;
    public NavMeshAgent agent;
    public LayerMask isGround, isPlayer, isWall, isEnemy,isOuterWall;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public int walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool attacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private void Awake()
    {
        if (enemyType == "BossEnemy")
        {
            enemyHealth = 2000f;
        }
        else { enemyHealth = 100f; }
    }
    private void Start()
    {
        healthBar = FindObjectOfType<HealthBarController>();
        player = GameObject.Find("FPSPlayer").transform;//find player
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Physics.Linecast(transform.position - new Vector3(0, 10f, 0), player.transform.position, out hit);
        //check the sight range and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer) && hit.collider != null && hit.collider.tag == "Player";
        if (!playerInSightRange && !playerInAttackRange)
        {
            //Debug.Log("patrolling");
            Patrol();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            //Debug.Log("chasing");
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            //Debug.Log("attacking");
            Attack();
        }
    }
    void Patrol()
    {
        if (!walkPointSet) {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        float distanceToWalkPoint = Vector3.Distance(transform.position, walkPoint);
        if (Physics.CheckSphere(transform.position, 25f, isWall) || Physics.CheckSphere(transform.position, 25f, isOuterWall))
        {
            walkPointSet = false;
        }
        //Walkpoint reached
        if (distanceToWalkPoint <= 8f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {

        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 20f, isGround) && !Physics.Raycast(walkPoint, transform.forward, 10f, isWall) && !Physics.Raycast(walkPoint, transform.forward, 10f, isEnemy))
        {
            walkPointSet = true;
        }
    }
    void generateChild()
    {
        Debug.Log("ObjectPool");
        if (enemyType == "BossEnemy")
        {
            bossChild = ObjectPool.instance.GetPooledGameObject();
        }
        if (bossChild != null)
            {
                bossChild.transform.position = transform.position;
                bossChild.SetActive(true);
            }                  
    }
    public IEnumerator Spawner()
    {
        bool flag = true;

        while (flag)
        {
           
            yield return new WaitForSeconds(10f);
            generateChild();
        }
    }
    void Attack()
    {
        if (enemyType == "FlyingEnemy" || enemyType == "BossChild")
        {
            attackAsFlyingEnemy();
        }
        if (enemyType == "BossEnemy")
        {
            StartCoroutine(Spawner());
           // InvokeRepeating (nameof(generateChild), 20f, 30f);
            attackAsBossEnemy();
        }        
    }
    void attackAsFlyingEnemy()
    {
        transform.LookAt(player);
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //Walkpoint reached
        if (distanceToPlayer < 5f)
        {
            walkPointSet = false;
        }
        if (!attacked)
        {
            enemyAnim.SetBool("isAttacking", true);
            agent.SetDestination(player.position);
            attacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
    void attackAsBossEnemy()
    { 
        agent.SetDestination(transform.position);//dont move when attacking the player
        transform.LookAt(player);
        if (!attacked)
        {
            //attack
            Rigidbody rb = Instantiate(projectile,transform.position+new Vector3(0,10f,0),transform.rotation).GetComponent<Rigidbody>();
            rb.velocity= (player.transform.position + new Vector3(0, -5f, 0) - transform.position).normalized * projectileSpeed;
            Destroy(rb, 2f);
            enemyAnim.SetBool("isWalking", false);
            attacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void ResetAttack()
    {
        if (enemyType == "FlyingEnemy")
        {
            enemyAnim.SetBool("isAttacking", false);
        }
        if (enemyType == "BossEnemy")
        {
            enemyAnim.SetBool("isWalking", true);
        }
        attacked = false ;
    }
  
    public void TakeDamage(float amount)
    {
        Debug.Log("Enemy taking damage");
        enemyHealth =enemyHealth- amount;
        if (enemyHealth <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        if (enemyType == "BossEnemy")
        {
            isBossALive = false;
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (healthBar.takeDamage(damageToPlayerOnCollision) <= 0)
            {
                if (Player.tries > 1)
                {
                    player.gameObject.GetComponent<Player>().ResetPlayer();
                    player.gameObject.GetComponent<Player>().decreaseTries();
                }
                else
                {
                    Player.GameOverUI.GetComponent<GameOver>().ShowGameOver();
                }
            }
        }
        else
        {
            HealthBarController.isTakingDamage = false;
        }
    }
}
