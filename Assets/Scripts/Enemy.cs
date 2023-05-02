using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//SE
public class Enemy : MonoBehaviour
{
    [SerializeField] int scoreValue = 2;

    [SerializeField] private float health = 100.0f;

    [SerializeField] private float damageToPlayer = 20.0f;
    [SerializeField] private float damageRate = 0.2f;
    [SerializeField] private float damageTime;

    public GameObject explosion; //Graphic explosion effect
    [SerializeField] AudioClip explosionSFX;
    public GameObject dropCollectible;


    void Update()
    {
        // Kill all enemy instances at Game Over
        // if (GameManager.instance.gameOver) 
        // {
        //     Destroy(gameObject);
        // } 
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)      
        {
            GameObject effect = Instantiate(explosion, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
            Destroy(this.gameObject);
            Destroy(effect, 1.0f);
            GameManager.instance.AddPoints(scoreValue);

            //Randomly drop a coin
            float randomNumber = Random.Range(0, 10);
            if (randomNumber >= 3) 
            {
                Instantiate(dropCollectible, this.transform.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" && other is BoxCollider && Time.time > damageTime)
        {
            other.transform.GetComponent<Player>().TakeDamage(damageToPlayer);
            damageTime = Time.time + damageRate;
        }
    }
}
