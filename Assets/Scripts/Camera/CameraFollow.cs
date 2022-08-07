using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Camera for Room 
    [SerializeField] private float speed;
    private float currentPositionX;
    private Vector3 velocity;

    //Camera for Player 
    [SerializeField] private Transform player;
    [SerializeField] private float distanceAhead;
    [SerializeField] private float cameraSpeed;
    private float Ahead;

    private void Update()
    {
        //Camera for Room 
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPositionX, transform.position.y, transform.position.z),
            ref velocity, speed);

        //Camera for Player 
        transform.position = new Vector3(player.position.x + Ahead, transform.position.y, transform.position.z);
        Ahead = Mathf.Lerp(Ahead, (distanceAhead * player.localScale.x), Time.deltaTime * cameraSpeed);

    }

    public void ChangeRoom(Transform newRoom)
    {
        currentPositionX = newRoom.position.x;
    }
}
