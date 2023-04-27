using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoblinEnemyGFXScript : MonoBehaviour
{

    Rigidbody2D rb;
    Animator animator;

    public GameObject Goblin;

    public EnemyAI goblinScript;

    public GoblinAttackScript attackScript;


    // Start is called before the first frame update
    void Start()
    {
        rb = Goblin.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        


    }
    public void Move()
    {
        goblinScript.canMove = true;
    }

    public void noMove()
    {
        goblinScript.canMove = false;
    }






}
