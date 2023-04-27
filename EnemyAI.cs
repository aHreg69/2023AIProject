using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform target;
    public GoblinAttackScript attackScript;
    public Animator animator;

    public PlayerController playerScript;

    // AI Pathfinding Variables
    bool reachedEndOfPath = false;
    Path path;
    int currentWaypoint = 0;
    Seeker seeker;

    // Goblin Stats
    public float speed = 2f;
    public float nextWaypoint = 3f;
    public int maxHealth = 100;
    public int currentHealth;
    bool goblinIsDead = false;
    public bool canMove = true;


    // Goblin Awareness and Attack Delay
    public bool isAwareOfPlayer = false;
    [SerializeField]
    private float playerAwarenessDistance;
    [SerializeField]
    private float distanceToAttack;
    public float attackSpeed = 1f;
    public float attackDelay = 1f;

    




    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        InvokeRepeating("UpdatePath", 0f, .1f);
    }

    void UpdatePath()
    {
        if (TargetInDistance() && seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }

    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);


        if (distance < nextWaypoint)
        {
            currentWaypoint++;
        }
    }




    // Update is called once per frame
    void FixedUpdate()
    {

        if (TargetInDistance() && (playerScript.isDead == false) && (playerScript.wonGame == false) && (goblinIsDead == false) && (canMove == true))
        {
            PathFollow();
        }

        if (TargetInAttackDistance()&& (playerScript.wonGame == false) && Time.time > attackDelay)
        {
            attackScript.Attack();
            attackDelay = Time.time + attackSpeed;
        }


        if (rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

    }

    private void Update()
    {
        
    }

    private bool TargetInDistance()
    {
        return (Vector2.Distance(transform.position, target.transform.position) < playerAwarenessDistance);
    }

    public bool TargetInAttackDistance()
    {
        if (playerScript.isDead == false)
        {
            return (Vector2.Distance(transform.position, target.transform.position) < distanceToAttack);
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            die();
        }

    }

    

    public void die()
    {
        goblinIsDead = true;

        animator.SetBool("dead", true);

        Debug.Log("Goblin died");
        //gameObject.SetActive(false);
    }

}
