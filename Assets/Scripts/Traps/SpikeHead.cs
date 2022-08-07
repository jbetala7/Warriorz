using UnityEngine;

public class SpikeHead : TrapDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float delay;
    [SerializeField] private LayerMask playerLayer;
    private float time;
    private Vector3 target; //player's position once detected by the 'Spike Head'
    private bool inAction;
    private Vector3[] directions = new Vector3[4];

    [Header("Sound")]
    [SerializeField] private AudioClip impactClip;

    private void OnEnable()
    {
        Stop();    
    }

    private void Update()
    {
        //move spike to the target only if it is in action/attacking
        if(inAction)
            transform.Translate(target *Time.deltaTime * speed);
        else
        {
            time += Time.deltaTime;
            if(time > delay)
                LookforPlayer();
        }
    }

    private void LookforPlayer()
    {
        Directions();

        //to see if the 'Spike Head' can see the player or not
        for(int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if(hit.collider != null && !inAction)
            {
                inAction = true;
                target = directions[i];
                time = 0;
            }
        }
    }
    private void Directions()
    {
        directions[0] = transform.right * range; //direction for right
        directions[1] = -transform.right * range; //direction for left
        directions[2] = transform.up * range; //direction for up
        directions[3] = -transform.up * range; //direction for down
    }

    private void Stop()
    {
        target = transform.position; //'Spike Head' stops moving after the detination is set as the current position
        inAction = false;
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        SoundSystem.instance.Play(impactClip);
        base.OnTriggerEnter2D(collision);
        Stop();//stop attacking after one hit
    }
}
