using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField]
    private GameObject Menu;

    public void Play()
    {
        Time.timeScale = 1.0f;
        Menu.SetActive(false);
    }
}
