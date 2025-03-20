using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

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
    private Vector3Int pos1;
    private Vector3Int pos2;
    private Vector3Int pos3;
    private Vector3Int currentPos;
    private Direction currentDirection;
    private int moveNum = 1;
    private bool isCorner = false;
    public List<Vector3Int> cooridorPos = new List<Vector3Int>();
    public List<Vector3Int> cardinalDirections;
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
            currentPos = start.entryPos[1];
            currentDirection = start.exitDirection;
            switch (currentDirection)
            {
                case Direction.left:
                    pos1 = new Vector3Int(currentPos[0] - moveNum, currentPos[1] + moveNum, 0);
                    pos2 = new Vector3Int(currentPos[0] - moveNum, currentPos[1], 0);
                    pos3 = new Vector3Int(currentPos[0] - moveNum, currentPos[1] - moveNum, 0);

                    SetCooridor(pos1, pos2, pos3, bottomWall, topWall);
                    break;
                case Direction.up:
                    pos1 = new Vector3Int(currentPos[0] - moveNum, currentPos[1] + moveNum, 0);
                    pos2 = new Vector3Int(currentPos[0], currentPos[1] + moveNum, 0);
                    pos3 = new Vector3Int(currentPos[0] + moveNum, currentPos[1] + moveNum, 0);

                    SetCooridor(pos1, pos2, pos3, rightWall, leftWall);
                    break;
                case Direction.right:

                    pos1 = new Vector3Int(currentPos[0] + moveNum, currentPos[1] + moveNum, 0);
                    pos2 = new Vector3Int(currentPos[0] + moveNum, currentPos[1], 0);
                    pos3 = new Vector3Int(currentPos[0] + moveNum, currentPos[1] - moveNum, 0);

                    SetCooridor(pos1, pos2, pos3, bottomWall, topWall);
                    break;
                case Direction.down:
                    pos1 = new Vector3Int(currentPos[0] - moveNum, currentPos[1] - moveNum, 0);
                    pos2 = new Vector3Int(currentPos[0], currentPos[1] - moveNum, 0);
                    pos3 = new Vector3Int(currentPos[0] + moveNum, currentPos[1] - moveNum, 0);

                    SetCooridor(pos1, pos2, pos3, rightWall, leftWall);
                    break;
            }
            currentPos = pos2;
            valuesAssigned = false;
        }
        else
        {
            int turnChance = UnityEngine.Random.Range(1, 10);

            if (turnChance >= 7)
            {
            }
            switch (currentDirection)
            {
                case Direction.left:
                    pos1 = new Vector3Int(currentPos[0] - moveNum, currentPos[1] + moveNum, 0);
                    pos2 = new Vector3Int(currentPos[0] - moveNum, currentPos[1], 0);
                    pos3 = new Vector3Int(currentPos[0] - moveNum, currentPos[1] - moveNum, 0);

                    SetCooridor(pos1, pos2, pos3, bottomWall, topWall);
                    break;
                case Direction.up:
                    pos1 = new Vector3Int(currentPos[0] - moveNum, currentPos[1] + moveNum, 0);
                    pos2 = new Vector3Int(currentPos[0], currentPos[1] + moveNum, 0);
                    pos3 = new Vector3Int(currentPos[0] + moveNum, currentPos[1] + moveNum, 0);

                    SetCooridor(pos1, pos2, pos3, rightWall, leftWall);
                    break;
                case Direction.right:

                    pos1 = new Vector3Int(currentPos[0] + moveNum, currentPos[1] + moveNum, 0);
                    pos2 = new Vector3Int(currentPos[0] + moveNum, currentPos[1], 0);
                    pos3 = new Vector3Int(currentPos[0] + moveNum, currentPos[1] - moveNum, 0);

                    SetCooridor(pos1, pos2, pos3, bottomWall, topWall);
                    break;
                case Direction.down:
                    pos1 = new Vector3Int(currentPos[0] - moveNum, currentPos[1] - moveNum, 0);
                    pos2 = new Vector3Int(currentPos[0], currentPos[1] - moveNum, 0);
                    pos3 = new Vector3Int(currentPos[0] + moveNum, currentPos[1] - moveNum, 0);

                    SetCooridor(pos1, pos2, pos3, rightWall, leftWall);
                    break;
            }
            currentPos = pos2;
        }
    }
    private void SetCooridor(Vector3Int pos1, Vector3Int pos2, Vector3Int pos3, Sprite sprite1, Sprite sprite2)
    {
        wallsTilemap.SetTile(pos1, new Tile() { sprite = sprite1 });
        wallsTilemap.SetTile(pos2, null);
        wallsTilemap.SetTile(pos3, new Tile() { sprite = sprite2 });

        cooridorPos.Add(pos1);
        cooridorPos.Add(pos2);
        cooridorPos.Add(pos3);
    }
    private void SetCorner(Vector3Int pos1, Vector3Int pos2, Vector3Int pos3, Direction currentDirection, bool turnLeft, Sprite sideSprite, Sprite cornerSprite, Sprite upperSprite)
    {
        Vector3Int localUp = new Vector3Int(0,0,0);
        Vector3Int localDown = new Vector3Int(0,0,0);
        switch(currentDirection)
        {
            case Direction.up:
                localUp = new Vector3Int(0,-1,0);
                localDown = new Vector3Int(0,1,0);
                break;
            case Direction.down:
                localUp = new Vector3Int(0, 1, 0);
                localDown = new Vector3Int(0, -1, 0);
                break;
            case Direction.left:
                localUp = new Vector3Int(-1, 0, 0);
                localDown = new Vector3Int(1, 0, 0);
                break;
            case Direction.right:
                localUp = new Vector3Int(1, 0, 0);
                localDown = new Vector3Int(-1, 0, 0);
                break;
        }
        wallsTilemap.SetTile(pos2, null);
        if (turnLeft)
        {
            wallsTilemap.SetTile(pos3, new Tile() { sprite = sideSprite });
            wallsTilemap.SetTile(pos1, null);

            wallsTilemap.SetTile(pos1 + localUp, new Tile() { sprite = cornerSprite });
            wallsTilemap.SetTile(pos3 + localDown, new Tile() { sprite = cornerSprite });
        }
        else
        {
            
            wallsTilemap.SetTile(pos1, new Tile() { sprite = sideSprite });
            wallsTilemap.SetTile(pos3, null);


            wallsTilemap.SetTile(pos3 + localUp, new Tile() { sprite = cornerSprite });
            wallsTilemap.SetTile(pos1 + localDown, new Tile() { sprite = cornerSprite });

        }

        wallsTilemap.SetTile(pos2 + cardinalDirections[2], new Tile() { sprite = upperSprite });

        cooridorPos.Add(pos1);
        cooridorPos.Add(pos2);
        cooridorPos.Add(pos3);
    }
}
