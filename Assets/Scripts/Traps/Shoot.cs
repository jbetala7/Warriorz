using UnityEngine;

public class Shoot : TrapDamage //damage player on every touch
{
    [SerializeField] private float speed;
    [SerializeField] private float reset; //deactive object after certain period of time 
    private float time; //lifetime
    private bool hit;

    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void StartShooting()
    {
        time = 0;
        hit = false;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }

    private void Update()
    {
        if (hit) return; 
        float shootingSpeed = speed * Time.deltaTime;
        transform.Translate(shootingSpeed, 0, 0);

        time += Time.deltaTime;
        if(time > reset)
            gameObject.SetActive(false);
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision); //execute code from 'EnemyDamage' script
        boxCollider.enabled = false;
        
        if (animator != null)
            animator.SetTrigger("explode"); //explod if the object is a fireball
        else
            gameObject.SetActive(false); //deativate when hit any other collider
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
