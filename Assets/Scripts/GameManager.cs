using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Material mat;
    public GameObject panel;
    public Slider color, speed, speed2, repeat;
    public Dropdown texture;
    public Toggle symmetry;
    public Image image;
    public float gradientSpeed, rotationSpeed, repeatN, gradientColor;
    
    
    void Update()
    {
        mat = image.GetComponent<Explorer>().mat = image.material;
        UpdateSettings();
    }
    void UpdateSettings()
    {
        
        if (color)
        {
            gradientColor = color.value;
        }
        if (speed)
        {
            gradientSpeed = speed.value;
        }
        if (speed2)
        {
            rotationSpeed = speed2.value;
        }
        if (repeat)
        {
            repeatN = repeat.value;
        }
        

        if (symmetry.isOn)
        {
            mat.SetFloat("_Symmetry", 1);
        }
        else
        {
            mat.SetFloat("_Symmetry", 0);
        }

        mat.SetFloat("_Color", gradientColor);
        mat.SetFloat("_Speed", gradientSpeed);
        mat.SetFloat("_Speed2", rotationSpeed);
        mat.SetFloat("_Repeat", repeatN);
    }
    public void TextureUpdate(int value)
    {
        image.material = image.GetComponent<MeshRenderer>().materials[value];
        image.GetComponent<Explorer>().mat = image.GetComponent<MeshRenderer>().materials[value];
    }
    public void Reinit()
    {
        color.value = 0.5f;
        speed.value = 0.1f;
        speed2.value = 1f; 
        repeat.value = 2f;
        symmetry.isOn = false;
        texture.value = 0;

        mat.SetFloat("_Color", color.value);
        mat.SetFloat("_Speed", speed.value);
        mat.SetFloat("_Speed2", speed2.value);
        mat.SetFloat("_Repeat", repeat.value);
        mat.SetFloat("_Symmetry", 0);
        TextureUpdate(0);



    }
}
