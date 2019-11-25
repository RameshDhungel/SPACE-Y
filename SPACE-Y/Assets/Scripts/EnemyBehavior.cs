using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject previousBullet = null;
    private GameObject[] bulletArray;
    private Transform player;
    private Vector2 orbitDirection = new Vector2(0, 0);

    private float health;
    private float enemyShootRange = 10f;
    private float waitTime = 3.5f;
    private float timeCounter = 0f;
    private float alpha = 0.01f;

    private int damage;

    private static int cloneCount = 4;

    private bool isBomber = false;

    private void Awake()
    {
        string name = this.gameObject.name;
        if (name.Contains("Shooter"))
        {
            health = Random.Range(60, 80);
            damage = Random.Range(10, 14);
            enemyShootRange = Random.Range(8, 14);
            waitTime = Random.Range(2, 3.5f);
        }
        else if (name.Contains("Rotater"))
        {
            health = Random.Range(50, 60);
            damage = Random.Range(15, 17);
            enemyShootRange = Random.Range(8, 14);
            waitTime = Random.Range(1, 3.5f);       
        }else if (name.Contains("Bomber"))
        {
            damage = Random.Range(20, 25);
            health = Random.Range(20, 30);
            isBomber = true;
        }
        else if (name.Contains("boss"))
        {
            damage = Random.Range(35, 45);
            health = 400;
            enemyShootRange = Random.Range(10, 14);
            waitTime = Random.Range(1, 3.5f);
        }

    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        bulletArray = GameObject.FindGameObjectsWithTag("EnemyWeapon"); // Stores all the player bullet in a array
        if (bulletArray.Length > 10) // This checks if there is more than 15 bullet in the Array
        {
            Destroy(bulletArray[0]); // This will delete the oldest bullet so we dont have shit tone of unneeded bullet prefab in hierarchy
        }
    }
    private void FixedUpdate()
    {
        
        Vector2 vectorMag = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        float mag = Mathf.Sqrt(Mathf.Pow(vectorMag.x, 2) + Mathf.Pow(vectorMag.y, 2));

        if (isBomber)
        {
            SuicideBomb(mag);
        }
        else
        {
         Shoot(mag,enemyShootRange);
        }

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
       
    }

    public void Shoot(float mag, float enemyShootRange)
    {
        if (mag < enemyShootRange)
        {
             if (timeCounter < Time.time)
             {       
                GameObject bullet = Instantiate(bulletPrefab, new Vector3(this.gameObject.GetComponent<Transform>().position.x + 2, this.gameObject.GetComponent<Transform>().position.y, this.gameObject.GetComponent<Transform>().position.z), Quaternion.identity);
                bullet.GetComponent<Weapon>().Enemyshoot(player);
                bullet.tag = "EnemyWeapon";
                bullet.GetComponent<Weapon>().SetEnemyObject(this.gameObject); //Finds the weapon script on bullet and sets the enemy GameObject to this enemy Object
                timeCounter = Time.time + waitTime;            
                
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "PlayerWeapon")// Checks if the bullet if from player
        {
            int damage = player.GetComponent<PlayerHealth>().Dealdamage(); // Gets the damage amount from player script
            TakeDamage(damage); 
           
        }
    }
    public int DealDamage()
    {
        return damage;
    }
    public void TakeDamage(int damage)
    {
        health -= damage; // Deducts Damage
    }

}
