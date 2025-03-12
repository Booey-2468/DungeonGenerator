using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateCooridor : MonoBehaviour
{
    public GenerateExit start;
    public Sprite topWall;
    public Sprite bottomWall;
    public Sprite leftWall;
    public Sprite rightWall;
    public Sprite topLeftCorner;
    public Sprite topRightCorner;
    public Sprite bottomLeftCorner;
    public Sprite bottomRightCorner;
    public Tilemap wallsTilemap;
    public List<Vector3Int> cooridorPos = new List<Vector3Int>();
    // Start is called before the first frame update
    void Start()
    {
        Vector3Int pos1;
        Vector3Int pos2;
        Vector3Int pos3;
        Vector3Int initialPos = start.entryPos[1];
        Direction currentDirection = start.exitDirection;
        
        switch(currentDirection)
        {
            case Direction.left:
                pos1 = new Vector3Int(initialPos[0]++, initialPos[1]++, 0);
                pos2 = new Vector3Int(initialPos[0]++, initialPos[1], 0);
                pos3 = new Vector3Int(initialPos[0]++, initialPos[1]--, 0);

                wallsTilemap.SetTile(pos1, new Tile() { sprite = bottomWall });
                wallsTilemap.SetTile(pos2, null);
                wallsTilemap.SetTile(pos3, new Tile() { sprite = topWall });

                cooridorPos.Add(pos1);
                cooridorPos.Add(pos2);
                cooridorPos.Add(pos3);
                break;
            case Direction.up:
                pos1 = new Vector3Int(initialPos[0]--, initialPos[1]++, 0);
                pos2 = new Vector3Int(initialPos[0], initialPos[1]++, 0);
                pos3 = new Vector3Int(initialPos[0]++, initialPos[1]++, 0);

                wallsTilemap.SetTile(pos1, new Tile() { sprite = rightWall });
                wallsTilemap.SetTile(pos2, null);
                wallsTilemap.SetTile(pos3, new Tile() { sprite = leftWall });

                cooridorPos.Add(pos1);
                cooridorPos.Add(pos2);
                cooridorPos.Add(pos3);
                break;
            case Direction.right:
                pos1 = new Vector3Int(initialPos[0]--, initialPos[1]++, 0);
                pos2 = new Vector3Int(initialPos[0]--, initialPos[1], 0);
                pos3 = new Vector3Int(initialPos[0]--, initialPos[1]--, 0);

                wallsTilemap.SetTile(pos1, new Tile() { sprite = bottomWall });
                wallsTilemap.SetTile(pos2, null);
                wallsTilemap.SetTile(pos3, new Tile() { sprite = topWall });

                cooridorPos.Add(pos1);
                cooridorPos.Add(pos2);
                cooridorPos.Add(pos3);
                break;
            case Direction.down:
                pos1 = new Vector3Int(initialPos[0]--, initialPos[1]--, 0);
                pos2 = new Vector3Int(initialPos[0], initialPos[1]--, 0);
                pos3 = new Vector3Int(initialPos[0]++, initialPos[1]--, 0);

                wallsTilemap.SetTile(pos1, new Tile() { sprite = rightWall });
                wallsTilemap.SetTile(pos2, null);
                wallsTilemap.SetTile(pos3, new Tile() { sprite = leftWall });

                cooridorPos.Add(pos1);
                cooridorPos.Add(pos2);
                cooridorPos.Add(pos3);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
