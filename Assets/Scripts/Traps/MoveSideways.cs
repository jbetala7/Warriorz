using UnityEngine;

public class MoveSideways : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    private bool goingLeft;
    private float left;
    private float right;

    private void Awake()
    {
        left = transform.position.x - distance;
        right = transform.position.x + distance;
    }

    private void Update()
    {
        if(goingLeft)
        {
            if(transform.position.x > left)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                goingLeft = false;
        }
        else
        {
            if (transform.position.x < right)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                goingLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().Damage(damage);
        }
    }
}
