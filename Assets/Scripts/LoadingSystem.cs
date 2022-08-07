using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSystem : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            SceneManager.LoadScene(1);
    }
}
