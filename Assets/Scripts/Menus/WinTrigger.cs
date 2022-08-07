using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private WinMenu gameWinMenu;
    [SerializeField] private AudioClip checkpointClip;

    // Start is called before the first frame update
    void Start()
    {
        gameWinMenu = GameObject.Find("WinManager").GetComponent<WinMenu>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            SoundSystem.instance.Play(checkpointClip);
            gameWinMenu.Win();
        }
    }
}
