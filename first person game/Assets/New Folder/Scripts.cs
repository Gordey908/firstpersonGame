using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts : MonoBehaviour
{
    [SerializeField]
    private GameObject cubePrefab;

    private const int PYRAMID_COUNT = 5;
    private const int PYRAMID_HEIGHT = 5;
    private const int PYRAMID_BASE = PYRAMID_HEIGHT * 2 - 1;

    void Start()
    {
        for (int x = 0; x < PYRAMID_COUNT; x++)
        {
            for (int z = 0; z < PYRAMID_COUNT; z++)
            {
                CreatePyramid(new Vector3(x * PYRAMID_BASE, 0f, z * PYRAMID_BASE));
            }
        }
    }

    private void CreatePyramid(Vector3 pos)
    { 
        int offsetX = 0, offsetZ = 0;

        for (int y = 0; y < PYRAMID_HEIGHT; y++)
        {
            for (int x = (int)pos.x + offsetX; x < pos.x + PYRAMID_BASE - offsetX; x++)
            {
                for (int z = (int)pos.z + offsetZ; z < pos.z + PYRAMID_BASE - offsetZ; z++)
                {
                    Instantiate(cubePrefab, new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), Quaternion.identity);
                }
            }
        }

        offsetX++;
        offsetZ++;
    }
}
