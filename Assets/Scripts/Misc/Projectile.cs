using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public GameObject[] powerupArray; 
    // Start is called before the first frame update
    void Start()
    {
        powerupArray = GameObject.FindGameObjectsWithTag("PowerUp");

        if (lifetime <= 0)
            lifetime = 2.0f;

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);

        foreach (GameObject powerup in powerupArray)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), powerup.GetComponent<Collider2D>());
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "PowerUp")
        {
            Destroy(this.gameObject);
        }
    }
}