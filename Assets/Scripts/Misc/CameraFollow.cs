using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    public float leftLimit = -1;
    public float rightLimit = 20;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), transform.position.y, transform.position.z);
    }
}