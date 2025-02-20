using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCooridor : MonoBehaviour
{
    public GenerateWall walls;
    // Start is called before the first frame update
    void Start()
    {
        int wallX = Random.Range(walls.posX, walls.posX + walls.sizeX);
        int wallY = Random.Range(walls.posY, walls.posY + walls.sizeY);
        int wallDirection = Random.Range(1, 5);
        switch (wallDirection)
        {
            case 1:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
