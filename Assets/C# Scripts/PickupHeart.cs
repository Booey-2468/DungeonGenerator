using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHeart : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");    // Finds player gameobject via tag
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null && player == collision.gameObject)   // Checks that player was correctly gotten and the collision is with player
        {
            PlayerScript playerHealthStats = player.GetComponent<PlayerScript>();   // Stores player script
            if(playerHealthStats.playerHealth < playerHealthStats.maxPlayerHealth - 1)  //Checks if playerHealth has little enough hearts to add a full heart
            {
                playerHealthStats.playerHealth += 2;    // Adds full heart to playerHealth/2 health and destroys itself
                Destroy(gameObject);
            }
            else if(playerHealthStats.playerHealth < playerHealthStats.maxPlayerHealth) // Checks if player health has little enough hearts for a half heart else do nothing
            {
                playerHealthStats.playerHealth++;
                Destroy(gameObject);
            }
        }
    }
}
