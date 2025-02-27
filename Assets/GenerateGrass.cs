using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateGrass : MonoBehaviour
{
    [SerializeField] private Tilemap backgroundTileMap;
    [SerializeField] private Sprite grass;

    public GenerateWall walls;
    [SerializeField] private int gridOffset = 1;
    private List<Vector3Int> tilePos = new List<Vector3Int>();
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < walls.sizeX; x++)
        {
            for (int y = 0; y < walls.sizeY; y++)
            {
                Vector3Int pos = new Vector3Int((walls.posX + x) * gridOffset, (walls.posY + y) * gridOffset, 0);
                backgroundTileMap.SetTile(pos, new Tile() { sprite = grass });
                tilePos.Add(pos);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
