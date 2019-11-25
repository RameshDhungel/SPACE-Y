using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    private GameObject Planet;
    public GameObject PlayerPlaceholder;
    public LevelGenerator lvelgeneratorsomthing;
    public GameObject bulletPrefab;

    public float speed = 4;
    public float JumpHeight = 1.2f;

   // float gravity = 35;
    bool OnGround = false;
    bool LookingLeft = false;
    bool LookingRight = true;


    float distanceToGround;
    Vector3 Groundnormal;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lvelgeneratorsomthing = GameObject.FindObjectOfType<LevelGenerator>();
        
        while (!lvelgeneratorsomthing.generationComplete)
        {
          
        }
        transform.position = new Vector2(lvelgeneratorsomthing.spawnAsteroid.transform.position.x, lvelgeneratorsomthing.spawnAsteroid.transform.position.y + 20);
        Planet = lvelgeneratorsomthing.spawnAsteroid;
        
    }
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
           
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            
        }
        else if (Input.GetKey("w") || Input.GetKey("up"))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x,JumpHeight);
        }
        if (LookingRight)
        {
            
            if (Input.GetKey("a"))
            {
                LookingRight = false;
                LookingLeft = true;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        if (LookingLeft)
        {
            if (Input.GetKey("d"))
            {
                LookingRight = true;
                LookingLeft = false;
                transform.Rotate(0f, 180f, 0f);
            }

        }

    }
}
