using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GenerateGrass : MonoBehaviour
{
    public Tilemap backgroundTileMap;
    public Sprite grass;
    public Sprite grass2;
    public Sprite grass3;
    private Sprite groundSprite;

    public GenerateWall walls;
    [SerializeField] private int gridOffset = 1;
    private List<Vector3Int> tilePos = new List<Vector3Int>();

    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void CreateGrass()
    {
        for (int x = 0; x < walls.sizeX; x++)
        {
            for (int y = 0; y < walls.sizeY; y++)
            {
                int randomNum = UnityEngine.Random.Range(1, 3);
                switch (randomNum)
                {
                    case 1:
                        groundSprite = grass;
                        break;
                    case 2:
                        groundSprite = grass2;
                        break;
                    case 3:
                        groundSprite = grass3;
                        break;
                }
                Vector3Int pos = new Vector3Int((walls.posX + x) * gridOffset, (walls.posY + y) * gridOffset, 0);
                backgroundTileMap.SetTile(pos, new Tile() { sprite = groundSprite });
                tilePos.Add(pos);
            }
        }
    }
}
