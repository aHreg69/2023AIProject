using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightScript : MonoBehaviour
{

    public Animator animator;

    public PlayerController player;

    //public PlayerInput input;

    public Transform attackPoint;
    public float attackRange = .5f;
    public LayerMask enemyLayers;

    public int attackDamage = 20;

    void Update()
    {
        
    }

    public void Attack()
    {
        animator.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage);
            Debug.Log("Enemy Hit");
        }


    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        else
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

    }

}
