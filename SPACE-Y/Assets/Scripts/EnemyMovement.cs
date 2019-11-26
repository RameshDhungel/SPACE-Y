using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D enemyRigidbody;
    private Vector2 moveDirection; // Direction the enemy will move
    private Vector2 orbitDirection = new Vector2(0, 0);

    private float moveSpeed; // How fast the enemy will move
    private float rotateAngle; // How much to rotate enemy to face player
    private float alpha = 1f;
    public float width = 0; 
    public float height = 0;
    public float visibleRange; // How far enemy can see
    public float stopRange; // The min range between player and enemy before it stops moving

    private bool isRotater = false; 

    private void Awake()
    {
        string name = this.gameObject.name; //Name of the enemy
        if (name.Contains("Shooter")) // sets attributes for different enemy
        {
            moveSpeed = Random.Range(5, 7);
            visibleRange = Random.Range(10, 14);
            stopRange = Random.Range(3, 7);

        }else if (name.Contains("Bomber"))
        {
            moveSpeed = 10f;
            visibleRange = Random.Range(8, 10);
            stopRange = 0f;
        }else if (name.Contains("Rotater"))
        {
            moveSpeed = Random.Range(2, 4);
            width = Random.Range(20, 30);
            isRotater = true;
        }else if (name.Contains("boss"))
        {
            moveSpeed = 4;
            visibleRange = 10f;
            stopRange = 1;
        }      
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRigidbody = this.GetComponent<Rigidbody2D>();
        //transform.position = new Vector2(10, 10);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position; // difference between player and enemy
            rotateAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Angle between player and enemy
            enemyRigidbody.rotation = rotateAngle; // Rotates the enemy towards player
            direction.Normalize(); // Makes the distance between 1 and -1
            moveDirection = direction;
        }
    }
    private void FixedUpdate()
    {
        Vector2 vectorMag = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);//diference between player and enemy
        float distanceBetweenPlayerandEnemy = Mathf.Sqrt(Mathf.Pow(vectorMag.x, 2) + Mathf.Pow(vectorMag.y, 2));


        if (isRotater) // checks to see if the enemy is rotater and then calls rotate function
        {
            Rotate(width, height);
        }
        else
        {
            MoveEnemy(moveDirection, distanceBetweenPlayerandEnemy, visibleRange, stopRange);
        }
        
    }
    public void MoveEnemy(Vector2 direction, float distanceBetweenPlayerandEnemy, float enemyDistance)
    {
        if (distanceBetweenPlayerandEnemy > enemyDistance)
        {
            enemyRigidbody.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }
    public void MoveEnemy(Vector2 direction, float distanceBetweenPlayerandEnemy, float visibleRange, float stopRange)
    {
          
        if (distanceBetweenPlayerandEnemy <= visibleRange && stopRange < distanceBetweenPlayerandEnemy)
        {
           
            enemyRigidbody.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }
    public void Rotate(float width, float height)
    {
        alpha += moveSpeed;
        transform.position = new Vector2( orbitDirection.x + (width * Mathf.Sin(alpha)), orbitDirection.y + (height * Mathf.Cos(alpha)));
        

    }
}
