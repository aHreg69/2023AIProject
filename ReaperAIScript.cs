using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ReaperAIScript : MonoBehaviour
{
    public Transform target;
    public ReaperAttackScript attackScript;

    public PlayerController playerScript;

    public float speed = 2f;
    [SerializeField]
    private float distanceToAttack;
    public float attackSpeed = 1f;
    public float attackDelay = 1f;

    public float nextWaypoint = 3f;
    bool reachedEndOfPath = false;


    Path path;
    int currentWaypoint = 0;

    Seeker seeker;
    Rigidbody2D rb;
    Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0f, .1f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }

    }

    private void FollowPath()
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

        if (force != Vector2.zero)
        {

        }
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
        if ((playerScript.isDead == false) && (playerScript.wonGame == false))
        {
            FollowPath();
        }
        

        if (rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (TargetInAttackDistance() && (playerScript.wonGame == false) && Time.time > attackDelay)
        {
            attackScript.Attack();
            attackDelay = Time.time + attackSpeed;
        }

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
}
