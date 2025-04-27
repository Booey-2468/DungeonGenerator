using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float speed = 3;
    public float enemyHealth = 20;
    [SerializeField] private int attackDamage = 1;
    private Vector3 originalPosition;
    [HideInInspector]public int posX;
    [HideInInspector]public int posY;
    [HideInInspector] public int sizeX;
    [HideInInspector] public int sizeY;
    [HideInInspector] public bool hasBeenCalled = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        player = GameObject.FindWithTag("Player");   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasBeenCalled && player != null)
        {
            if (enemyHealth <= 0)
            {
                Destroy(gameObject);
            }
            else if ((player.transform.position.x >= posX && player.transform.position.x <= posX + sizeX) && (player.transform.position.y >= posY && player.transform.position.y <= posY + sizeY))
            {
                transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.fixedDeltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, speed * Time.fixedDeltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            player.GetComponent<PlayerScript>().playerHealth -= attackDamage;
        }
    }
}
