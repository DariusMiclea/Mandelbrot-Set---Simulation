using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;
    private InputField text;
    void Start()
    {
        slider = gameObject.GetComponent<UnityEngine.UI.Slider>();
        text = gameObject.GetComponent<UnityEngine.UI.InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateValueFromFloat(float value)
    {
        Debug.Log("float value changed: " + value);
        if (slider) { slider.value = value; }
        if (text) { text.text = value.ToString(); }
    }

    public void UpdateValueFromString(string value)
    {
        Debug.Log("string value changed: " + value);
        if (slider) { slider.value = float.Parse(value); }
        if (text) 
        {
            text.text = value; 
        }
    }
}
