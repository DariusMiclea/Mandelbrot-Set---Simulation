using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Explorer : MonoBehaviour
{
    public Material mat;
    public Vector2 pos;
    public float scale, angle;

    private Vector2 smoothPos;
    private float smoothScale, smoothAngle;
    
    private void UpdateShader()
    {
        smoothPos = Vector2.Lerp(smoothPos, pos, 0.03f);
        smoothScale = Mathf.Lerp(smoothScale, scale, 0.03f);
        smoothAngle = Mathf.Lerp(smoothAngle, angle, 0.03f);

        float aspect = (float)Screen.width / (float)Screen.height;
        float scaleX = smoothScale;
        float scaleY = smoothScale;

        if (aspect > 1f)
        {
            scaleY /= aspect;
        }
        else
        {
            scaleX *= aspect;
        }
        mat.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, smoothScale, smoothScale));
        mat.SetFloat("_Angle", smoothAngle);
    }
    private void HandleInputs()
    {
       
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            scale *= 0.99f;
        }
        if(Input.GetKey(KeyCode.Mouse1))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            scale *= 1.01f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            angle -= 0.01f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            angle += 0.01f;
        }

        Vector2 dir = new Vector2(scale / 100, 0);
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);
        dir = new Vector2(dir.x * cos - dir.y * sin, dir.x * sin + dir.y * cos);

        if (Input.GetKey(KeyCode.A))
        {
            pos -= dir;
        }

        if (Input.GetKey(KeyCode.D))
        {
            pos += dir;
        }
        dir = new Vector2(-dir.y, dir.x);
        if (Input.GetKey(KeyCode.W))
        {
            pos += dir;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos -= dir;
        }

    }
    void FixedUpdate()
    {
        UpdateShader();
        HandleInputs();
    }
}