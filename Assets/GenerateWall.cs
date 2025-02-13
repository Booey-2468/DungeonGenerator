using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWall : MonoBehaviour
{
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject bottomWall;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject topLeftCorner;
    [SerializeField] private GameObject topRightCorner;
    [SerializeField] private GameObject bottomLeftCorner;
    [SerializeField] private GameObject bottomRightCorner;
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    [SerializeField] private int gridOffset = 1;
    private GameObject currentSelection;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = -1; x <= sizeX; x++)
        {
            for (int y = -1; y <= sizeY; y++)
            {
                if (x == -1)
                {
                    if (y == -1)
                    {
                        currentSelection = bottomLeftCorner;
                    }
                    else if (y == sizeY)
                    {
                        currentSelection = topLeftCorner;
                    }
                    else
                    {
                        currentSelection = leftWall;
                    }
                }
                else if (x == sizeX)
                {
                    if (y == -1)
                    {
                        currentSelection = bottomRightCorner;
                    }
                    else if (y == sizeY)
                    {
                        currentSelection = topRightCorner;
                    }
                    else
                    {
                        currentSelection = rightWall;
                    }
                }
                else
                {
                    if (y == -1)
                    {
                        currentSelection = bottomWall;
                    }
                    else if (y == sizeY)
                    {
                        currentSelection = topWall;
                    }
                }

                Vector2 pos = new Vector2(x * gridOffset, y * gridOffset);
                GameObject tile = Instantiate(topWall, pos, Quaternion.identity);
                tile.transform.SetParent(this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
