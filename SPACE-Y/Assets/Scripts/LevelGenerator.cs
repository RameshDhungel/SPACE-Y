using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject[] standardAsteroids = new GameObject[] { };
    public GameObject starterAsteroid;
    public GameObject bossAsteroid;

    public float spawnWidth;
    public float spawnAmount;

    bool[][] pointHasAsteroid = new bool[8][];
    Vector2[][] positions = new Vector2[8][];
    int xCoordinate;
    int yCoordinate;


    void Start()
    {
        GenerateBelt();
    }


    void GenerateBelt()
    {
        
    }
}
