using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           // Destroy(collision.gameObject);
        }

        else if(collision.CompareTag("Wall"))
        {
            //Destroy(this.gameObject);
        }
    }
}
