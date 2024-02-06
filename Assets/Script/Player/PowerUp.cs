using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        Health,
        Damage,
        Speed // İsterseniz başka tipler de ekleyebilirsiniz.
    }

    public PowerUpType powerUpType;
    public int amount = 1; // Power-up'ın miktarı

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUp(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void ApplyPowerUp(GameObject player)
    {
        PlayerMovement playerController = player.GetComponent<PlayerMovement>();

        switch (powerUpType)
        {
            case PowerUpType.Health:
                playerController.Heal(amount);
                break;
            case PowerUpType.Damage:
                playerController.IncreaseDamage(amount);
                break;
            case PowerUpType.Speed:
                playerController.IncreaseSpeed(amount);
                break;
            // Diğer power-up tiplerini buraya ekleyebilirsiniz.
        }
    }
}

