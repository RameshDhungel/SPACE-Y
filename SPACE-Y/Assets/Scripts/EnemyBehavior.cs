using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Vector2 enemyShootRange = new Vector2(6, 6);
    float waitTime = 3.0f;
    public GameObject bulletPrefab;
    private Transform player;
    GameObject previousBullet = null;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
         if (Mathf.Abs(player.position.x - transform.position.x) < enemyShootRange.x || Mathf.Abs(player.position.y - transform.position.y) < enemyShootRange.y)
         {
            Shoot();
         } 
    }

    public void Shoot()
    {
        if (waitTime < Time.time)
        {
            if (previousBullet != null)
            {
                Destroy(previousBullet);
            }
            GameObject bullet = Instantiate(bulletPrefab, this.gameObject.GetComponentInChildren<Transform>());
            Vector2 direction = new Vector2(player.transform.position.x, player.transform.position.y);
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, bullet.transform.up).z;
            bullet.GetComponent<Rigidbody2D>().angularVelocity = -rotateAmount * 1000f;
            waitTime = Time.time + 1.5f;
            bullet.GetComponent<Rigidbody2D>().velocity = transform.up = direction * 3000 * Time.fixedDeltaTime;
            previousBullet = bullet;
        }
    }
}
