using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    bool stop = true;
    float speed = 30f;
    float screenPercent = 0.95f;
    void Start()
    {
        Debug.Log(Screen.width);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(Input.mousePosition);
        if (Input.mousePosition.x >= Screen.width * screenPercent)
        {
            if (stop == false)
            {
                OpenMenu();
                
            }
            screenPercent = 0.7f;
        }
        else
        {
            if(stop == false)
            {
                CloseMenu();
            }
            screenPercent = 0.95f;

        }
        
    }
    void OpenMenu()
    {
        Debug.Log(menu.GetComponent<RectTransform>().offsetMax.x);
        if (menu.GetComponent<RectTransform>().offsetMax.x > 30)
        {
            menu.transform.Translate(-1 * Time.deltaTime * speed, 0, 0);
            
        }
        if (menu.GetComponent<RectTransform>().offsetMax.x < 20)
        {
            menu.transform.position.Set(20, 0, 0);
        }
    }

    public void CloseMenu()
    {
        Debug.Log("Menu closed");
        if (menu.GetComponent<RectTransform>().offsetMin.x < 1920f)
        {
            menu.transform.Translate(1 * Time.deltaTime * speed, 0, 0);

        }
        
    }
   public void onClick()
    {
        stop = !stop;

    }
}
