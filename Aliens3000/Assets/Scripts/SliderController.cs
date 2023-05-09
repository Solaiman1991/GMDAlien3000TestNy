using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.minValue = 0;
    }
}