using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    int damage = 10;


    PlayerController playerController;
    //PlayerShooting playerShooting;

    bool isDead;
    bool damaged;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        //playerShooting

        currentHealth = startingHealth;
    }

    void Update()
    {
        Debug.Log(currentHealth);
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "EnemyWeapon") // Checks if the bullet is from enemy
        {
            GameObject enemy = collision.collider.GetComponent<Weapon>().GetEnemyObject(); //This find the script Weapon on the bullet and returns the correct enemy object
            TakeDamge(enemy.GetComponent<EnemyBehavior>().DealDamage()); // From the correct enemy it gets its Enemy Behavior script and gets the damage value
            Destroy(collision.collider.gameObject); // Destorys bullet once it hits the player
        }
    }
    public void TakeDamge (int amount)
    {

        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if(currentHealth <= 0 && !isDead)
        {
            Death(); 
        }
    }

    void Death()
    {
        isDead = true;
        //playerShooting

        playerController.enabled = false;
        Destroy(this.gameObject); // destroys player
        Time.timeScale = 0f; // freezes the game
    }
    public int Dealdamage()
    {
        return damage;
    }
}
