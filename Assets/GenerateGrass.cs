using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrass : MonoBehaviour
{
    [SerializeField] private GameObject grass;
    public GenerateWall walls;
    [SerializeField] private int gridOffset = 1;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < walls.sizeX; x++)
        {
            for (int y = 0; y < walls.sizeY; y++)
            {
                Vector2 pos = new Vector2((walls.posX + x) * gridOffset, (walls.posY + y) * gridOffset);
                GameObject tile = Instantiate(grass, pos, Quaternion.identity);
                tile.transform.SetParent(this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
