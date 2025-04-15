using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Patrolowanie")]
    public Transform[] patrolPoints; // Punkty, mi�dzy kt�rymi przeciwnik patroluje
    private int currentPatrolIndex = 0; // Aktualny punkt patrolowy
    public float patrolSpeed = 2f; // Pr�dko�� patrolowania

    [Header("Atakowanie Gracza")]
    public float detectionRange = 5f; // Zasi�g wykrywania gracza
    public float attackRange = 1f; // Zasi�g ataku
    public int damage = 3; // Ilo�� obra�e� zadawanych graczowi
    public float attackCooldown = 2f; // Cooldown ataku
    private float nextAttackTime = 0f; // Czas nast�pnego ataku

    [Header("Gracz")]
    private Transform player; // Referencja do gracza
    private PlayerHealth playerHealth; // Komponent zdrowia gracza

    private void Start()
    {
        // Znajd� gracza na podstawie tagu
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogError("Nie znaleziono gracza z tagiem 'Player'.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange) // Gracz w zasi�gu
        {
            FollowPlayer();
        }
        else
        {
            Patrol();
        }

        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            AttackPlayer();
        }
    }

    private void Patrol()
    {
        // Ruch do aktualnego punktu patrolowego
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);

        // Sprawdzenie, czy przeciwnik dotar� do punktu patrolowego
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Przej�cie do nast�pnego punktu
        }
    }

    private void FollowPlayer()
    {
        // Pod��anie za graczem
        transform.position = Vector3.MoveTowards(transform.position, player.position, patrolSpeed * Time.deltaTime);
    }

    private void AttackPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Przeciwnik zada� graczowi " + damage + " obra�e�!");
            nextAttackTime = Time.time + attackCooldown; // Ustaw cooldown
        }
    }
}
