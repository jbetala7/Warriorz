using UnityEngine;
public class Respawn : MonoBehaviour
{
    public static Respawn Instance { get; set; }
    [SerializeField] private AudioClip checkpointClip;
    [SerializeField] private Transform currentCheckpoint;
    private Health playerHealth;
    public int timesToRespawn, currentTimesRespawned;

    private void Awake()
    {
        Instance = this;
        playerHealth = GetComponent<Health>();
    }

    public void PlayerRespawn()
    {
        if (timesToRespawn > 0)
        {
            playerHealth.RespawnPlayer();//reset player health and animation
        }
        else
        {
            playerHealth.Death();
        }

        if (currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.position; //reset player position to checkpoint
        }
        //Camera.main.GetComponent<CameraFollow>().ChangeRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundSystem.instance.Play(checkpointClip);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
