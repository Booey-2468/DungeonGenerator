using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;



public class GenerateGrass : MonoBehaviour
{
    public Tilemap backgroundTileMap;
    [SerializeField] private Sprite grass;
    [SerializeField] private Sprite grass2;
    [SerializeField] private Sprite grass3;
    private Sprite groundSprite;    // Sprites for grass area

    [SerializeField] private GameObject thumperPrefab;
    [SerializeField] private GameObject axeGirlPrefab;
    [SerializeField] private GameObject goliatahasPrefab; // prefabs for enemies 

    [HideInInspector]public GenerateWall walls;
    private int gridOffset = 1;
    private int enemyCount = 0;
    private int maxEnemyCount = 10;
    private List<Vector3Int> tilePos = new List<Vector3Int>();
    private List<List<int>> coordVector;
    [HideInInspector] public bool spawnEnemies = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CreateBackdrop()
    {
        enemyCount = 0;
        maxEnemyCount = UnityEngine.Random.Range(2, 15);
        coordVector = new List<List<int>>();
        for (int x = walls.posX; x < walls.posX + walls.sizeX; x++)
        {
            for (int y = walls.posY; y < walls.posY + walls.sizeY; y++)
            {
                ChooseGrassSprite();
                bool shouldEnemyExist = (UnityEngine.Random.Range(1, 3) == 1 && (x > walls.posX && x < walls.posX + walls.sizeX) && (y > walls.posY && y < walls.posY + walls.sizeY));
                if(enemyCount <= maxEnemyCount && shouldEnemyExist && spawnEnemies)
                {
                    int randX = UnityEngine.Random.Range(walls.posX + 1, walls.posX + walls.sizeX);
                    int randY = UnityEngine.Random.Range(walls.posY + 1, walls.posY + walls.sizeY);
                    ChooseEnemy(randX, randY);
                }

                Vector3Int pos = new Vector3Int(x * gridOffset, y * gridOffset, 0);
                backgroundTileMap.SetTile(pos, new Tile() { sprite = groundSprite });
                tilePos.Add(pos);
            }
        }
    }

    private bool SpaceFilled(int spawnX, int spawnY)
    {
        foreach(List<int> i in coordVector)
        {
            if (i[0] == spawnX && i[1] == spawnY) 
                return true;
        }
        return false;
    }
    private void ChooseEnemy(int spawnX, int spawnY)
    {
        int randomEnemy = UnityEngine.Random.Range(1, 100);

        Vector3Int randomEnemyPos = new Vector3Int(spawnX, spawnY, 0);

        if (SpaceFilled(spawnX, spawnY)) { return; }

        coordVector.Add(new List<int> { spawnX, spawnY });
        enemyCount++;

        if (randomEnemy > 98)
        {
            SpawnEnemy(goliatahasPrefab, randomEnemyPos);
        }
        else if(randomEnemy < 50)
        {
            SpawnEnemy(thumperPrefab, randomEnemyPos);
        }
        else
        {
            SpawnEnemy(axeGirlPrefab, randomEnemyPos);
        }
    }

    private void ChooseGrassSprite()
    {
        int randomGrass = UnityEngine.Random.Range(1, 3);
        switch (randomGrass)
        {
            case 1:
                groundSprite = grass;
                break;
            case 2:
                groundSprite = grass2;
                break;
            case 3:
                groundSprite = grass3;
                break;
        }
    }
    private GameObject SpawnEnemy(GameObject chosenPrefab, Vector3Int spawnLocation)
    {
        GameObject enemy = Instantiate(chosenPrefab, spawnLocation, UnityEngine.Quaternion.identity);
        EnemyMovement enemyScript = enemy.GetComponent<EnemyMovement>();
        enemyScript.posX = walls.posX;
        enemyScript.posY = walls.posY;
        enemyScript.sizeX = walls.sizeX;
        enemyScript.sizeY = walls.sizeY;
        enemyScript.hasBeenCalled = true;

        return enemy;
    }
}
