using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float health;
    private Transform player;
    private Rigidbody2D enemyRigidbody;
    private Vector2 moveDirection;
    private Vector2 enemyDistance = new Vector2(5,5);
    private float rotateAngle;

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

        /* if (Mathf.Abs(player.position.x - transform.position.x) > enemyShootRange.x || Mathf.Abs(player.position.y - transform.position.y) > enemyShootRange.y)
         {
            Shoot();
         } */
 
    }
    private void FixedUpdate()
    {
        if (Mathf.Abs(player.position.x - transform.position.x) > enemyDistance.x || Mathf.Abs(player.position.y - transform.position.y) > enemyDistance.y)
        {
            MoveEnemy(moveDirection);
        }
        
    }
    public void MoveEnemy(Vector2 direction)
    {
        enemyRigidbody.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
