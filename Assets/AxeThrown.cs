using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrown : MonoBehaviour
{
    public Vector2 initialDirection;
    public bool hasBeenCalled = false;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasBeenCalled) 
        {
            transform.Rotate(new Vector3Int(0, 0, -10));
            AxeThrow();
        }

    }
    void AxeThrow()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(initialDirection * 10 * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject == player)
        {
            player.GetComponent<PlayerMovement>().playerHealth--;
            Destroy(gameObject);
        }
    }

}
