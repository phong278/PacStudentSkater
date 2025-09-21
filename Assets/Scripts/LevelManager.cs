using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Tile Prefabs")]
    public GameObject outsideCorner;
    public GameObject outsideWall;
    public GameObject insideCorner;
    public GameObject insideWall;
    public GameObject tJunction;
    public GameObject standardPellet;
    public GameObject powerPellet;
    public GameObject ghostExit;
    public GameObject empty;

    // Map (0,0 = top-left)
    int[,] levelMap = new int[,]
    {
         {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
         {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
         {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
         {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
         {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
         {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
         {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
         {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
         {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
         {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
         {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
         {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
         {0,0,0,0,0,2,5,4,4,0,3,4,4,8},
         {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
         {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int row = 0; row < levelMap.GetLength(0); row++)
        {
            for (int col = 0; col < levelMap.GetLength(1); col++)
            {
                int tileCode = levelMap[row, col];

                GameObject prefab = GetPrefabForCode(tileCode, out Quaternion rotation);

                if (prefab != null)
                {
                    Vector3 pos = new Vector3(col, -row, 0);
                    pos += new Vector3(-levelMap.GetLength(1) / 2, levelMap.GetLength(0) / 2, 0);
                    Instantiate(prefab, pos, rotation, transform);
                }
            }
        }
    }

    GameObject GetPrefabForCode(int code, out Quaternion rotation)
    {
        rotation = Quaternion.identity;

        switch (code)
        {
            case 1: return outsideCorner;
            case 2: return outsideWall;
            case 3: return insideCorner;
            case 4: return insideWall;
            case 5: return standardPellet;
            case 6: return powerPellet;
            case 7: return tJunction;
            case 8: return ghostExit;
            case 0: return null; // empty space
            default: return null;
        }
    }

}

