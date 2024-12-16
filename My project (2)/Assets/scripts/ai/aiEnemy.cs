using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Patrolowanie")]
    public Transform[] patrolPoints; // Punkty, miêdzy którymi przeciwnik patroluje
    private int currentPatrolIndex = 0; // Aktualny punkt patrolowy
    public float patrolSpeed = 2f; // Prêdkoœæ patrolowania

    [Header("Atakowanie Gracza")]
    public float detectionRange = 5f; // Zasiêg wykrywania gracza
    public float attackRange = 1f; // Zasiêg ataku
    public int damage = 3; // Iloœæ obra¿eñ zadawanych graczowi
    public float attackCooldown = 2f; // Cooldown ataku
    private float nextAttackTime = 0f; // Czas nastêpnego ataku

    [Header("Gracz")]
    private Transform player; // Referencja do gracza
    private PlayerHealth playerHealth; // Komponent zdrowia gracza

    private void Start()
    {
        // ZnajdŸ gracza na podstawie tagu
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

        if (distanceToPlayer <= detectionRange) // Gracz w zasiêgu
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

        // Sprawdzenie, czy przeciwnik dotar³ do punktu patrolowego
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Przejœcie do nastêpnego punktu
        }
    }

    private void FollowPlayer()
    {
        // Pod¹¿anie za graczem
        transform.position = Vector3.MoveTowards(transform.position, player.position, patrolSpeed * Time.deltaTime);
    }

    private void AttackPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Przeciwnik zada³ graczowi " + damage + " obra¿eñ!");
            nextAttackTime = Time.time + attackCooldown; // Ustaw cooldown
        }
    }
}
