using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateWall : MonoBehaviour
{
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Sprite topWall;
    [SerializeField] private Sprite bottomWall;
    [SerializeField] private Sprite leftWall;
    [SerializeField] private Sprite rightWall;
    [SerializeField] private Sprite topLeftCorner;
    [SerializeField] private Sprite topRightCorner;
    [SerializeField] private Sprite bottomLeftCorner;
    [SerializeField] private Sprite bottomRightCorner;
    [SerializeField] public int sizeX;
    [SerializeField] public int sizeY;
    [SerializeField] public int posX;
    [SerializeField] public int posY;
    [SerializeField] private int gridOffset = 1;
    private Sprite currentSelection;
    
    public GenerateWall(Tilemap wallTilemap, Sprite topWall, Sprite bottomWall, Sprite leftWall, Sprite rightWall, Sprite topLeftCorner, Sprite topRightCorner, Sprite bottomLeftCorner, Sprite bottomRightCorner, int sizeX, int sizeY, int posX, int posY, int gridOffset)
    {
        this.wallTilemap = wallTilemap;
        this.topWall = topWall;
        this.bottomWall = bottomWall;
        this.leftWall = leftWall;
        this.rightWall = rightWall;
        this.topLeftCorner = topLeftCorner;
        this.topRightCorner = topRightCorner;
        this.bottomLeftCorner = bottomLeftCorner;
        this.bottomRightCorner = bottomRightCorner;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.posX = posX;
        this.posY = posY;
        this.gridOffset = gridOffset;
    }


    // Start is called before the first frame update
    void Start()
    {
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
                    wallTilemap.SetTile(pos, new Tile() { sprite = currentSelection });
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
