using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class GenerateCooridor : MonoBehaviour
{
    public GenerateWall walls;
    [SerializeField] private Sprite topWall;
    [SerializeField] private Sprite bottomWall;
    [SerializeField] private Sprite leftWall;
    [SerializeField] private Sprite rightWall;
    [SerializeField] private Sprite topLeftCorner;
    [SerializeField] private Sprite topRightCorner;
    [SerializeField] private Sprite bottomLeftCorner;
    [SerializeField] private Sprite bottomRightCorner;
    [SerializeField] private Tilemap wallsTileMap;
    private List<Vector3Int> entryPos = new List<Vector3Int>();
    // Start is called before the first frame update
    void Start()
    {

            Vector3Int pos1 = new Vector3Int();
            Vector3Int pos2 = new Vector3Int();
            Vector3Int pos3 = new Vector3Int();

            int wallX = UnityEngine.Random.Range(walls.posX, walls.posX - 1 + walls.sizeX);
            int wallY = UnityEngine.Random.Range(walls.posY, walls.posY - 1 + walls.sizeY);

            int prioritiseXY = UnityEngine.Random.Range(0, 2);
            int wallDirection = UnityEngine.Random.Range(1, 5);
            int wallAlign = 1;
            // 1 is left | 2 is up | 3 is right | 4 is down
            switch(wallDirection)
            {
                case 1:
                    wallX = walls.posX;

                    pos1 = new Vector3Int(wallX, wallY + wallAlign, 0);
                    pos2 = new Vector3Int(wallX, wallY, 0);
                    pos3 = new Vector3Int(wallX, wallY - wallAlign, 0);

                    wallsTileMap.SetTile(pos1, new Tile() {sprite = bottomRightCorner});
                    wallsTileMap.SetTile(pos2, null);
                    wallsTileMap.SetTile(pos3, new Tile() {sprite = topRightCorner});

                    entryPos.Add(pos1);
                    entryPos.Add(pos2);
                    entryPos.Add(pos3);
                    break;
                case 2:
                    wallY = walls.sizeY;

                    pos1 = new Vector3Int(wallX - wallAlign, wallY, 0);
                    pos2 = new Vector3Int(wallX, wallY, 0);
                    pos3 = new Vector3Int(wallX + wallAlign, wallY, 0);

                    wallsTileMap.SetTile(pos1, new Tile() { sprite = bottomRightCorner });
                    wallsTileMap.SetTile(pos2, null);
                    wallsTileMap.SetTile(pos3, new Tile() {sprite = bottomLeftCorner });

                    entryPos.Add(pos1);
                    entryPos.Add(pos2);
                    entryPos.Add(pos3);
                    break;
                case 3:
                    wallX = walls.sizeX;

                    pos1 = new Vector3Int(wallX, wallY + wallAlign, 0);
                    pos2 = new Vector3Int(wallX, wallY, 0);
                    pos3 = new Vector3Int(wallX, wallY - wallAlign, 0);

                    wallsTileMap.SetTile(pos1, new Tile() { sprite = bottomLeftCorner });
                    wallsTileMap.SetTile(pos2, null);
                    wallsTileMap.SetTile(pos3, new Tile() { sprite = topLeftCorner });

                    entryPos.Add(pos1);
                    entryPos.Add(pos2);
                    entryPos.Add(pos3);
                    break;
                case 4:
                    wallY = walls.posY;

                    pos1 = new Vector3Int(wallX - wallAlign, wallY, 0);
                    pos2 = new Vector3Int(wallX, wallY, 0);
                    pos3 = new Vector3Int(wallX + wallAlign, wallY, 0);

                    wallsTileMap.SetTile(pos1, new Tile() { sprite = topRightCorner });
                    wallsTileMap.SetTile(pos2, null);
                    wallsTileMap.SetTile(pos3, new Tile() { sprite = topLeftCorner });

                    entryPos.Add(pos1);
                    entryPos.Add(pos2);
                    entryPos.Add(pos3);
                    break;
            }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
