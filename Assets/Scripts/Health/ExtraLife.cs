using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    [SerializeField] private float value;

    [Header("Sound")]
    [SerializeField] private AudioClip collectClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundSystem.instance.Play(collectClip);
            collision.GetComponent<Health>().Life(value);
            gameObject.SetActive(false);
        }
    }
}
