using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;
public enum Direction
{
    left,
    right,
    up,
    down
}
public class GenerateExit : MonoBehaviour
{
    public GenerateWall walls;
    public Sprite topLeftCorner;
    public Sprite topRightCorner;
    public Sprite bottomLeftCorner;
    public Sprite bottomRightCorner;
    public Tilemap wallsTilemap;
    public List<Vector3Int> entryPos = new List<Vector3Int>();
    public Direction exitDirection;
    public bool customExit = false;
    public int wallX = 0, wallY = 0;    
    public bool isOpening = false;
    public int wallDirection;

    private List<Vector3Int> cardinalDirections = new List<Vector3Int>
    {
        new Vector3Int(-1, 0 ,0),
        new Vector3Int(1, 0 ,0),
        new Vector3Int(0, 1 ,0),
        new Vector3Int (0, -1 ,0),
    };

    // Update is called once per frame
    public void CreateExit()
    {

        Vector3Int pos1 = new Vector3Int();
        Vector3Int pos2 = new Vector3Int();
        Vector3Int pos3 = new Vector3Int();

        if (!customExit)
        {
            wallX = UnityEngine.Random.Range((int)walls.posX + 1, (int)(walls.posX + walls.sizeX - 1));
            wallY = UnityEngine.Random.Range((int)walls.posY + 1, (int)(walls.posY + walls.sizeY - 1));
            exitDirection = (Direction)UnityEngine.Random.Range(0, 4);
        }

        // 1 is left | 2 is up | 3 is right | 4 is down
        switch (exitDirection)
        {
            case Direction.left:
                wallX = walls.posX - 1;
                pos1 = new Vector3Int(wallX, wallY, 0) + cardinalDirections[(int)Direction.up];
                pos2 = new Vector3Int(wallX, wallY, 0);
                pos3 = new Vector3Int(wallX, wallY, 0) + cardinalDirections[(int)Direction.down];

                SetExit(pos1, pos2, pos3, bottomRightCorner,topRightCorner, exitDirection);
                   
                break;
            case Direction.up:
                wallY = walls.posY + walls.sizeY;

                pos1 = new Vector3Int(wallX, wallY, 0) + cardinalDirections[(int)Direction.left];
                pos2 = new Vector3Int(wallX, wallY, 0);
                pos3 = new Vector3Int(wallX, wallY, 0) + cardinalDirections[(int)Direction.right];

                SetExit(pos1, pos2, pos3, bottomRightCorner, bottomLeftCorner, exitDirection);

                break;
            case Direction.right:
                wallX = walls.posX + walls.sizeX;

                pos1 = new Vector3Int(wallX, wallY, 0) + cardinalDirections[(int)Direction.up];
                pos2 = new Vector3Int(wallX, wallY, 0);
                pos3 = new Vector3Int(wallX, wallY, 0) + cardinalDirections[(int)Direction.down];

                SetExit(pos1, pos2, pos3, bottomLeftCorner, topLeftCorner, exitDirection);
                break;
            case Direction.down:
                wallY = walls.posY - 1;

                pos1 = new Vector3Int(wallX, wallY, 0) + cardinalDirections[(int)Direction.left];
                pos2 = new Vector3Int(wallX, wallY, 0);
                pos3 = new Vector3Int(wallX, wallY, 0) + cardinalDirections[(int)Direction.right];

                SetExit(pos1, pos2, pos3, topRightCorner, topLeftCorner, exitDirection);
                break;
        }
    }
    private void SetExit(Vector3Int pos1, Vector3Int pos2, Vector3Int pos3, Sprite sprite1, Sprite sprite2, Direction exitDirection)
    {
        List<Sprite> bannedSprites = new List<Sprite>(){topLeftCorner, topRightCorner, bottomLeftCorner, bottomRightCorner, null};

        bool intersectingExit = (!bannedSprites.Contains(wallsTilemap.GetSprite(pos1)) && !bannedSprites.Contains(wallsTilemap.GetSprite(pos2)) && !bannedSprites.Contains(wallsTilemap.GetSprite(pos3)));
        bool isNotBlocked = true;
        if (!isOpening)
        {
            isNotBlocked = (wallsTilemap.GetTile(pos1 + cardinalDirections[(int)exitDirection]) == null && wallsTilemap.GetTile(pos2 + cardinalDirections[(int)exitDirection]) == null && wallsTilemap.GetTile(pos3 + cardinalDirections[(int)exitDirection]) == null);
        }
        if (intersectingExit && isNotBlocked)
        {
            wallsTilemap.SetTile(pos1, new Tile() { sprite = sprite1 });
            wallsTilemap.SetTile(pos2, null);
            wallsTilemap.SetTile(pos3, new Tile() { sprite = sprite2 });

            entryPos.Add(pos1);
            entryPos.Add(pos2);
            entryPos.Add(pos3);
        }
    }
}
