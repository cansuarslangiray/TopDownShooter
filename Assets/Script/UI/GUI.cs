using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public GameObject homepage;
    public GameObject coinNum;
    public GameObject speed;
    public GameObject damage;
    public GameObject maxhp;
    public GameObject player;
    public GameObject playAgain;

    private void Update()
    {
        coinNum.GetComponent<Text>().text = "Coin:     " + PlayerMovement.coin;
        speed.GetComponent<Text>().text = "Speed:   " + player.GetComponent<PlayerMovement>().speed;
        damage.GetComponent<Text>().text = "Damage:  " + player.GetComponent<PlayerMovement>().damage;
        maxhp.GetComponent<Text>().text = "Max HP:     " + player.GetComponent<PlayerMovement>().maxHealth;
    }

    public void LoadAgain()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        PlayerMovement.health = 10;
    }

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
        playAgain.gameObject.SetActive(false);

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}