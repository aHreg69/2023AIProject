using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    Animator animator;
    Vector2 movement;
    Rigidbody2D rb;
    bool moving = false;



    void Start()
    {
        animator = animator.GetComponent<Animator>();
        rb = rb.GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (aiPath.desiredVelocity.x >= 0.01f){
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else if (aiPath.desiredVelocity.x <= -0.01f){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (rb.velocity != Vector2.zero)
        {
            moving = true;
        } else
        {
            moving = false;
        }

        if (moving = true)
        {
            animator.SetBool("isMoving", true);
        } else
        {
            animator.SetBool("isMoving", false);
        }

    }


}
