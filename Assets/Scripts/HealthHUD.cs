using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;

    public void UpdateHealthBar(float ratio)
    {
        Debug.Log(ratio);
        healthBar.fillAmount = ratio;
    }
}
