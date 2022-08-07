using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform prevRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraFollow cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.ChangeRoom(nextRoom);
                nextRoom.GetComponent<Level>().Activate(true);
                prevRoom.GetComponent<Level>().Activate(false);
            }
            else
            {
                cam.ChangeRoom(prevRoom);
                prevRoom.GetComponent<Level>().Activate(true);
                nextRoom.GetComponent<Level>().Activate(false);
            }   
        }
    }
}
