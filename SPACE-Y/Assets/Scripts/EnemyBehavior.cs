using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float enemyShootRange = 6f;
    float waitTime = 3.5f;
    float timeCounter = 0f;
    public GameObject bulletPrefab;
    private Transform player;
    GameObject previousBullet = null;
    private static int cloneCount = 4;
    Vector2 orbitDirection = new Vector2(0, 0);
    float alpha = 0.01f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        Vector2 vectorMag = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        float mag = Mathf.Sqrt(Mathf.Pow(vectorMag.x, 2) + Mathf.Pow(vectorMag.y, 2));

        // Shoot(mag,enemyShootRange);
        //SuicideBomb(mag);
        Shoot(mag,10);
        //Rotate(5, 5);
    }

    public void Shoot(float mag, float enemyShootRange)
    {
        if (mag < enemyShootRange)
        {
             if (timeCounter < Time.time)
             {
                if (previousBullet != null)
                {
                    Destroy(previousBullet);
                }
                GameObject bullet = Instantiate(bulletPrefab, this.gameObject.GetComponent<Transform>());
                bullet.GetComponent<Weapon>().shoot(player);
                /*
                Vector2 direction = new Vector2(player.transform.position - transform.position).Normalize;
                Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y); ;
                direction.Normalize();

                // float rotateAmount = Vector3.Cross(direction, enemyPos).z;
                bullet.GetComponent<Transform>().rotation = GameObject.Find("EnemySprite").GetComponent<Transform>().rotation;       
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2();
               
                */
                timeCounter = Time.time + waitTime;            
                previousBullet = bullet;
             }
        }
    }

    public void SuicideBomb(float mag)
    {
        if(mag <= 1){
            Destroy(this.gameObject);
        }
    }

    public void Splitter(float mag, float shootingRange)
    {
        if (cloneCount >= 0 && mag <= shootingRange )
        {
           GameObject clone = Instantiate(this.gameObject);
           clone.transform.position = new Vector2(transform.position.y + Random.Range(-2,2), transform.position.y + Random.Range(-2, 2));
           cloneCount-= 1;
        }
    }
    public void Rotate(float width, float height)
    {
        float X;
        float Y;
        X = orbitDirection.x + (width * Mathf.Sin(alpha));
        Y = orbitDirection.y + (width * Mathf.Cos(alpha));
        transform.position = new Vector2(X, Y);

        /*
        orbitDirection.x = orbitDirection.x + (width * Mathf.Sin(alpha));
        orbitDirection.y = orbitDirection.y + (width * Mathf.Cos(alpha));
        transform.position = orbitDirection;
        */

    }
}
