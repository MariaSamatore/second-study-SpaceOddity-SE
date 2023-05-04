using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float tumble = 1.5f; // Asteroid rotates around itself

    [SerializeField] int scoreValue = 1;
    
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float damageTaken = 10.0f;
    [SerializeField] private float damageToPlayer = 10.0f;
    [SerializeField] private float damageRate = 0.2f;
    [SerializeField] private float damageTime;

    [SerializeField] AudioClip explosionSFX;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float dropCollectiblePercentage;

    public GameObject[] dropCollectible;

    //Praise audio
    [SerializeField] private AudioClip[] praiseSFX;



    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }

        //Instantiating explosion effects for both the hazard and the player on collision
        if (other.tag != "Asteroid" && other.tag != "Enemy" && other.tag != "Powerup" && other.tag != "Coin")
            {
                GameObject explosionInstance = Instantiate(explosion, transform.position, transform.rotation);
                TakeDamage(damageTaken);
                AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
        }

        if (other.transform.tag == "Player" && Time.time > damageTime)
        {
            other.transform.GetComponent<Player>().TakeDamage(damageToPlayer);
            damageTime = Time.time + damageRate;
        }

        //Asteroids destroy the enemies they collide with
        if (other.transform.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(100);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health == 0)
        {

            GameObject effect = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
            Destroy(effect, 1.0f);
            GameManager.instance.AddPoints(scoreValue);

            //Randomly drop a Power-up
            float randomNumber = Random.Range(1, 100);
            if (randomNumber <= dropCollectiblePercentage)
            {
                if (dropCollectible != null)
                {
                    InstantiatePowerUp();

                    //Praise the player
                    AudioClip praiseClip = praiseSFX[UnityEngine.Random.Range(0, praiseSFX.Length - 1)];
                    AudioSource.PlayClipAtPoint(praiseClip, transform.position);
                }
            }
        }
    }

    void InstantiatePowerUp()
    {
        float playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().health;

        if (playerHealth <= 20) //Instantiate health pack
        {
            Instantiate(dropCollectible[0], this.transform.position, Quaternion.identity);
        }

        else if (playerHealth == 50) //Don't instantiate health pack
        {
            Instantiate(dropCollectible[Random.Range(1, 3)], this.transform.position, Quaternion.identity);
        }

        else //Instantiate any powerup
        {
            Instantiate(dropCollectible[Random.Range(0, 3)], this.transform.position, Quaternion.identity);
        }
        
    }
}
