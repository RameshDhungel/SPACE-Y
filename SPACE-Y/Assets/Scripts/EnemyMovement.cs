using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float moveSpeed = 1f;
    private float health;
    private Transform player;
    private Rigidbody2D enemyRigidbody;
    private Vector2 moveDirection;
    //private float enemyDistance = 5;
    //private float BomberenemyDistance = 0;
    //private float splitter = 8;
    private float[] movemnetArray = { 10, 0, 8 };
    private float[] stopmovemnetArray = { 4, 0, 4 };
    private float rotateAngle;
    Vector2 orbitDirection = new Vector2(0, 0);
    float alpha = .003f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(player.name);
        enemyRigidbody = this.GetComponent<Rigidbody2D>();
        Debug.Log(enemyRigidbody.name);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        rotateAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyRigidbody.rotation = rotateAngle;
        direction.Normalize();
        moveDirection = direction;
    }
    private void FixedUpdate()
    {
        Vector2 vectorMag = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        float distanceBetweenPlayerandEnemy = Mathf.Sqrt(Mathf.Pow(vectorMag.x, 2) + Mathf.Pow(vectorMag.y, 2));


        //MoveEnemy(moveDirection, distanceBetweenPlayerandEnemy, movemnetArray[0], stopmovemnetArray[0]);
        Rotate(5, 5);
        
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
        orbitDirection.x = orbitDirection.x + (width * Mathf.Sin(alpha));
        orbitDirection.y = orbitDirection.y + (width * Mathf.Cos(alpha));
        transform.position = orbitDirection;

    }
}
