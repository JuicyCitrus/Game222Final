using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResetButton : MonoBehaviour
{
    public static Action ResetScene = delegate { };

    public void Restart()
    {
        ResetScene();
    }
}
