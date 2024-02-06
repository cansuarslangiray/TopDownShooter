using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBasedProgressBar : MonoBehaviour
{
    public GameObject levelUpTexture;
    public GameObject deadImg;
    public float timeBound = 30;
    public GameObject PlayAgain;

    public float passedTime = 0f;

    public Slider timeSlider;

    public Text timeText;

    public GameObject Enemy;

    private bool _creatingEnemyStart;
    private bool _gameOver = false;
    public GameObject CoinParent;
    public AudioSource loseAudioSource;
    public AudioSource backgroundAudioSource;



    // Update is called once per frame
    void Update()
    {
        if (!_gameOver)
        {
            CheckPlayerState();
        }
    }


    void CheckPlayerState()
    {
        if (Enemy.transform.childCount > 0)
        {
            _creatingEnemyStart = true;
        }

        if (passedTime < timeBound)
        {
            passedTime += Time.deltaTime;

            timeSlider.value = 1 - (passedTime / timeBound);
            timeText.text = "00:" + Mathf.Ceil(timeBound - passedTime).ToString();

            if (timeBound - passedTime == 0 && Enemy.transform.childCount != 0 || PlayerMovement.health <= 0)

            {
                backgroundAudioSource.Stop();
                _gameOver = true;
                // ShowGameOverUI();
                Debug.Log("Failed");
                deadImg.gameObject.SetActive(true);
                PlayAgain.gameObject.SetActive(true);
                loseAudioSource.Play();
            }


            else if (Enemy.transform.childCount == 0 && timeBound - passedTime >= 0)
            {
                if (_creatingEnemyStart)
                {
                    PlayAgain.gameObject.SetActive(false);
                    levelUpTexture.SetActive(true);
                    for (int i = 0; i < CoinParent.transform.childCount; i++)
                    {
                        PlayerMovement.coin += CoinParent.transform.GetChild(i).gameObject.GetComponent<Coin>().value;
                        Destroy(CoinParent.transform.GetChild(i).gameObject);
                    }

                    _creatingEnemyStart = false;
                }

                passedTime = 0;
                GenerateLevel.isCompleted = false;

                //  ShowSurvivedUI();
            }
        }
        else
        {
            backgroundAudioSource.Stop();
            deadImg.gameObject.SetActive(true);
            PlayAgain.gameObject.SetActive(true);
            loseAudioSource.Play();
        }
    }

    public  void IncreaseTime()
    {
        passedTime -= 1;
    }
}