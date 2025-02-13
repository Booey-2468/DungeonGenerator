using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrass : MonoBehaviour
{
    [SerializeField] private GameObject grass;
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    [SerializeField] private int gridOffset = 1;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                Vector2 pos = new Vector2(x * gridOffset, y * gridOffset);
                GameObject tile = Instantiate(grass, pos, Quaternion.identity);
                tile.transform.SetParent(this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
