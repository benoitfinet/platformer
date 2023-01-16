using UnityEngine;
using UnityEngine.UI;


public class Expbar : MonoBehaviour
{
    public Slider slider;

    public void SetMinExp(int exp)
    {
        slider.minValue = exp;
        slider.value = exp;
    }

    public void SetExp(int exp)
    {
        slider.value = exp;
    }
}
