using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update

    int posX = -5, posY = -5, sizeX = 10, sizeY = 10;
    void Start()
    {
        List<Sprite> spriteList = new List<Sprite>() { topWall, bottomWall, leftWall, rightWall, topLeftCorner, topRightCorner, bottomLeftCorner, bottomRightCorner};
        GenerateWall initialRoom = Instantiate(roomPrefab, new Vector3Int(0,0,0), Quaternion.identity).GetComponent<GenerateWall>();
        GenerateExit initialExit = Instantiate(exitPrefab, new Vector3Int(0, 0, 0), Quaternion.identity).GetComponent<GenerateExit>();


        initialRoom.topWall = topWall; initialRoom.bottomWall = bottomWall; initialRoom.leftWall = leftWall; initialRoom.rightWall = rightWall;
        initialRoom.topLeftCorner = topLeftCorner; initialRoom.topRightCorner = topRightCorner; initialRoom.bottomLeftCorner = bottomLeftCorner; 
        initialRoom.bottomRightCorner = bottomRightCorner; initialRoom.wallsTilemap = wallsTileMap; initialRoom.posX = posX; 
        initialRoom.posY = posY; initialRoom.sizeX = sizeX; initialRoom.sizeY = sizeY;
        initialRoom.grassTilemap = grassTileMap; initialRoom.gridOffset = 1; initialRoom.valuesAssigned = true;

        initialExit.topLeftCorner = topLeftCorner; initialExit.topRightCorner = topRightCorner; initialExit.bottomLeftCorner = bottomLeftCorner;
        initialExit.bottomRightCorner = bottomRightCorner; initialExit.walls = initialRoom; initialExit.wallsTilemap = wallsTileMap; initialExit.valuesAssigned = true;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
