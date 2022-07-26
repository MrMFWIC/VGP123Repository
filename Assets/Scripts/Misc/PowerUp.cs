using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    /*public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
           
        }
        else
        {
             Destroy(this.gameObject);
        }
    }*/
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(1, 0);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer != 3 && collision.gameObject.tag != "Projectile")
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(-1, 0);
            }
            else
            {
                rb.velocity = new Vector2(1, 0);
            } 
        }
    }
}
