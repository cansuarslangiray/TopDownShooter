using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour
{
    public GameObject homepage;
 

    public void ActivateHomePage()
    {
        Debug.Log("workingggg");
        homepage.gameObject.SetActive(true); 
        PauseGame();
    }

    public void DisactivateHomePage()
    {
        homepage.gameObject.SetActive(false);
        ResumeGame();


    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
