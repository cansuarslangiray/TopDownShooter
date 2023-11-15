using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBasedProgressBar : MonoBehaviour
{
    public float timeBound = 30;

    public float passedTime = 0f;

    public Slider timeSlider;

    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (passedTime < timeBound)
        {
            passedTime += Time.deltaTime; 

            timeSlider.value = 1 - (passedTime / timeBound);
            timeText.text = ""+ Mathf.Ceil(timeBound - passedTime).ToString();
        }
        else
        {
            Debug.Log("Zaman Doldu!");
        }
    }
}
