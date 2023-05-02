using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]



public class Boundary 
{
    public float xMin, xMax, zMin, zMax;
}

//SE
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float tilt;
    [SerializeField] private Text playerNameField;
    [SerializeField] GameObject damageVFX;
    [SerializeField] AudioClip damageSFX;

    public float health = 100.0f;
    public GameObject explosion; //Graphic explosion effect

    public Boundary boundary;

    //public bool doubleGunState;
    [HideInInspector] public GameObject LeftGun2;
    [HideInInspector] public GameObject RightGun2;

    //game log
    [SerializeField] private GameDataLog logger;
    private float curTime = 0;


    private void Start()
    {
        LeftGun2 = transform.GetChild(3).gameObject;
        RightGun2 = transform.GetChild(5).gameObject;
        //Double Gun State
        LeftGun2.SetActive(false);
        RightGun2.SetActive(false);

        GameManager.instance.UpdateHealth(health);
        GameManager.instance.UpdateCoin();
        playerNameField.text = GameManager.instance.username; 
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        curTime += Time.deltaTime;
    }

    private void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * moveSpeed * Time.deltaTime;

        //Clamping position to game boundaries
        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }


    private IEnumerator OnTriggerEnter(Collider other)
    {
        //Changing player's colour on collision 
        if (other.transform.tag == "Enemy" || other.transform.tag == "Asteroid")
        {
            GetComponentsInChildren<Renderer>()[3].material.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            GetComponentsInChildren<Renderer>()[3].material.color = Color.red;
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        GameManager.instance.UpdateHealth(health);

        //instantiating damage to player vfx and sfx 
        AudioSource.PlayClipAtPoint(damageSFX, transform.position);
        GameObject damageGraphicEffect = Instantiate(damageVFX, transform.position, transform.rotation);
        damageGraphicEffect.transform.SetParent(this.transform); //The effect follows the player object
        Destroy(damageGraphicEffect, 1.0f);

        if (health <= 0)
        {
            GameObject effect = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
            Destroy(effect, 1.0f);

            //Logging a death
            if (logger)
            {
                Debug.Log("Saving to log");
                GameDataLog.LoggingDeathResults log = new GameDataLog.LoggingDeathResults();

                log.timestamp = curTime.ToString();
                log.levelID = SceneManager.GetActiveScene().name;

                logger.LogDeathResultData(log);
            }
            else
            {
                Debug.Log("No logger drag dropped into inspector.");
            }

            GameManager.instance.DeathCounterPerLevel(); //When player dies, signals the GameManager to increment the number of deaths.
            GameManager.instance.gameOver = true;        
        }
    }

    public IEnumerator TurnOffDoubleGun() 
    {
        LeftGun2.SetActive(true);
        RightGun2.SetActive(true);

        yield return new WaitForSeconds(10); //double gun duration is 10 seconds

        LeftGun2.SetActive(false);
        RightGun2.SetActive(false);
    }
}
