using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateWall : MonoBehaviour
{
    public Tilemap wallsTilemap;
    public Sprite topWall;
    public Sprite bottomWall;
    public Sprite leftWall;
    public Sprite rightWall;
    public Sprite topLeftCorner;
    public Sprite topRightCorner;
    public Sprite bottomLeftCorner;
    public Sprite bottomRightCorner;
    public int sizeX;
    public int sizeY;
    public int posX;
    public int posY;
    public int gridOffset = 1;
    private Sprite currentSelection;
    public GameObject grassPrefab;
    public Tilemap grassTilemap;
    public List<Vector3Int> wallList;
    public bool valuesAssigned = false;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (valuesAssigned)
        {
            GenerateGrass createGrass = Instantiate(grassPrefab, new Vector3Int(0, 0, 0), Quaternion.identity).GetComponent<GenerateGrass>();
            createGrass.backgroundTileMap = grassTilemap;
            createGrass.walls = this; createGrass.valuesAssigned = true;

            for (int x = -1; x <= sizeX; x++)
            {
                for (int y = -1; y <= sizeY; y++)
                {
                    if (x == -1)    // checks if its the first column of room
                    {
                        if (y == -1)
                        {
                            currentSelection = bottomLeftCorner;
                        }
                        else if (y == sizeY)
                        {
                            currentSelection = topLeftCorner;
                        }
                        else
                        {
                            currentSelection = leftWall;
                        }
                    }
                    else if (x == sizeX)// checks if its the last column of room 
                    {
                        if (y == -1)
                        {
                            currentSelection = bottomRightCorner;
                        }
                        else if (y == sizeY)
                        {
                            currentSelection = topRightCorner;
                        }
                        else
                        {
                            currentSelection = rightWall;
                        }
                    }
                    else          // Runs if its not the left or right wall/side
                    {
                        if (y == -1)
                        {
                            currentSelection = bottomWall;
                        }
                        else if (y == sizeY)
                        {
                            currentSelection = topWall;
                        }
                        else
                        {
                            currentSelection = null;
                        }
                    }
                    if (currentSelection != null)
                    {
                        Vector3Int pos = new Vector3Int((posX + x) * gridOffset, (posY + y) * gridOffset, 0);
                        wallsTilemap.SetTile(pos, new Tile() { sprite = currentSelection });
                    }
                }
            }
            valuesAssigned = false;
        }
    }
}
