using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.005f;
    public int maxHealth = 100;
    public int currentHealth = 1;

    public int coinCount = 0;
    public int coinsNeeded = 120;

    public GameManagerScript gameManager;
    public HealthBarScript healthBar;

    public ContactFilter2D movementFilter;
    public LightScript lightAttack;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;
    public bool isDead = false;
    public bool wonGame = false;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        coinCount = 0;

    }



    private void FixedUpdate() {

        if (canMove) {
            if (movementInput != Vector2.zero){

                bool success = TryMove(movementInput);

                if (!success) {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                if (!success) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("isMoving", success);
            } else {
                animator.SetBool("isMoving", false);
            }

            // Set the direction of sprite to movement direction
            if(movementInput.x < 0){
                spriteRenderer.flipX = true;
            } else if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
            }
        }
    }
    private bool TryMove(Vector2 direction){
            if (direction != Vector2.zero) {
                // Check for potential collisions
                int count = rb.Cast(
                    direction, // x and y values between -1 and 1 that represent the directions from the body to look for collisions
                    movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                    castCollisions, // List of collissions to store the found collsision into after the Cast is finished
                    moveSpeed * Time.fixedDeltaTime + collisionOffset // The amount to cast equal to the movement plus the offset
                    );
                if (count == 0){
                    rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
            
    }
    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }
    void OnAttack() {
        lightAttack.Attack();
    }
    public void Lockmovement() {
        canMove = false;
    }
    public void UnlockMovement() {
        canMove = true;
    }




    private void Update()
    {
        if (currentHealth <= 0)
        {
            die();
            return;
        }
        if (coinCount >= coinsNeeded)
        {
            win();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.setHealth(currentHealth);

        if (currentHealth <= 0)
        {
            die();
            return;
        }

    }

    public void die()
    {
        Debug.Log("Enemy Killed me");
        gameManager.gameOver();
        animator.SetBool("Dead", true);
        isDead = true;
        Lockmovement();
    }

    public void win()
    {
        Debug.Log("You won");
        gameManager.winGame();
        wonGame = true;
        Lockmovement();
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinCount++;

        }
    }



}
