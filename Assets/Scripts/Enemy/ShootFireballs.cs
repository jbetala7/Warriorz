using UnityEngine;

public class ShootFireballs : MonoBehaviour
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

    [Header("Shoot")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Sound")]
    [SerializeField] private AudioClip fireballClip;

    //references
    private Animator animator;
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
        if (inSight())
        {
            if (timer >= cooldown)
            {
                //attack
                timer = 0;
                animator.SetTrigger("fireAttack");
            }
        }
        if (patrol != null)
            patrol.enabled = !inSight();
    }
    private void Shooting()
    {
        SoundSystem.instance.Play(fireballClip);

        timer = 0;
        fireballs[Fireball()].transform.position = firepoint.position;
        fireballs[Fireball()].GetComponent<Shoot>().StartShooting();
        //shoot
    }
    private int Fireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool inSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
