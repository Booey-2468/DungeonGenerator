using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float speed = 3;
    [HideInInspector]public float enemyHealth = 20;
    [SerializeField] private int attackDamage = 1;
    private Vector3 originalPosition;
    [HideInInspector]public int posX;
    [HideInInspector]public int posY;
    [HideInInspector] public int sizeX;
    [HideInInspector] public int sizeY;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        player = GameObject.FindWithTag("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
        if ((player.transform.position.x >= posX && player.transform.position.x <= posX + sizeX) && (player.transform.position.y >= posY && player.transform.position.y <= posY + sizeY))
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (float)speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, (float)speed * Time.deltaTime);
        }
    }
}
