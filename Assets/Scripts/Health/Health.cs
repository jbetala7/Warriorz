using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float initialHealth;
    public float health { get; private set; }
    private Animator animator;
    private bool dead;
    private bool immune;

    public bool isPlayer = false;

    [Header("iFrames")]
    [SerializeField] private float immunePeriod;
    [SerializeField] private int playerBlinks;
    private SpriteRenderer spriteRenderer;

    [Header("Sound")]
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip hurtClip;

    [SerializeField] private Behaviour[] components;
    [SerializeField] private GameOverMenu gameOverMenu;

    private Respawn respawn;

    private void Awake()
    {
        health = initialHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawn = GetComponent<Respawn>();
    }

    public void Damage(float _damage)
    {
        if (immune) return;
        health = Mathf.Clamp(health - _damage, 0, initialHealth);

        if (health > 0)
        {
            //hurt
            animator.SetTrigger("hurt");

            //iframes
            StartCoroutine(Immune());

            SoundSystem.instance.Play(hurtClip);

        }
        else
        {
            Death();
        }
    }
    public void Death()
    {
        if (!dead)
        {
            //deactivate all the attached components
            foreach (Behaviour component in components)
                component.enabled = false;

            animator.SetBool("grounded", true);
            animator.SetTrigger("die");

            dead = true;
            SoundSystem.instance.Play(deathClip);


            if(isPlayer == true)
            {
                if (respawn.currentTimesRespawned >= respawn.timesToRespawn)
                    gameOverMenu.GameOver();

                respawn.currentTimesRespawned++;
            }
        }
    }

    
    public void Life(float _value)
    {
        health = Mathf.Clamp(health + _value, 0, initialHealth);
    }

    public void RespawnPlayer()
    {
        dead = false;
        Respawn.Instance.timesToRespawn--;

        Life(initialHealth);
        animator.ResetTrigger("die");
        animator.Play("idle");
        StartCoroutine(Immune());

        //activate all the attached components
        foreach (Behaviour component in components)
            component.enabled = true;
    }

    private IEnumerator Immune()
    {

        immune = true;
        Physics2D.IgnoreLayerCollision(7, 8, true);

        //immune period
        for (int i = 0; i < playerBlinks; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.7f);
            yield return new WaitForSeconds(immunePeriod / (playerBlinks * 4));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(immunePeriod / (playerBlinks * 4));
        }

        Physics2D.IgnoreLayerCollision(7, 8, false);
        immune = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
