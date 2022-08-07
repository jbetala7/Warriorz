using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("In Air")]
    [SerializeField] private float duration; //amount of time player can stay in the air before making the next jump
    private float timer; //time passed since the player moved from the edge off a game object

    [Header("Multitple Jumps")]
    [SerializeField] private int jumps;
    private int jumpCounter;

    [Header("Wall Jumps")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D player;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float horizontal;

    [Header("Sound")]
    [SerializeField] private AudioClip jumpClip;


    private void Awake()
    {
        //get refrences for rigidbody and animator from the game object
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        //make the player flip according to the movement
        if (horizontal > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontal < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //animtor parameter
        animator.SetBool("run", horizontal != 0);
        animator.SetBool("grounded", isGrounded());

        //jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //jump height
        if(Input.GetKeyUp(KeyCode.Space) && player.velocity.y > 0)
            player.velocity = new Vector2(player.velocity.x, player.velocity.y / 2);

        if(onWall())
        {
            player.gravityScale = 0;
            player.velocity = Vector2.zero;
        }
        else
        {
            player.gravityScale = 7;
            player.velocity = new Vector2(horizontal * speed, player.velocity.y);

            if (isGrounded())
            {
                timer = duration; //reset when player lands on ground
                jumpCounter = jumps;
            }
            else
                timer -= Time.deltaTime; //reduce timer when player is not on the ground
        }

    }

    private void Jump()
    {
        if (timer < 0 && !onWall() && jumpCounter <=0 ) return; 
        //if counter is 0 and if no jumps are left then do nothing

        SoundSystem.instance.Play(jumpClip);

        if (onWall())
            WallJump();
        else
        {
            if (isGrounded())
                player.velocity = new Vector2(player.velocity.x, jumpPower);
            else
            {
                //perform a normal jump if player is not on the ground and timer is bigger than 0
                if (timer > 0)
                    player.velocity = new Vector2(player.velocity.x, jumpPower);
                else
                {
                    if(jumpCounter > 0) //jump if more jumps are left and then reduce the counter
                    {
                        player.velocity = new Vector2(player.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            timer = 0;
        }
    }

    private void WallJump()
    {
        player.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool attackAllowed()
    {
        return horizontal == 0 && isGrounded() && !onWall();
    }
}