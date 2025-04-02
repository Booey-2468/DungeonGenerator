using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        InstantiateRoom(posX, posY, sizeX, sizeY);
    }
    // Update is called once per frame
    void Update()
    {
        if (roomList.Count < 10)
        {
            CreateBranchingExits();
            CreateCooridorFromExit();
            CreateRoomFromCooridor();

        }
    }
    void CreateBranchingExits()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            CreateAllExits(roomList[i]);
            roomList.Remove(roomList[i]);
        }
    }
    void CreateCooridorFromExit() 
    {
        for (int i = 0; i < exitList.Count; i++)
        {
            if (!exitList[i].isOpening)
            {
                StartCooridor(exitList[i]);
                exitList.Remove(exitList[i]);
            }

        }
    }
    void CreateRoomFromCooridor()
    {
        if (cooridorList.LastOrDefault() != null)
        {
            for (int i = 0; i < cooridorList.IndexOf(cooridorList.LastOrDefault()); i++)
            {
                if (cooridorList[i].cooridorBlocked)
                {
                    cooridorList.Remove(cooridorList[i]);

                }
                else if (cooridorList[i].generateRoom)
                {
                    posX = (cooridorList[i].currentPos + cardinalDirections[(int)cooridorList[i].currentDirection])[0];
                    posY = (cooridorList[i].currentPos + cardinalDirections[(int)cooridorList[i].currentDirection])[1];
                    sizeX = 10;
                    sizeY = 10;
                    Vector3Int roomSpawnPos = PlaceRoomPos(cooridorList[i].currentDirection, posX, posY, sizeX, sizeY);
                    posX = roomSpawnPos.x;
                    posY = roomSpawnPos.y;
                    if (tileLocations.LastOrDefault() != null)
                    {
                        for (int item = 0; item <= tileLocations.IndexOf(tileLocations.LastOrDefault()); item++) // Check if any tiles in area
                        {
                            if (tileLocations[item].LastOrDefault() != null)
                            {
                                for (int location = 0; location <= tileLocations[item].IndexOf(tileLocations[item].LastOrDefault()); location++)
                                {
                                    if (tileLocations[item][location].x >= posX && tileLocations[item][location].x <= posX + sizeX)
                                    {
                                        cooridorList[i].generateRoom = false;
                                        break;
                                    }
                                    if (tileLocations[item][location].y >= posY && tileLocations[item][location].y <= posY + sizeY)
                                    {
                                        cooridorList[i].generateRoom = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        cooridorList[i].generateRoom = false;
                    }

                    if (cooridorList[i].generateRoom && cooridorList[i].cooridorPos.LastOrDefault() != null)
                    {
                        GenerateWall spawnedRoom = InstantiateRoom(posX, posY, sizeX, sizeY);
                        InstantiateExit(spawnedRoom, true, true, cooridorList[i].cooridorPos.LastOrDefault()[1].x, cooridorList[i].cooridorPos.LastOrDefault()[1].y, cooridorList[i].currentDirection);
                        cooridorList.Remove(cooridorList[i]);

                    }
                }
            }
        }
    }
    void CreateAllExits(GenerateWall currentRoom, int numOfExits = -1, bool isOpening = false, bool customExit = false, int x = 0, int y = 0, int exitDirection = 1)
    {

        if(numOfExits < 0)
        {
            numOfExits = UnityEngine.Random.Range(1, 5);
        }
        for (int i = 0; i < numOfExits; i++)
        {
            GenerateExit currentExit = InstantiateExit(currentRoom, isOpening, customExit);
        }
    }
    GenerateExit InstantiateExit(GenerateWall currentRoom, bool isOpening = false, bool customExit = false, int x = 0, int y = 0, Direction exitDirection = Direction.up)
    {
        GenerateExit currentExit = Instantiate(exitPrefab, new Vector3Int(0, 0, 0), Quaternion.identity).GetComponent<GenerateExit>();
        currentExit.topLeftCorner = topLeftCorner;
        currentExit.topRightCorner = topRightCorner;
        currentExit.bottomLeftCorner = bottomLeftCorner;
        currentExit.bottomRightCorner = bottomRightCorner;
        currentExit.walls = currentRoom;
        currentExit.wallsTilemap = wallsTileMap;
        currentExit.isOpening = isOpening;

        if (customExit)
        {
            currentExit.customExit = customExit;
            currentExit.wallX = x;
            currentExit.wallY = y;
            currentExit.exitDirection = exitDirection;
        }
        currentExit.CreateExit();

        exitList.Add(currentExit);
        tileLocations.Add(currentExit.entryPos);
        Destroy(currentExit.transform.gameObject);

        return currentExit;

    }
    void StartCooridor(GenerateExit currentExit)
    {
        GenerateCooridor currentCooridor = Instantiate(cooridorPrefab, new Vector3Int(0, 0, 0), Quaternion.identity).GetComponent<GenerateCooridor>();
        currentCooridor.topWall = topWall; 
        currentCooridor.bottomWall = bottomWall; 
        currentCooridor.leftWall = leftWall; 
        currentCooridor.rightWall = rightWall;
        currentCooridor.topLeftCorner = topLeftCorner; 
        currentCooridor.topRightCorner = topRightCorner; 
        currentCooridor.bottomLeftCorner = bottomLeftCorner;
        currentCooridor.bottomRightCorner = bottomRightCorner; 
        currentCooridor.wallsTilemap = wallsTileMap; 
        currentCooridor.cardinalDirections = cardinalDirections;
        currentCooridor.start = currentExit;
        cooridorList.Add(currentCooridor);

        for (int i = 0; i < currentCooridor.cooridorPos.Count; i++)
        {
            tileLocations.Add(currentCooridor.cooridorPos[i]);
        }
    }
    GenerateWall InstantiateRoom(int posX, int posY, int sizeX, int sizeY)
    {
        GenerateWall currentRoom = Instantiate(roomPrefab, new Vector3Int(0, 0, 0), Quaternion.identity).GetComponent<GenerateWall>();

        currentRoom.topWall = topWall; 
        currentRoom.bottomWall = bottomWall; 
        currentRoom.leftWall = leftWall; 
        currentRoom.rightWall = rightWall;
        currentRoom.topLeftCorner = topLeftCorner;
        currentRoom.topRightCorner = topRightCorner; 
        currentRoom.bottomLeftCorner = bottomLeftCorner;    //Initialises room object with values
        currentRoom.bottomRightCorner = bottomRightCorner; 
        currentRoom.wallsTilemap = wallsTileMap; 
        currentRoom.posX = posX;
        currentRoom.posY = posY; 
        currentRoom.sizeX = sizeX; 
        currentRoom.sizeY = sizeY;
        currentRoom.grassTilemap = grassTileMap; 
        currentRoom.gridOffset = 1;
        currentRoom.CreateRoom();
        roomList.Add(currentRoom);
        tileLocations.Add(currentRoom.wallList);
        Destroy(currentRoom);
        return currentRoom;
    }

    Vector3Int PlaceRoomPos(Direction currentDirection, int posX, int posY, int sizeX, int sizeY)
    {
        Vector3Int placementLocation = new Vector3Int();
        int wallX = UnityEngine.Random.Range(((-sizeX + 1) / 2) + posX, ((sizeX - 1) / 2) + posX);
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
        return placementLocation;
    }
}
