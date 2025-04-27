using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private Sprite stoppingSprite;
    [SerializeField] private Tilemap wallsTileMap;
    [SerializeField] private Tilemap grassTileMap;

    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private GameObject exitPrefab;
    [SerializeField] private GameObject cooridorPrefab;

    private List<List<Vector3Int>> tileLocations = new List<List<Vector3Int>>();
    private List<GenerateWall> roomList = new List<GenerateWall>();
    private List<GenerateWall> totalRoomList = new List<GenerateWall>();
    private List<GenerateExit> exitList = new List<GenerateExit>();
    private List<GenerateCooridor> cooridorList = new List<GenerateCooridor>();
    private float timer;
    private float timerDuration = 6;
    private int maxRooms = 10;

    private List<Vector3Int> cardinalDirections = new List<Vector3Int> 
    { 
        new Vector3Int(-1, 0 ,0),
        new Vector3Int(1, 0 ,0),
        new Vector3Int(0, 1 ,0),
        new Vector3Int (0, -1 ,0),
    };
    // Start is called before the first frame update

    private int posX = -5, posY = -5, sizeX = 10, sizeY = 10;
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("DataStore") != null)
        {
            maxRooms = GameObject.FindGameObjectWithTag("DataStore").GetComponent<DungeonData>().maxRooms;
        }
        else
        {
            maxRooms = 10;
        }

        InstantiateRoom(posX, posY, sizeX, sizeY, false);
        timer = timerDuration;
    }
    // Update is called once per frame
    void Update()
    {

        if (totalRoomList.Count < maxRooms)
        {
            CreateBranchingExits();
            CreateCooridorFromExit();
            CreateRoomFromCooridor();

        }
        else if(totalRoomList.Count >= maxRooms)
        {
                for (int i = cooridorList.Count - 1; i >= 0; i--)
                {
                    if (!cooridorList[i].cooridorBlocked)
                    {
                        cooridorList[i].CreateCooridor();
                        cooridorList[i].BlockCorridor(stoppingSprite);
                        Destroy(cooridorList[i].gameObject);
                        cooridorList.Remove(cooridorList[i]);
                    }
                }
        }
        else if (totalRoomList.Count < 2 && cooridorList.Count == 0 )
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else if(totalRoomList.Count <= 3 && timer <= 0)
        {
            SceneManager.LoadScene("GameStart");
        }
    }
    void CreateBranchingExits()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            CreateAllExits(roomList[i]);
            Destroy(roomList[i].gameObject);
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
                Destroy(exitList[i].gameObject);
                exitList.Remove(exitList[i]);
            }

        }
    }

    /// <summary>
    /// This creates a room and an opening to the room it also checks if the room can be spawned in the first place and carries on if it can't
    /// </summary>
    void CreateRoomFromCooridor()
    {
        for(int i = cooridorList.Count - 1; i >= 0; i--)
        {
            if (cooridorList[i].cooridorBlocked)
            {
                Destroy(cooridorList[i].gameObject);
                cooridorList.RemoveAt(i);
                continue;
            }
        }
        for (int i = 0; i < cooridorList.Count; i++)
        {
            if (cooridorList[i].generateRoom)
            {
                posX = (cooridorList[i].currentPos + cardinalDirections[(int)cooridorList[i].currentDirection]).x;
                posY = (cooridorList[i].currentPos + cardinalDirections[(int)cooridorList[i].currentDirection]).y;
                sizeX = UnityEngine.Random.Range(6, 50);
                sizeY = UnityEngine.Random.Range(6, 50);

                Vector3Int roomSpawnPos = PlaceRoomPos(cooridorList[i].currentDirection, posX, posY, sizeX, sizeY);
                posX = roomSpawnPos.x;
                posY = roomSpawnPos.y;

                for (int x = posX - 1; x <= posX + sizeX; x++)
                {
                    for(int y = posY - 1; y <= posY + sizeY; y++)
                    {
                        if(wallsTileMap.GetSprite(new Vector3Int(x, y, 0)) != null)
                        {
                            cooridorList[i].generateRoom = false;
                            break;
                        }
                    }
                    if (!cooridorList[i].generateRoom) { break; }
                }

                if (cooridorList[i].generateRoom && cooridorList[i].cooridorPos.LastOrDefault() != null)
                {
                    GenerateWall spawnedRoom = InstantiateRoom(posX, posY, sizeX, sizeY);

                    Direction openingDirection;
                    if (cooridorList[i].currentDirection == Direction.left || cooridorList[i].currentDirection == Direction.up) { openingDirection = cooridorList[i].currentDirection + 1; }
                    else { openingDirection = cooridorList[i].currentDirection - 1; }

                    InstantiateExit(spawnedRoom, true, true, cooridorList[i].cooridorPos.LastOrDefault()[1].x, cooridorList[i].cooridorPos.LastOrDefault()[1].y, openingDirection);
                    Destroy(cooridorList[i].gameObject);
                    cooridorList.Remove(cooridorList[i]);

                }
                else
                {
                    cooridorList[i].generateRoom = false;
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentRoom">The room from which exits can be created</param>
    void CreateAllExits(GenerateWall currentRoom)
    {
        int numOfExits = UnityEngine.Random.Range(1, 5);
        for (int i = 0; i < numOfExits; i++)
        {
            InstantiateExit(currentRoom);
        }
    }
    GenerateExit InstantiateExit(GenerateWall currentRoom, bool isOpening = false, bool customExit = false, int x = 0, int y = 0, Direction exitDirection = Direction.up)
    {
        GenerateExit currentExit = Instantiate(exitPrefab, new Vector3Int(0, 0, 0), Quaternion.identity).GetComponent<GenerateExit>();
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
        return currentExit;

    }
    /// <summary>
    /// Initialises and Starts the creation of the cooridor
    /// </summary>
    /// <param name="currentExit"></param>
    void StartCooridor(GenerateExit currentExit)
    {
        GenerateCooridor currentCooridor = Instantiate(cooridorPrefab, new Vector3Int(0, 0, 0), Quaternion.identity).GetComponent<GenerateCooridor>();
        currentCooridor.wallsTilemap = wallsTileMap; 
        currentCooridor.cardinalDirections = cardinalDirections;
        currentCooridor.start = currentExit;
        currentCooridor.CreateCooridor();
        cooridorList.Add(currentCooridor);

        for (int i = 0; i < currentCooridor.cooridorPos.Count; i++)
        {
            tileLocations.Add(currentCooridor.cooridorPos[i]);
        }
    }
    GenerateWall InstantiateRoom(int posX, int posY, int sizeX, int sizeY, bool spawnEnemies = true)
    {
        GenerateWall currentRoom = Instantiate(roomPrefab, new Vector3Int(0, 0, 0), Quaternion.identity).GetComponent<GenerateWall>();

        currentRoom.wallsTilemap = wallsTileMap; 
        currentRoom.posX = posX;
        currentRoom.posY = posY; 
        currentRoom.sizeX = sizeX; 
        currentRoom.sizeY = sizeY;
        currentRoom.grassTilemap = grassTileMap; 
        currentRoom.spawnEnemies = spawnEnemies;
        currentRoom.lastRoom = (totalRoomList.Count + 1 >= maxRooms);
        currentRoom.CreateRoom();

        roomList.Add(currentRoom);
        totalRoomList.Add(currentRoom);
        tileLocations.Add(currentRoom.wallList);
        return currentRoom;
    }

    Vector3Int PlaceRoomPos(Direction currentDirection, int posX, int posY, int sizeX, int sizeY)
    {
        Vector3Int placementLocation = new Vector3Int();
        int wallX = UnityEngine.Random.Range(posX - sizeX + 2, posX - 1);
        int wallY = UnityEngine.Random.Range(posY - sizeY + 2, posY - 1);
        switch (currentDirection)
        {
            case Direction.left:
                placementLocation = new Vector3Int(posX - sizeX, wallY, 0);
                break;
            case Direction.right:
                placementLocation = new Vector3Int( posX, wallY, 0);
                break;
            case Direction.up:
                placementLocation = new Vector3Int( wallX, posY, 0);

                break;
            case Direction.down:
                placementLocation = new Vector3Int( wallX , posY - sizeY, 0);
                break;
        }
        return placementLocation;
    }
}
