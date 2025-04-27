using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject endScreenPrefab;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null && player == collision.gameObject)
        {
            Time.timeScale = 0;
            Instantiate(endScreenPrefab);
        }
    }
}
