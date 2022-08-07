using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private float lifetime;

    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float powerSpeed = speed * Time.deltaTime * direction;
        transform.Translate(powerSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 3) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("explode");

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().Damage(1);
    }

    public void SetDirection(float fdirection)
    {
        lifetime = 0;
        direction = fdirection;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
        float localScaleX = transform.localScale.x;

        if (Mathf.Sign(localScaleX) != fdirection)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
