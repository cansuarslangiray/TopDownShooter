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

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("Player");
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
        _enemyAnimator = GetComponent<Animator>();
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
        else if (health ==0)
        {
            DeadEnemyCounterUI.deadEnemyNum++;
            StartCoroutine(EnemyDeadAnim());
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    IEnumerator EnemyDeadAnim()
    {
        _speed = 0;
        transform.GetComponent<CapsuleCollider2D>().enabled = false;
        _enemyAnimator.speed = 0.5f;
        _enemyAnimator.SetBool("Dead", true);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}