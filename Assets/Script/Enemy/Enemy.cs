using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject targetPlayer;
    private float _speed = 3.0f;
    private Vector2 _position;
    private SpriteRenderer _enemySpriteRenderer;
    private Animator _enemyAnimator;
    public int damage = 1;
    public int health;
    public GameObject[] coinPrfab;
    private GameObject coin;
    private TimeBasedProgressBar _timeBar;
    public GameObject popUp;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("Player");
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
        _enemyAnimator = GetComponent<Animator>();
        coin = GameObject.Find("Coins");
        _timeBar = GameObject.Find("GameManager").GetComponent<TimeBasedProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = _speed * Time.deltaTime;
        Turn();
        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, targetPlayer.transform.position, step);
    }

    void Turn()
    {
        if (targetPlayer.transform.position.x < transform.position.x)
        {
            _enemySpriteRenderer.flipX = true;
        }
        else
        {
            _enemySpriteRenderer.flipX = false;
        }
    }

    public void SetHealth(int damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
        else if (health < 0)
        {
            health = 0;
        }
        else if (health == 0)
        {
            DeadEnemyCounterUI.deadEnemyNum++;
            StartCoroutine(EnemyDeadAnim());
            int number = Random.Range(0, 3);
            if (number ==1)
            {
                _timeBar.IncreaseTime();
               var popup= Instantiate(popUp, transform.position, Quaternion.identity);
               Destroy(popup,1);
            }
        }
    }
    

    public int GetDamage()
    {
        return damage;
    }

    private void CreateCoin()
    {
        int range = Random.Range(0, 4);
        for (int i = 0; i < range; i++)
        {
            var posy = transform.position.x;
            var pos = new Vector2(posy + i / 2, transform.position.y);
            var createdCoin = Instantiate(coinPrfab[Random.Range(0, coinPrfab.Length)], pos, Quaternion.identity);
            createdCoin.transform.SetParent(coin.transform);
        }
    }

    IEnumerator EnemyDeadAnim()
    {
        _speed = 0;
        transform.GetComponent<CapsuleCollider2D>().enabled = false;
        _enemyAnimator.speed = 0.5f;
        _enemyAnimator.SetBool("Dead", true);
        yield return new WaitForSeconds(1);
        CreateCoin();
        Destroy(gameObject);
    }
}