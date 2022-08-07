using UnityEngine;

public class FireballHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void Update()
    {
        transform.localScale = enemy.localScale;
    }
}
