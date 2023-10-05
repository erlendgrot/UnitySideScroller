
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float range;
    
    [SerializeField] private int damage;
    [SerializeField] private AudioClip meleeAttackSound;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header ("Player Parameters")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    
    //References
    private Animator anim;
    private Health playerHealth;

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Only attack when player is in sight
        if (PlayerInSight())
        {
            //Attack the player
            if (cooldownTimer >= attackCoolDown && playerHealth.currentHealth > 0)
            {
                
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack"); // Setting the trigger for the attack-animation
                SoundManager.instance.PlaySound(meleeAttackSound);

            }
        }

        // If you see the player, stop patroling
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();

    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    //Animation reference (Function is executed in meleeAttack animation)
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            // Damage player health
            playerHealth.TakeDamage(damage);
        }
    }
}
