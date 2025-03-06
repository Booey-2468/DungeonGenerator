using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update

    int posX = -5, posY = -5, sizeX = 10, sizeY = 10;
    void Start()
    {
        GenerateWall initialRoom = new (wallsTileMap, topWall, bottomWall, leftWall, rightWall, topLeftCorner, topRightCorner, bottomLeftCorner, bottomRightCorner, posX, posY, sizeX, sizeY, 1);
        GenerateExit initialExit = new (wallsTileMap, topWall, bottomWall, leftWall, rightWall, topLeftCorner, topRightCorner, bottomLeftCorner, bottomRightCorner, initialRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
