using System.Collections;
using System.Collections.Generic;
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
            Vector3Int pos1 = new Vector3Int();
            Vector3Int pos2 = new Vector3Int();
            Vector3Int pos3 = new Vector3Int();


            int wallX = UnityEngine.Random.Range((int)walls.posX + 1, (int)(walls.posX + walls.sizeX - 1));
            int wallY = UnityEngine.Random.Range((int)walls.posY + 1, (int)(walls.posY + walls.sizeY - 1));



            int wallDirection = UnityEngine.Random.Range(1, 5);

            // 1 is left | 2 is up | 3 is right | 4 is down
            switch (wallDirection)
            {
                case 1:
                    wallX = walls.posX - 1;
                    pos1 = new Vector3Int(wallX, wallY++, 0);
                    pos2 = new Vector3Int(wallX, wallY, 0);
                    pos3 = new Vector3Int(wallX, wallY--, 0);


                    wallsTilemap.SetTile(pos1, new Tile() { sprite = bottomRightCorner });
                    wallsTilemap.SetTile(pos2, null);
                    wallsTilemap.SetTile(pos3, new Tile() { sprite = topRightCorner });

                    entryPos.Add(pos1);
                    entryPos.Add(pos2);
                    entryPos.Add(pos3);
                    exitDirection = Direction.left;
                    break;
                case 2:
                    wallY = walls.posY + walls.sizeY;

                    pos1 = new Vector3Int(wallX--, wallY, 0);
                    pos2 = new Vector3Int(wallX, wallY, 0);
                    pos3 = new Vector3Int(wallX++, wallY, 0);

                    wallsTilemap.SetTile(pos1, new Tile() { sprite = bottomRightCorner });
                    wallsTilemap.SetTile(pos2, null);
                    wallsTilemap.SetTile(pos3, new Tile() { sprite = bottomLeftCorner });

                    entryPos.Add(pos1);
                    entryPos.Add(pos2);
                    entryPos.Add(pos3);

                    exitDirection = Direction.up;
                    break;
                case 3:
                    wallX = walls.posX + walls.sizeX;

                    pos1 = new Vector3Int(wallX, wallY++, 0);
                    pos2 = new Vector3Int(wallX, wallY, 0);
                    pos3 = new Vector3Int(wallX, wallY--, 0);

                    wallsTilemap.SetTile(pos1, new Tile() { sprite = bottomLeftCorner });
                    wallsTilemap.SetTile(pos2, null);
                    wallsTilemap.SetTile(pos3, new Tile() { sprite = topLeftCorner });


                    entryPos.Add(pos1);
                    entryPos.Add(pos2);
                    entryPos.Add(pos3);

                    exitDirection = Direction.right;
                    break;
                case 4:
                    wallY = walls.posY - 1;


                    pos1 = new Vector3Int(wallX--, wallY, 0);
                    pos2 = new Vector3Int(wallX, wallY, 0);
                    pos3 = new Vector3Int(wallX++, wallY, 0);

                    wallsTilemap.SetTile(pos1, new Tile() { sprite = topRightCorner });
                    wallsTilemap.SetTile(pos2, null);
                    wallsTilemap.SetTile(pos3, new Tile() { sprite = topLeftCorner });

                    entryPos.Add(pos1);
                    entryPos.Add(pos2);
                    entryPos.Add(pos3);
                    exitDirection = Direction.down;
                    break;
            }
            valuesAssigned = false;
        }
    }
}
