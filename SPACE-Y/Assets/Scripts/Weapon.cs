﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Vector2 direction;
    GameObject player;
    PlayerHealth playerHealth;
    public float speed = 60f;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Enemyshoot(Transform player)
    {
        direction = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y).normalized * 100;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y);
    }
}
