using UnityEngine;
using System.Collections;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float delay; //fire activation delay
    [SerializeField] private float onTime;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool triggered; //player triggers the trap
    private bool on; //trap is active

    [Header("Sound")]
    [SerializeField] private AudioClip fireClip;

    private Health playerHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(playerHealth != null && on)
        {
            playerHealth.Damage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            if(!triggered)
            {
                //activate firetrap
                StartCoroutine(OnFireTrap());
            }
            if (on)
                collision.GetComponent<Health>().Damage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth = null;
        }
    }

    private IEnumerator OnFireTrap()
    {
        triggered = true;
        spriteRenderer.color = Color.red; //notify the player that the trap has been triggered 
        yield return new WaitForSeconds(delay); //add delay
        SoundSystem.instance.Play(fireClip);
        spriteRenderer.color = Color.white;
        on = true;
        animator.SetBool("on", true); //start animation
        yield return new WaitForSeconds(onTime);
        on = false;
        triggered = false;
        animator.SetBool("on", false);
    }
}
