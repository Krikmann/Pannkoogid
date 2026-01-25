using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    public GameObject sliderObject;
    Slider slider;
    public static bool onVolumeChange = false;
    // Start is called before the first frame update
    void Start()
    {
        slider = sliderObject.GetComponent<Slider>();
        slider.value = GlobalReferences.volume;
    }

    // Update is called once per frame
    void Update()
    {
        GlobalReferences.volume = slider.value;
    }
    public void OnChangeXD()
    {
        onVolumeChange = true;
    }
}
