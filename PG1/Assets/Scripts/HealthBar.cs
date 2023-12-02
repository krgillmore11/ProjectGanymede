using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] Slider slider;

    public void SetMax(int health){
        slider.maxValue = health;
        slider.value = health;
    }
    // Start is called before the first frame update
    public void SetHealth(int health){
        slider.value = health;
    }
}
