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
        transform.Rotate(new Vector3Int(0, 0, -10));
        if (hasBeenCalled) 
        {
            AxeThrow();
        }

    }
    void AxeThrow()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(initialDirection * 10);
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.transform.gameObject == player)
        {
            Destroy(this.gameObject);
        }
    }

}
