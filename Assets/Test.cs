using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void onCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Entered " + collision.gameObject.name);
    }

    private void onCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Collision Exited " + collision.gameObject.name);
    }

    private void onCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collision Staying " + collision.gameObject.name);
    }

    private void onTriggerEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Entered " + collision.gameObject.name);
    }

    private void onTriggerExit2D(Collision2D collision)
    {
        Debug.Log("Collision Exited " + collision.gameObject.name);
    }

    private void onTriggerStay2D(Collision2D collision)
    {
        Debug.Log("Collision Stayed " + collision.gameObject.name);
    }

}
