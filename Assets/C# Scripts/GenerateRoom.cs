using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateWall : MonoBehaviour
{
    [HideInInspector] public Tilemap wallsTilemap;
    [SerializeField] private Sprite topWall;
    [SerializeField] private Sprite bottomWall;
    [SerializeField] private Sprite leftWall;
    [SerializeField] private Sprite rightWall;
    [SerializeField] private Sprite topLeftCorner;
    [SerializeField] private Sprite topRightCorner;
    [SerializeField] private Sprite bottomLeftCorner;
    [SerializeField] private Sprite bottomRightCorner;
    [HideInInspector] public int sizeX;
    [HideInInspector] public int sizeY;
    [HideInInspector] public int posX;
    [HideInInspector] public int posY;
    [HideInInspector] public bool lastRoom = false;
    private int gridOffset = 1;
    private Sprite currentSelection;
    [HideInInspector] public bool spawnEnemies = true;
    [SerializeField] private GameObject grassPrefab;
    [HideInInspector] public Tilemap grassTilemap;
    [HideInInspector] public List<Vector3Int> wallList;

    // Update is called once per frame
    public void CreateRoom()
    {
        GenerateGrass createBackdrop = Instantiate(grassPrefab, new Vector3Int(0, 0, 0), Quaternion.identity).GetComponent<GenerateGrass>();
        createBackdrop.backgroundTileMap = grassTilemap;
        createBackdrop.walls = this;
        createBackdrop.spawnEnemies = spawnEnemies;
        createBackdrop.lastRoom = lastRoom;
        createBackdrop.CreateBackdrop();
        Destroy(createBackdrop);


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
                    wallList.Add(pos);
                }
            }
        }
    }
}
