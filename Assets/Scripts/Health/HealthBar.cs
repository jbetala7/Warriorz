using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image full;
    [SerializeField] private Image current;

    private void Start()
    {
        full.fillAmount = playerHealth.health / 10;
    }

    private void Update()
    {
        current.fillAmount = playerHealth.health / 10;
    }
}
