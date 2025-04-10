using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(Vector3.MoveTowards(transform.position, player.transform.position, 10));
    }
}
