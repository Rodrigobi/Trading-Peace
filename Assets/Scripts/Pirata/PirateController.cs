using UnityEngine;

public class PirateController : MonoBehaviour
{
    public Transform lowerLeftLimit;
    public Transform upperRightLimit;
    public float wanderSpeed = 1.0f;
    public float chaseSpeed = 2.0f;
    public float visionRange = 5f;
    public float attackCooldown = 10f;
    public LayerMask playerLayer;
    public int waterDamage = 20;

    private Transform playerTransform;
    private Vector3 wanderTarget;
    private float attackTimer;
    private bool isFlipped;
    private bool isChasing;

    void Start()
    {
        SetNewWanderTarget();
        attackTimer = attackCooldown; // Start with the ability to attack
    }

    void Update()
    {
        // Decrement the attack timer
        if (attackTimer < attackCooldown)
        {
            attackTimer += Time.deltaTime;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Wander();
        }

        // Detect player
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, visionRange, playerLayer);
        if (playerCollider != null)
        {
            playerTransform = playerCollider.transform;
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
    }

    void Wander()
    {
        if (Vector2.Distance(transform.position, wanderTarget) > 0.1f)
        {
            MoveTowardsTarget(wanderTarget, wanderSpeed);
        }
        else
        {
            SetNewWanderTarget();
        }
    }

    void ChasePlayer()
    {
        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) > 0.1f)
        {
            MoveTowardsTarget(playerTransform.position, chaseSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto con el que colisionamos es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
        BarcoController barcoController = collision.gameObject.GetComponent<BarcoController>();
        if (barcoController != null)
        {
            // Llamamos al método para reducir el agua
            barcoController.ReducirAgua(20); // El valor '20' es la cantidad de agua que quieres reducir
        }
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (attackTimer >= attackCooldown)
            {
                AttackPlayer(collision.transform);
                attackTimer = 0f; // Reset attack timer
                SetNewWanderTarget(); // Return to wandering
                isChasing = false;
            }
        }
    }

    void AttackPlayer(Transform player)
    {
        // Reduce the player's water by waterDamage
        player.GetComponent<PlayerHealth>().ReduceWater(waterDamage);
    }

    void MoveTowardsTarget(Vector3 target, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        FlipSprite(target.x < transform.position.x);
    }

    void SetNewWanderTarget()
    {
        float newX = Random.Range(lowerLeftLimit.position.x, upperRightLimit.position.x);
        float newY = Random.Range(lowerLeftLimit.position.y, upperRightLimit.position.y);
        wanderTarget = new Vector3(newX, newY, transform.position.z);
    }

    void FlipSprite(bool flip)
    {
        if (flip != isFlipped)
        {
            isFlipped = flip;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the vision range in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }


    public class PlayerHealth : MonoBehaviour
    {
        public int waterAmount = 100;

        public void ReduceWater(int amount)
        {
            waterAmount -= amount;
            if (waterAmount <= 0)
            {
                // Handle player death here
            }
        }
    }
}
}
