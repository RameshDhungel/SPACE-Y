using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    private Vector2 direction;
    private Vector3 target; // This is the mouse position 
    public Camera camera;
    public GameObject bulletPrefab;
    public GameObject firePoint;

    private float speed = 1000f; //speed of bullet it is high because we multiplied it by fixed time
    private GameObject[] bulletArray;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        bulletArray = GameObject.FindGameObjectsWithTag("PlayerWeapon"); // Stores all the player bullet in a array
        if(bulletArray.Length > 15) // This checks if there is more than 15 bullet in the Array
        {
            Destroy(bulletArray[0]); // This will delete the oldest bullet so we dont have shit tone of unneeded bullet prefab in hierarchy
        }

        target = camera.ScreenToWorldPoint(Input.mousePosition); //converts mouse clicks in the screen to game world and stores it as vector 3
        Vector3 difference = target - transform.position; // This is the difference between weapon and the mouse click
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // This just gets an rotaion of z axis from the difference
        //rotZ = rotZ > 173 ? 173 : rotZ < -0.3f ? -0.3f : rotZ; // To prevent gun from going under player but currently doesnt work
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);  // This rotates the gun towards the mouse constantly does it even when not clicking
        if (Input.GetButtonDown("Fire1"))
        {
            float distance = difference.magnitude; //This is the distance between weapon and mouse click
            Vector2 direction = difference / distance; // this makes a vector 2 of the direction the bullet should follow
            direction.Normalize(); // This makes it so the direction is 1 or -1 so we can multiply with our speed var
            shoot(direction,rotZ);

        }
    }
    public void shoot(Vector2 direction, float rotZ)
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = firePoint.transform.position;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * speed * Time.fixedDeltaTime;
        bullet.tag = "PlayerWeapon"; //Changes the tag to player weapon so we can compare tag later
    }
}
