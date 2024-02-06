using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    // Start is called before the first frame update
  
    

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            Debug.Log("workinggg coin");
            PlayerMovement.coin+=value;
            Destroy(gameObject);
        }
    }
}
