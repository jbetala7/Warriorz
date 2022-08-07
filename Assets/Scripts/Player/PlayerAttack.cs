using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireballs;
    [SerializeField] private AudioClip fireballClip;
    [SerializeField] private Text fireballCountText;
    private Animator animator;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    public int initialFireballs = 3;
    public int fireballValue = 2;

    public bool canAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        canAttack = true;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.attackAllowed() && canAttack)
            Attack();

        cooldownTimer += Time.deltaTime;
    }
    private void Attack()
    {
        if (initialFireballs < 1) return;


        initialFireballs--;
        fireballCountText.text = "" + initialFireballs;

        SoundSystem.instance.Play(fireballClip);

        animator.SetTrigger("attack");
        cooldownTimer = 0;

        var fireball = new GameObject();
        fireball = Instantiate(fireballs, firePoint.position, Quaternion.identity);

        fireball.GetComponent<Fireball>().SetDirection(Mathf.Sign(transform.localScale.x));

    }
    public void StartAttackDelay()
    {
        StartCoroutine(attackDelay());
    }
    private IEnumerator attackDelay()
    {
        while (true)
        {
            canAttack = false;
            yield return new WaitForSeconds(0.5f);
            canAttack = true;
            yield break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fireball")
        {
            initialFireballs += fireballValue;
            fireballCountText.text = "" + initialFireballs;
            other.gameObject.SetActive(false);
        }
    }
}
