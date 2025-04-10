using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float speed;
    private Vector3 originalPosition;
    public int posX = 0;
    public int posY = 0;
    public int sizeX = 0;
    public int sizeY = 0;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        player = GameObject.FindWithTag("Player");   
        if (speed == 0)
            speed = 3;   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (float) speed * Time.deltaTime) ;
    }
}
