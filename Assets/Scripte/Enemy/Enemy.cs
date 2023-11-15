using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject targetPlayer;
    private float _speed = 3.0f;
    private Vector2 _position;

    public int health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = _speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, targetPlayer.transform.position, step);
    }

    public void SetHealth(int damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
        else if(health<=0)
        {
            Destroy(gameObject);
            DeadEnemyCounterUI.deadEnemyNum++;

        }
    }
}
