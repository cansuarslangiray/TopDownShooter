using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Vector3 position;
    public Camera camera;
    public Slider slide;
    private void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        slide = GetComponent<Slider>();
        slide.value = 10;
    }
    public void SetPositionFill(Vector3 pos)
    {
        position = pos;
    }

   

    public void SetDamage(float fill)
    {
        slide.value -= fill;
    }

    private void Update()
    {
        transform.position = camera.WorldToScreenPoint(position);
    }
}
