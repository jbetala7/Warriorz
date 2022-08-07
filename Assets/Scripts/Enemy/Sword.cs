using UnityEngine;

public class Sword : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float cooldown;
    [SerializeField] private float damage;
    [SerializeField] private float range;

    [Header("Collider")]
    [SerializeField] private float distance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float timer = Mathf.Infinity;

    [Header("Sound")]
    [SerializeField] private AudioClip swordClip;

    //references
    private Animator animator;
    private Health playerHealth;
    private Patrol patrol;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        patrol = GetComponentInParent<Patrol>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        //atack only if the player is in sight
        if(inSight())
        {
            if (timer >= cooldown && playerHealth.health > 0)
            {
                //attack
                timer = 0;
                animator.SetTrigger("attack");
                SoundSystem.instance.Play(swordClip);
            }
        }

        if (patrol != null)
            patrol.enabled = !inSight();
    }

    private bool inSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
            0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range* transform.localScale.x * distance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void HurtPlayer()
    {
        if(inSight())
        {
            //damage player only when in sight
            playerHealth.Damage(damage);
        }
    }
}
