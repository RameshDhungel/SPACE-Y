using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    private GameObject Planet;
    public GameObject PlayerPlaceholder;
    public LevelGenerator lvelgeneratorsomthing;

    public float speed = 4;
    public float JumpHeight = 1.2f;

    float gravity = 35;
    bool OnGround = false;


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
        //GroundControl

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {

            distanceToGround = hit.distance;
            Groundnormal = hit.normal;

            if (distanceToGround <= 0.2f)
            {
                OnGround = true;
            }
            else
            {
                OnGround = false;
            }


        }


        //GRAVITY and ROTATION

        Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;

        if (OnGround == false)
        {
            rb2d.AddForce(gravDirection * -gravity);

        }

        //

        Quaternion toRotation = Quaternion.FromToRotation(transform.up, Groundnormal) * transform.rotation;
        transform.rotation = toRotation;

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform != Planet.transform)
        {

            Planet = collision.transform.gameObject;

            Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;

            Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravDirection) * transform.rotation;
            transform.rotation = toRotation;

            rb2d.velocity = Vector3.zero;
            rb2d.AddForce(gravDirection * gravity);


       

        }
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

    }
}
