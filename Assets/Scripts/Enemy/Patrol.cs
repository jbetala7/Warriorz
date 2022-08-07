using UnityEngine;

public class Patrol : MonoBehaviour
{

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    private Vector3 initialScale;
    private bool movingLeft;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Idle")]
    [SerializeField] private float stayingIdle;
    private float timer;

    [Header("Patrol")]
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;

    private void Awake()
    {
        initialScale  = enemy.localScale;
    }

    private void OnDisable()
    {
        animator.SetBool("run", false);
    }

    private void Update()
    {
        if(movingLeft)
        {
            if(enemy.position.x >= left.position.x)
                Move(-1);
            else
            {
                ChangeDirection();
            }
        }
        else
        {
            if (enemy.position.x <= right.position.x)
                Move(1);
            else
            {
                ChangeDirection();
            }
        }   
    }
    private void Move(int _direction)
    {
        timer = 0;
        animator.SetBool("run", true);

        //make enemy face the direction
        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * _direction, initialScale.y, initialScale.z);

        //make enemy move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }

    private void ChangeDirection()
    {
        animator.SetBool("run", false);
        timer += Time.deltaTime;

        if(timer > stayingIdle)
            movingLeft = !movingLeft;
    }
}
