using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [Header("Enemy")]
    [SerializeField] private Transform trap;
    [SerializeField] private float speed;
    private Vector3 initialScale;
    private bool movingLeft;

    [Header("Idle")]
    [SerializeField] private float stayingIdle;
    private float timer;

    [Header("Patrol")]
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;

    private void Awake()
    {
        initialScale  = trap.localScale;
    }

    private void Update()
    {
        if(movingLeft)
        {
            if(trap.position.x >= left.position.x)
                Move(-1);
            else
            {
                ChangeDirection();
            }
        }
        else
        {
            if (trap.position.x <= right.position.x)
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

        trap.localScale = new Vector3(Mathf.Abs(initialScale.x) * _direction, initialScale.y, initialScale.z);

        trap.position = new Vector3(trap.position.x + Time.deltaTime * _direction * speed,
            trap.position.y, trap.position.z);
    }

    private void ChangeDirection()
    {
        timer += Time.deltaTime;

        if(timer > stayingIdle)
            movingLeft = !movingLeft;
    }
}
