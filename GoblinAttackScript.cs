using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttackScript : MonoBehaviour
{

    
    public Animator animator;
    public Transform attackPoint;

    public float attackRange = .5f;
    public int attackDamage = 20;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("Goblin hit me");
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        } else
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        
    }


}
