using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    public Transform attackPos;
    public float attackRange = .5f;
    public LayerMask enemyLayers;

    public int attackDamage = 60;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
            // Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            // for(int i = 0; i< enemiesToDamage.Length; i++)
           // {
                // enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
            //}
            //Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        // Play attack animation
        anim.SetTrigger("attack");

        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayers);
        
        // Damage Enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }    
        // Start attack CD
        cooldownTimer = 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPos == null)
            return;

        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
