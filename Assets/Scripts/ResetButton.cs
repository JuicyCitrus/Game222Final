using UnityEngine;
using System;

public class ResetButton : MonoBehaviour
{
    public static event Action ResetScene = delegate { };

    public void Restart()
    {
        ResetScene?.Invoke();
    }
}
