using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private Sprite topWall;
    [SerializeField] private Sprite bottomWall;
    [SerializeField] private Sprite leftWall;
    [SerializeField] private Sprite rightWall;
    [SerializeField] private Sprite topLeftCorner;
    [SerializeField] private Sprite topRightCorner;
    [SerializeField] private Sprite bottomLeftCorner;
    [SerializeField] private Sprite bottomRightCorner;
    [SerializeField] private Tilemap wallsTileMap;
    [SerializeField] private Tilemap grassTileMap;

    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private GameObject exitPrefab;
    [SerializeField] private GameObject cooridorPrefab;
    GameObject roomSpawner;
    private List<List<Vector3Int>> tileLocations = new List<List<Vector3Int>>();
    private List<GenerateWall> roomList = new List<GenerateWall>();
    private List<GenerateExit> exitList = new List<GenerateExit>();
    private List<GenerateCooridor> cooridorList = new List<GenerateCooridor>();


    private List<Vector3Int> cardinalDirections = new List<Vector3Int> 
    { 
        new Vector3Int(-1, 0 ,0),
        new Vector3Int(1, 0 ,0),
        new Vector3Int(0, 1 ,0),
        new Vector3Int (0, -1 ,0),
    };
    // Start is called before the first frame update

    int posX = -5, posY = -5, sizeX = 10, sizeY = 10;
    void Awake()
    {
        List<Sprite> spriteList = new List<Sprite>() { topWall, bottomWall, leftWall, rightWall, topLeftCorner, topRightCorner, bottomLeftCorner, bottomRightCorner};
        GenerateWall initialRoom = Instantiate(roomPrefab, new Vector3Int(0,0,0), Quaternion.identity).GetComponent<GenerateWall>();


        initialRoom.topWall = topWall; initialRoom.bottomWall = bottomWall; initialRoom.leftWall = leftWall; initialRoom.rightWall = rightWall;
        initialRoom.topLeftCorner = topLeftCorner; initialRoom.topRightCorner = topRightCorner; initialRoom.bottomLeftCorner = bottomLeftCorner; 
        initialRoom.bottomRightCorner = bottomRightCorner; initialRoom.wallsTilemap = wallsTileMap; initialRoom.posX = posX; 
        initialRoom.posY = posY; initialRoom.sizeX = sizeX; initialRoom.sizeY = sizeY;
        initialRoom.grassTilemap = grassTileMap; initialRoom.gridOffset = 1; initialRoom.valuesAssigned = true;
        roomList.Add(initialRoom);


    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < roomList.Count; i++)
        {
            CreateAllExits(roomList[i]);
            roomList.Remove(roomList[i]);
        }
        for (int i = 0; i < exitList.Count; i++)
        {
            if (!exitList[i].isOpening)
            {
                StartCooridor(exitList[i]);
                exitList.Remove(exitList[i]);
            }
        }

        for (int i = 0; i < cooridorList.Count; i++)
        {
            if (cooridorList[i].cooridorBlocked)
            {
                cooridorList.Remove(cooridorList[i]);
            }


            if (cooridorList[i].generateRoom)
            {
                posX = (cooridorList[i].currentPos + cardinalDirections[(int)cooridorList[i].currentDirection])[0];
                posY = (cooridorList[i].currentPos + cardinalDirections[(int)cooridorList[i].currentDirection])[1];
                sizeX = 10;
                sizeY = 10;

                for(int item = 0; item < tileLocations.Count; i++)
                {
                    for (int location = 0; location < tileLocations[item].Count; location++)
                    {
                        if (tileLocations[item][location][0] > posX && tileLocations[item][location][0] <= posX + sizeX)
                        {
                            cooridorList[i].generateRoom = false;
                            break;
                        }
                        if (tileLocations[item][location][1] > posY && tileLocations[item][location][1] <= posY + sizeY)
                        {
                            cooridorList[i].generateRoom = false;
                            break;
                        }
                    }

                }
                if (cooridorList[i].generateRoom)
                {
                    CreateRoom(posX, posY, sizeX, sizeY, cooridorList[i].currentPos + cardinalDirections[(int)cooridorList[i].currentDirection]);
                }
            }
        }
    }

    void CreateAllExits(GenerateWall currentRoom)
    {
        int numOfExits = UnityEngine.Random.Range(1, 5);
        List<List<Vector3Int>> exitLocations = new List<List<Vector3Int>>();
        for (int i = 0; i < numOfExits; i++)
        {
            GenerateExit currentExit = Instantiate(exitPrefab, new Vector3Int(0, 0, 0), Quaternion.identity).GetComponent<GenerateExit>();
            currentExit.topLeftCorner = topLeftCorner; currentExit.topRightCorner = topRightCorner; currentExit.bottomLeftCorner = bottomLeftCorner;
            currentExit.bottomRightCorner = bottomRightCorner; currentExit.walls = currentRoom; currentExit.wallsTilemap = wallsTileMap; currentExit.valuesAssigned = true;
            currentExit.checkPos = exitLocations;

            exitList.Add(currentExit);
            tileLocations.Add(currentExit.entryPos);
            exitLocations.Add(currentExit.entryPos);
        }
    }
    void StartCooridor(GenerateExit currentExit)
    {
        GenerateCooridor currentCooridor = Instantiate(cooridorPrefab, new Vector3Int(0, 0, 0), Quaternion.identity).GetComponent<GenerateCooridor>();
        currentCooridor.topWall = topWall; currentCooridor.bottomWall = bottomWall; currentCooridor.leftWall = leftWall; currentCooridor.rightWall = rightWall;
        currentCooridor.topLeftCorner = topLeftCorner; currentCooridor.topRightCorner = topRightCorner; currentCooridor.bottomLeftCorner = bottomLeftCorner;
        currentCooridor.bottomRightCorner = bottomRightCorner; currentCooridor.wallsTilemap = wallsTileMap; currentCooridor.cardinalDirections = cardinalDirections;
        currentCooridor.start = currentExit; currentCooridor.valuesAssigned = true;

        for (int i = 0; i < currentCooridor.cooridorPos.Count; i++)
        {
            tileLocations.Add(currentCooridor.cooridorPos[i]);
        }
    }
    void CreateRoom(int posX, int posY, int sizeX, int sizeY, Vector3Int entryPoint)
    {
        GenerateWall currentRoom = Instantiate(roomPrefab, new Vector3Int(0, 0, 0), Quaternion.identity).GetComponent<GenerateWall>();

        currentRoom.topWall = topWall; currentRoom.bottomWall = bottomWall; currentRoom.leftWall = leftWall; currentRoom.rightWall = rightWall;
        currentRoom.topLeftCorner = topLeftCorner; currentRoom.topRightCorner = topRightCorner; currentRoom.bottomLeftCorner = bottomLeftCorner;    //Initialises room object with values
        currentRoom.bottomRightCorner = bottomRightCorner; currentRoom.wallsTilemap = wallsTileMap; currentRoom.posX = posX;
        currentRoom.posY = posY; currentRoom.sizeX = sizeX; currentRoom.sizeY = sizeY;
        currentRoom.grassTilemap = grassTileMap; currentRoom.gridOffset = 1; currentRoom.valuesAssigned = true;
        roomList.Add(currentRoom);
        tileLocations.Add(currentRoom.wallList);
    }

    void PlaceRoomPos(Direction currentDirection, int posX, int posY, int sizeX, int sizeY)
    {
        Vector3Int placementLocation;
        int wallX = UnityEngine.Random.Range( ((-sizeX + 1) / 2) + posX, ((sizeX - 1) / 2) + posX);
        int wallY = UnityEngine.Random.Range(((-sizeY + 1) / 2) + posY, ((sizeY - 1) / 2) + posY);
        switch (currentDirection)
        {
            case Direction.left:
                placementLocation = new Vector3Int(-sizeX + posX, wallY, 0);
                break;
            case Direction.right:
                placementLocation = new Vector3Int( posX, wallY, 0);
                break;
            case Direction.up:
                placementLocation = new Vector3Int( wallX, posY, 0);

                break;
            case Direction.down:
                placementLocation = new Vector3Int( wallX , -sizeY + posY, 0);
                break;
        }
    }
}
