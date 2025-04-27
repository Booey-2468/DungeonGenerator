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
    [SerializeField] private Sprite rocks;
    private Sprite groundSprite;    // Sprites for grass area

    [SerializeField] private GameObject thumperPrefab;
    [SerializeField] private GameObject axeGirlPrefab;
    [SerializeField] private GameObject goliatahasPrefab; // prefabs for enemies 

    [SerializeField] private GameObject heartPrefab;

    [SerializeField] private GameObject winConditionPrefab;

    [HideInInspector]public GenerateWall walls;
    private int gridOffset = 1;
    private int enemyCount = 0;
    private int maxEnemyCount = 10;
    private int heartCount = 0;
    private int maxHeartCount = 5;
    private bool shouldGoliathasSpawn = false;
    private List<Vector3Int> tilePos = new List<Vector3Int>();
    private List<List<int>> coordVector;
    [HideInInspector] public bool spawnEnemies = true;
    [HideInInspector] public bool lastRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CreateBackdrop()
    {
        enemyCount = 0;

        shouldGoliathasSpawn = lastRoom;

        maxEnemyCount = UnityEngine.Random.Range(2, 15);

        maxHeartCount = UnityEngine.Random.Range(0, 6);

        coordVector = new List<List<int>>();

        for (int x = walls.posX; x < walls.posX + walls.sizeX; x++)
        {
            for (int y = walls.posY; y < walls.posY + walls.sizeY; y++)
            {
                ChooseGrassSprite();

                bool shouldEnemyExist = (UnityEngine.Random.Range(1, 3) == 1 && (x > walls.posX && x < walls.posX + walls.sizeX) && (y > walls.posY && y < walls.posY + walls.sizeY));
                if(enemyCount <= maxEnemyCount && shouldEnemyExist && spawnEnemies)
                {
                    ChooseEnemy();
                }

                if (shouldEnemyExist && spawnEnemies)
                {
                    SpawnHearts();
                }
                SpawnWinCondition();
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
    /// <summary>
    /// Spawns a heart pickup to gain health with.
    /// </summary>
    /// <param name="spawnX"> Used for the x spawn location of the heart pickup</param>
    /// <param name="spawnY"> Used for the y spawn location of the heart pickup</param>
    private void SpawnHearts()
    {
        int spawnX = UnityEngine.Random.Range(walls.posX + 1, walls.posX + walls.sizeX);
        int spawnY = UnityEngine.Random.Range(walls.posY + 1, walls.posY + walls.sizeY);

        if (!SpaceFilled(spawnX, spawnY) && heartCount < maxHeartCount && heartPrefab != null)  //Ensures that the space isnt filled there isn't too many hearts and the heart prefab isnt null
        {
            coordVector.Add(new List<int> {spawnX, spawnY});    // Adds coordinates to prefab and ups the heart count the spawns the heart
            heartCount++;
            Vector3Int heartSpawnPos = new Vector3Int(spawnX, spawnY);
            Instantiate(heartPrefab, heartSpawnPos, UnityEngine.Quaternion.identity);
        }
    }

    void SpawnWinCondition()
    {
        int spawnX = UnityEngine.Random.Range(walls.posX + 1, walls.posX + walls.sizeX);
        int spawnY = UnityEngine.Random.Range(walls.posY + 1, walls.posY + walls.sizeY);
        if (lastRoom && !SpaceFilled(spawnX, spawnY) && winConditionPrefab != null)
        {
            coordVector.Add(new List<int> { spawnX, spawnY });    // Adds coordinates to prefab and spawns win condition and resets lastRoom
            Vector3Int crownSpawn = new Vector3Int(spawnX, spawnY);

            Instantiate(winConditionPrefab, crownSpawn, UnityEngine.Quaternion.identity);
            lastRoom = false;

        }

        spawnX = UnityEngine.Random.Range(walls.posX + 1, walls.posX + walls.sizeX);
        spawnY = UnityEngine.Random.Range(walls.posY + 1, walls.posY + walls.sizeY);

        if (shouldGoliathasSpawn && !SpaceFilled(spawnX, spawnY) && goliatahasPrefab != null)
        {
            coordVector.Add(new List<int> { spawnX, spawnY });    // Adds coordinates to prefab and spawns win goliathas
            Vector3Int goliathasSpawn = new Vector3Int(spawnX, spawnY);

            Instantiate(goliatahasPrefab, goliathasSpawn, UnityEngine.Quaternion.identity);

            shouldGoliathasSpawn = false;
        }
    }


    private void ChooseEnemy()
    {
        int spawnX = UnityEngine.Random.Range(walls.posX + 1, walls.posX + walls.sizeX);
        int spawnY = UnityEngine.Random.Range(walls.posY + 1, walls.posY + walls.sizeY);
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
        int randomGrass = UnityEngine.Random.Range(1, 101);

        if(randomGrass < 41)
        {
            groundSprite = grass;
        }
        else if(randomGrass < 90)
        {
            groundSprite = grass2;
        }
        else if (randomGrass > 89)
        {
            groundSprite = rocks;
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
