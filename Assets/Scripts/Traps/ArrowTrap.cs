using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float timer;

    [Header("Sound")]
    [SerializeField] private AudioClip arrowClip;

    private void Shoot()
    {
        timer = 0;

        SoundSystem.instance.Play(arrowClip);
        arrows[Arrow()].transform.position = firePoint.position;
        arrows[Arrow()].GetComponent<Shoot>().StartShooting();
    }

    private int Arrow()
    {
        for(int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= cooldown)
            Shoot();
    }
}
