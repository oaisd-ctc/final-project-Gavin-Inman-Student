using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bars : MonoBehaviour
{

    public Slider slider;

    public void SetAmmount(float ammount)
    {
        slider.value = ammount;
    }

    public void SetMax(float max, float ammount)
    {
        slider.maxValue = max;
        slider.value = ammount;
    }

}
