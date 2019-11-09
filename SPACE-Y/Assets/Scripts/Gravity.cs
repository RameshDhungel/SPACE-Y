using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    float gravity = 50;
    bool OnGround = false;
    bool enter = false;


    float distanceToGround;
    Vector3 Groundnormal;
    Rigidbody2D player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        this.gameObject.AddComponent<CircleCollider2D>();
        this.gameObject.GetComponent<CircleCollider2D>().radius = 0.20f;
        this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
    }

    private void FixedUpdate()
    {
        if (enter)
        {

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

            Vector3 gravDirection = (player.transform.position - transform.position).normalized;

            if (OnGround == false)
            {
                player.AddForce(gravDirection * -gravity);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enter = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enter = false;
    }
}
