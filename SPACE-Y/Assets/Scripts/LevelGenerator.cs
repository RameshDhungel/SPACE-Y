using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject[] standardAsteroids = new GameObject[] { }; //holds regular asteroid
    public GameObject starterAsteroid; 
    public GameObject bossAsteroid; 

    public float spawnWidth; //distance between asteroid spawn points
    public float spawnHeight; //distance between asteroid spawn points vertically
    public float standardAsteroidSpawnAmount; //amount of asteroids not includoing starter or boss

    public int rows = 8; //rows of spawn points 
    public int columns = 8; //columns of spawn points

    List<List<bool>> pointHasAsteroid; //true indicates point already has an asteroid
    List<List<Vector2>> positions; //holds asteroid spawn positions

    //for selecting spawn positions
    int xCoordinate; 
    int yCoordinate;
    System.Random rand;


    void Start()
    {
        //initialize lists
        pointHasAsteroid = new List<List<bool>>();
        positions = new List<List<Vector2>>();

        rand = new System.Random();

        GenerateBelt();
    }


    void GenerateBelt()
    {
        //set every position to false for has asteroid
        for (int r = 0; r < rows; r++)
        {
            pointHasAsteroid.Add(new List<bool>());

            for (int c = 0; c < columns; c++)
            {
                pointHasAsteroid[r].Add(false);
            }
        }


        //make positions
        Vector2 nextposition = new Vector2(0, 0);

        for (int r = 0; r < rows; r++)
        {
            positions.Add(new List<Vector2>());

            for (int c = 0; c < columns; c++)
            {
                positions[r].Add(nextposition);
                nextposition.x += spawnWidth;
            }
            nextposition.x = 0;
            nextposition.y += spawnHeight;

        }


        //spawn starter asteroid
        yCoordinate = rand.Next(rows);
        xCoordinate = rand.Next(columns);
        Instantiate(starterAsteroid, positions[yCoordinate][xCoordinate], Quaternion.identity);
        pointHasAsteroid[yCoordinate][xCoordinate] = true; //make sure to keep track which positions have asteroid


        //spawn boss asteroid
        bool hasSpawnedBossAsteroid = false;
        while (!hasSpawnedBossAsteroid)
        {
            yCoordinate = rand.Next(rows);
            xCoordinate = rand.Next(columns);

            if (pointHasAsteroid[yCoordinate][xCoordinate] == false)
            {
                Instantiate(bossAsteroid, positions[yCoordinate][xCoordinate], Quaternion.identity);
                hasSpawnedBossAsteroid = true;
                pointHasAsteroid[yCoordinate][xCoordinate] = true; //make sure to keep track which positions have asteroid
            }
        }


        //spawn standard asteroids
        int amountSpawned = 0;
        int whichAsteroid;
        while (amountSpawned < standardAsteroidSpawnAmount)
        {
            yCoordinate = rand.Next(rows);
            xCoordinate = rand.Next(columns);
            whichAsteroid = rand.Next(standardAsteroids.Length);

            if (pointHasAsteroid[yCoordinate][xCoordinate] == false)
            {
                Instantiate(standardAsteroids[whichAsteroid], positions[yCoordinate][xCoordinate], Quaternion.identity);
                amountSpawned++;
                pointHasAsteroid[yCoordinate][xCoordinate] = true; //make sure to keep track which positions have asteroid
            }
        }

    }
}
