using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public bool generationComplete = false;

    public GameObject[] standardAsteroids = new GameObject[] { }; //holds regular asteroid
    public GameObject starterAsteroidPrefab; 
    public GameObject bossAsteroidPrefab;
    public GameObject[] enemies = new GameObject[] { };
    public GameObject spawnAsteroid;

    public float spawnWidth; //distance between asteroid spawn points
    public float spawnHeight; //distance between asteroid spawn points vertically
    public float standardAsteroidSpawnAmount; //amount of asteroids not includoing starter or boss
    public int enemyCount;

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
        GenerateEnemies();
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
        spawnAsteroid = Instantiate(starterAsteroidPrefab, positions[yCoordinate][xCoordinate], Quaternion.identity);
        pointHasAsteroid[yCoordinate][xCoordinate] = true; //make sure to keep track which positions have asteroid


        //spawn boss asteroid
        bool hasSpawnedBossAsteroid = false;
        while (!hasSpawnedBossAsteroid)
        {
            yCoordinate = rand.Next(rows);
            xCoordinate = rand.Next(columns);

            if (pointHasAsteroid[yCoordinate][xCoordinate] == false)
            {
                Instantiate(bossAsteroidPrefab, positions[yCoordinate][xCoordinate], Quaternion.identity);
                hasSpawnedBossAsteroid = true;
                pointHasAsteroid[yCoordinate][xCoordinate] = true; //make sure to keep track which positions have asteroid
            }
        }


        //spawn standard asteroids
        int amountSpawned = 0;
        int whichAsteroid;
        GameObject currentAsteroid;

        while (amountSpawned < standardAsteroidSpawnAmount)
        {
            yCoordinate = rand.Next(rows);
            xCoordinate = rand.Next(columns);
            whichAsteroid = rand.Next(standardAsteroids.Length);

            if (pointHasAsteroid[yCoordinate][xCoordinate] == false)
            {
                currentAsteroid = Instantiate(standardAsteroids[whichAsteroid], positions[yCoordinate][xCoordinate], Quaternion.identity);
                //currentAsteroid.GetComponent<Transform>().localScale *= 10;
                currentAsteroid.AddComponent<Gravity>();
                amountSpawned++; 
                pointHasAsteroid[yCoordinate][xCoordinate] = true; //make sure to keep track which positions have asteroid
            }
        }

        generationComplete = true;

    }

    void GenerateEnemies()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        List<bool> pointHasEnemy = new List<bool>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            pointHasEnemy.Add(false);
        }

        int spawnedAmount = 0;
        int selectedSpawnPoint;
        int selectedEnemy;

        while (spawnedAmount < enemyCount)
        {
            selectedSpawnPoint = rand.Next(spawnPoints.Length);
            selectedEnemy = rand.Next(enemies.Length);

            if (!pointHasEnemy[selectedSpawnPoint])
            {
                Vector3 point = new Vector3(spawnPoints[selectedSpawnPoint].GetComponent<Transform>().position.x, spawnPoints[selectedSpawnPoint].GetComponent<Transform>().position.y, 0f);
                Instantiate(enemies[selectedEnemy], point, Quaternion.identity);           
                spawnedAmount++;
                pointHasEnemy[selectedSpawnPoint] = true;
            }
        }

    }

}
