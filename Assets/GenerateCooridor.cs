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
    public Vector3Int currentPos;
    private Direction currentDirection;
    private int moveNum = 1;
    private bool isCorner = false;
    private int cooridorCount = 0;
    public List<List<Vector3Int>> cooridorPos = new List<List<Vector3Int>>();
    public List<Vector3Int> cardinalDirections;
    public bool valuesAssigned = false;
    public bool cooridorBlocked = false;
    public int current;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (valuesAssigned && !cooridorBlocked)
        {
            currentPos = start.entryPos[1][1];
            currentDirection = start.exitDirection;
            valuesAssigned = false;
        }

        if(!valuesAssigned && !cooridorBlocked)
        {
            int turnChance = UnityEngine.Random.Range(1, 100);
            int leftOrRight = UnityEngine.Random.Range(0, 2);
            bool turnLeft = false;
            Sprite sprite1 = null;
            Sprite sprite2 = null;
            Sprite terminatingSprite = null;
            List<Sprite> turningSpriteList = new List<Sprite>() {null, null, null};

            bool generateRoom = (turnChance < 10 && cooridorCount > 1);
            isCorner = (turnChance > 75 && cooridorCount > 3);  // 25% chance to turn and must have at least 3 more cooridors till the next turn

            switch (currentDirection)
            {
                case Direction.left:
                    pos1 = new Vector3Int(currentPos[0] - moveNum, currentPos[1] + moveNum, 0);
                    pos2 = new Vector3Int(currentPos[0] - moveNum, currentPos[1], 0);
                    pos3 = new Vector3Int(currentPos[0] - moveNum, currentPos[1] - moveNum, 0);

                    sprite1 = bottomWall;
                    sprite2 = topWall;
                    terminatingSprite = rightWall;

                    if (leftOrRight == 1 && isCorner)
                    {
                        turnLeft = true;
                        turningSpriteList = new List<Sprite>() { bottomWall, topLeftCorner, rightWall };

                        List<Vector3Int> returnStore = SetCorner(pos1, pos2, pos3, currentDirection, turnLeft, turningSpriteList[0], turningSpriteList[1], turningSpriteList[2]);
                        pos1 = returnStore[0]; pos2 = returnStore[1]; pos3 = returnStore[2]; 
                        
                        currentDirection = Direction.down;
                    }
                    else if (leftOrRight == 0 && isCorner)
                    {
                        turningSpriteList = new List<Sprite>() { topWall, bottomLeftCorner, rightWall };

                        List<Vector3Int> returnStore = SetCorner(pos1, pos2, pos3, currentDirection, turnLeft, turningSpriteList[0], turningSpriteList[1], turningSpriteList[2]);
                        pos1 = returnStore[0]; pos2 = returnStore[1]; pos3 = returnStore[2];

                        currentDirection = Direction.up;
                    }
                    break;
                case Direction.up:
                    pos1 = new Vector3Int(currentPos[0] - moveNum, currentPos[1] + moveNum, 0);
                    pos2 = new Vector3Int(currentPos[0], currentPos[1] + moveNum, 0);
                    pos3 = new Vector3Int(currentPos[0] + moveNum, currentPos[1] + moveNum, 0);

                    sprite1 = rightWall;
                    sprite2 = leftWall;
                    terminatingSprite = bottomWall;

                    if (leftOrRight == 1 && isCorner)
                    {
                        turnLeft = true;
                        turningSpriteList = new List<Sprite>() { leftWall, topRightCorner, bottomWall };

                        List<Vector3Int> returnStore = SetCorner(pos1, pos2, pos3, currentDirection, turnLeft, turningSpriteList[0], turningSpriteList[1], turningSpriteList[2]);
                        pos1 = returnStore[0]; pos2 = returnStore[1]; pos3 = returnStore[2];

                        currentDirection = Direction.left;
                    }
                    else if (leftOrRight == 0 && isCorner)
                    {
                        turningSpriteList = new List<Sprite>() { rightWall, topLeftCorner, bottomWall };

                        List<Vector3Int> returnStore = SetCorner(pos1, pos2, pos3, currentDirection, turnLeft, turningSpriteList[0], turningSpriteList[1], turningSpriteList[2]);
                        pos1 = returnStore[0]; pos2 = returnStore[1]; pos3 = returnStore[2];

                        currentDirection = Direction.right;
                    }
                    break;
                case Direction.right:

                    pos1 = new Vector3Int(currentPos[0] + moveNum, currentPos[1] + moveNum, 0);
                    pos2 = new Vector3Int(currentPos[0] + moveNum, currentPos[1], 0);
                    pos3 = new Vector3Int(currentPos[0] + moveNum, currentPos[1] - moveNum, 0);

                    sprite1 = bottomWall;
                    sprite2 = topWall;
                    terminatingSprite = leftWall;

                    if (leftOrRight == 1 && isCorner)
                    {
                        turnLeft = true;
                        turningSpriteList = new List<Sprite>() { topWall, bottomRightCorner, leftWall };

                        List<Vector3Int> returnStore = SetCorner(pos1, pos2, pos3, currentDirection, turnLeft, turningSpriteList[0], turningSpriteList[1], turningSpriteList[2]);
                        pos1 = returnStore[0]; pos2 = returnStore[1]; pos3 = returnStore[2];

                        currentDirection = Direction.up;
                    }
                    else if (leftOrRight == 0 && isCorner)
                    {
                        turningSpriteList = new List<Sprite>() { bottomWall, topRightCorner, leftWall };

                        List<Vector3Int> returnStore = SetCorner(pos1, pos2, pos3, currentDirection, turnLeft, turningSpriteList[0], turningSpriteList[1], turningSpriteList[2]);
                        pos1 = returnStore[0]; pos2 = returnStore[1]; pos3 = returnStore[2];

                        currentDirection = Direction.down;
                    }
                    break;
                case Direction.down:
                    pos1 = new Vector3Int(currentPos[0] - moveNum, currentPos[1] - moveNum, 0);
                    pos2 = new Vector3Int(currentPos[0], currentPos[1] - moveNum, 0);
                    pos3 = new Vector3Int(currentPos[0] + moveNum, currentPos[1] - moveNum, 0);

                    sprite1 = rightWall;
                    sprite2 = leftWall;
                    terminatingSprite = topWall;

                    if (leftOrRight == 1 && isCorner)
                    {
                        turnLeft = true;
                        turningSpriteList = new List<Sprite>() { rightWall, bottomLeftCorner, topWall };

                        List<Vector3Int> returnStore = SetCorner(pos1, pos2, pos3, currentDirection, turnLeft, turningSpriteList[0], turningSpriteList[1], turningSpriteList[2]);
                        pos1 = returnStore[0]; pos2 = returnStore[1]; pos3 = returnStore[2];

                        currentDirection = Direction.right;
                    }
                    else if(leftOrRight == 0 && isCorner)
                    {
                        turningSpriteList = new List<Sprite>() { leftWall, bottomRightCorner, topWall };

                        List<Vector3Int> returnStore = SetCorner(pos1, pos2, pos3, currentDirection, turnLeft, turningSpriteList[0], turningSpriteList[1], turningSpriteList[2]);
                        pos1 = returnStore[0]; pos2 = returnStore[1]; pos3 = returnStore[2];

                        currentDirection = Direction.left;
                    }
                    break;
            }
            if (!isCorner)
            {
                SetCooridor(pos1, pos2, pos3, sprite1, sprite2, terminatingSprite);
            }
            else
            {
                cooridorCount = 0;
            }

            currentPos = pos2;
        }
    }
    private void SetCooridor(Vector3Int pos1, Vector3Int pos2, Vector3Int pos3, Sprite sprite1, Sprite sprite2, Sprite terminatingSprite)
    {
        if (wallsTilemap.GetTile(pos1) == null && wallsTilemap.GetTile(pos2) == null && wallsTilemap.GetTile(pos3) == null)
        {
            wallsTilemap.SetTile(pos1, new Tile() { sprite = sprite1 });
            wallsTilemap.SetTile(pos2, null);
            wallsTilemap.SetTile(pos3, new Tile() { sprite = sprite2 });

            cooridorPos.Add(new List<Vector3Int>() {pos1, pos2, pos3 });
            cooridorCount++;
        }
        else
        {
            pos1 = cooridorPos[cooridorPos.Count][0];
            pos2 = cooridorPos[cooridorPos.Count][1];
            pos3 = cooridorPos[cooridorPos.Count][2];

            wallsTilemap.SetTile(pos1, null);
            wallsTilemap.SetTile(pos2, new Tile() { sprite = terminatingSprite });
            wallsTilemap.SetTile(pos3, null);
            cooridorBlocked = true;
        }
    }


    private List<Vector3Int> SetCorner(Vector3Int pos1, Vector3Int pos2, Vector3Int pos3, Direction initialDirection, bool turnLeft, Sprite sideSprite, Sprite cornerSprite, Sprite upperSprite)
    {
        Vector3Int localUp = new Vector3Int(0,0,0);
        Vector3Int localDown = new Vector3Int(0,0,0);
        Vector3Int localLeft = new Vector3Int(0, 0, 0);
        Vector3Int localRight = new Vector3Int(0, 0, 0);
        switch (initialDirection)
        {
            case Direction.up:
                localUp = new Vector3Int(0,1,0);
                localDown = new Vector3Int(0,-1,0);
                localLeft = new Vector3Int(-1, 0, 0);
                localRight = new Vector3Int(1, 0, 0);
                break;
            case Direction.down:
                localUp = new Vector3Int(0, -1, 0);
                localDown = new Vector3Int(0, 1, 0);
                localLeft = new Vector3Int(1, 0, 0);
                localRight = new Vector3Int(-1, 0, 0);
                break;
            case Direction.left:
                localUp = new Vector3Int(-1, 0, 0);
                localDown = new Vector3Int(1, 0, 0);
                localLeft = new Vector3Int(0, -1, 0);
                localRight = new Vector3Int(0, 1, 0);
                break;
            case Direction.right:
                localUp = new Vector3Int(1, 0, 0);
                localDown = new Vector3Int(-1, 0, 0);
                localLeft = new Vector3Int(0, 1, 0);
                localRight = new Vector3Int(0, -1, 0);
                break;
        }
        wallsTilemap.SetTile(pos2, null);
        if (turnLeft)
        {
            wallsTilemap.SetTile(pos2 + localRight, new Tile() { sprite = sideSprite });
            wallsTilemap.SetTile(pos2 + localLeft, null);


            wallsTilemap.SetTile(pos2 + localLeft + localDown, new Tile() { sprite = cornerSprite });

            wallsTilemap.SetTile(pos2 + localUp, new Tile() { sprite = upperSprite });

            pos2 += localLeft;
            pos1 = pos2 + localUp;
            pos3 = pos2 + localDown;

        }
        else
        {
            wallsTilemap.SetTile(pos2 + localLeft, new Tile() { sprite = sideSprite });

            wallsTilemap.SetTile(pos2 + localRight, null);

            wallsTilemap.SetTile(pos2 + localRight + localDown, new Tile() { sprite = cornerSprite });

            wallsTilemap.SetTile(pos2 + localUp, new Tile() { sprite = upperSprite });

            pos2 += localRight;
            pos1 = pos2 + localUp;
            pos3 = pos2 + localDown;

        }
        wallsTilemap.SetTile(pos1, new Tile() { sprite = upperSprite });


        cooridorPos.Add(new List<Vector3Int>() { pos1, pos2, pos3 });

        return new List<Vector3Int>() { pos1, pos2, pos3 };

    }
}
