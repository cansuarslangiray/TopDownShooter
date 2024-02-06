using System;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerMovement : MonoBehaviour
{
    public static int coin;
    private float _moveH;
    private float _moveV;
    public float speed = 5;
    public GameObject bulletPrefabs;
    private bool _amIShooting;
    private SpriteRenderer _playerSpriteRenderer;
    public float boundary = 18;
    private bool _runX = true;
    private bool _runY = true;
    private Animator _playerAnim;
    public GameObject gunPrep;
    public static int health = 10;
    public int maxHealth = 10;
    public GameObject healthBar;
    public int damage = 1;
    public AudioSource gunAudioSource;
    public AudioSource healAudioSource;


    private void Start()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _playerAnim = GetComponent<Animator>();
        _playerAnim.SetBool("isRunning", false);
    }

    private void Update()
    {
        var pos = new Vector3(transform.position.x, transform.position.y - 0.7f);
        healthBar.GetComponent<HealthBar>().SetPositionFill(pos);
        Movement();
        Shoot();
        CheckHealth();
    }

    private void Movement()
    {
      
        _moveH = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        _moveV = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A) && !_amIShooting)
        {
            _playerSpriteRenderer.flipX = true;
            _playerAnim.SetBool("isRunning", true);
            gunPrep.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetKey(KeyCode.D) && !_amIShooting)
        {
            _playerSpriteRenderer.flipX = false;
            _playerAnim.SetBool("isRunning", true);
            gunPrep.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            _playerAnim.SetBool("isRunning", false);
        }

        float newXPos = transform.position.x + _moveH;
        newXPos = Mathf.Clamp(newXPos, -boundary, boundary);
        float newYPos = transform.position.y + _moveV;
        newYPos = Mathf.Clamp(newYPos, -boundary, boundary);
        transform.position = new Vector2(newXPos, newYPos);
    }


    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (transform.position.x < clickedPosition.x)
            {
                _playerSpriteRenderer.flipX = false;
            }
            else
            {
                _playerSpriteRenderer.flipX = true;
            }

            clickedPosition.z = 0f; // Ensure the z-coordinate is appropriate for 2D gameplay
            gunAudioSource.Play();
            var bullet = Instantiate(bulletPrefabs, gunPrep.transform.position, Quaternion.identity);
            Vector2 direction = (clickedPosition - gunPrep.transform.position).normalized;

            bullet.GetComponent<Bullet>().SetDirection(direction);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gunPrep.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //  bullet.transform.rotation=Quaternion.AngleAxis(angle, Vector3.forward);
            _amIShooting = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            if (healthBar != null)
            {
                var slider = healthBar.GetComponent<HealthBar>().slide;
                var enemyDamage = other.transform.GetComponent<Enemy>().GetDamage();
                healthBar.GetComponent<HealthBar>().SetDamage(enemyDamage);
                health -= enemyDamage;
            }
        }

        /*if (other.transform.CompareTag("Coin"))
        {
            coin += other.gameObject.GetComponent<Coin>().value;
            Destroy(other.gameObject);
        }*/
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        var slider = healthBar.GetComponent<HealthBar>().slide;
        healthBar.GetComponent<HealthBar>().SetHealth(amount);
        healAudioSource.Play();
    }

    public void IncreaseDamage(int amount)
    {
        damage += amount;
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }

    /* public void TakeDamage(int amount)
     {
         health = Mathf.Max(health - amount, 0f);
         
     }*/


    void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(healthBar.gameObject);
            _playerAnim.SetBool("IsDead", true);
            gunPrep.SetActive(false);
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            speed = 0;
            Debug.Log("Dead");
        }
    }
}