using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject[] traps;
    private Vector3[] startPosition;

    private void Awake()
    {
        //save the start position of all the traps
        startPosition = new Vector3[traps.Length];
        for(int i = 0; i < traps.Length; i++)
        {
            if(traps[i] != null)
                startPosition[i] = traps[i].transform.position;
        }
    }

    public void Activate(bool _currentStatus)
    {
        //activate or deactivate traps
        for (int i = 0; i < traps.Length; i++)
        {
            if (traps[i] != null)
                traps[i].SetActive(_currentStatus);
                traps[i].transform.position = startPosition[i];
        }
    }
}
