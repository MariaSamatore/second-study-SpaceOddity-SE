using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 80;
    [SerializeField] private float magneticSpeed = 1f;
    [SerializeField] private float magneticFieldRadius;
    [SerializeField] int powerupChoice = 0;

    //VFX and SFX
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] AudioClip sfx;

  
    //Health booster powerup
    [SerializeField] private float healthBoostValue = 30f;

    //Increased magnetic field
    [SerializeField] public float magnetDuration = 10f;
    [SerializeField] private float radiusAdder = 2f;

    //Double Gun
    [SerializeField] public float doubleGunDuration = 10f;

    //Timer graphic
    [SerializeField] private GameObject doubleGunTimerGraphic;
    [SerializeField] private GameObject magnetTimerGraphic;

    private void Start()
    {
        /*Because I defined the magnetic radius as a static variable in the coin class (script), 
        I need to reset the magnetic radius at the start of the game. Otherwise if I exit the game when the 
        manetic radius is increased, the next time I play the game, the radius is increased at the beginning*/

        magneticFieldRadius = 1f; //default value for the magnetic radius. 
    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        MagnetMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject pickupVFX = Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(pickupVFX, 1.0f);
            AudioSource.PlayClipAtPoint(sfx, transform.position);

            if (powerupChoice == 1)      // Health pack
            {
                Player stats = other.GetComponent<Player>();
                stats.health += healthBoostValue;
                if (stats.health > 100)
                {
                    stats.health = 100;
                }
                GameManager.instance.UpdateHealth(stats.health);
                Destroy(this.gameObject);

                
            }

            else if (powerupChoice == 2)     //Double Gun
            {
                //Removing othe graphic of the powerup at touching it but the powerup effect is active
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                
                //Instantiate a timer graphic prefab
                GameObject timer = Instantiate(doubleGunTimerGraphic, transform.position, Quaternion.identity) as GameObject;
                timer.transform.SetParent(GameObject.FindGameObjectWithTag("DoubleGun timer").transform, false);
                timer.transform.localPosition = new Vector2 (0, 0);

                //The powerup effect
                StartCoroutine(other.gameObject.GetComponent<Player>().TurnOffDoubleGun());
                Destroy(this.gameObject, doubleGunDuration + 1);
                Destroy(timer, doubleGunDuration);
            }

            else   //Increased Magnet
            {
                //Removing othe graphic of the powerup at touching it but the powerup effect is active
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                // Powerup effect: Increase the radius of the magnetism
                magneticFieldRadius += radiusAdder;
                Coin.magneticRadius = magneticFieldRadius; //passing radius to coin magnet

                //Instantiate a timer graphic prefab
                GameObject timer = Instantiate(magnetTimerGraphic, transform.position, Quaternion.identity);
                timer.transform.SetParent(GameObject.FindGameObjectWithTag("Magnet timer").transform, false);
                timer.transform.localPosition = new Vector2 (0, 0);
                

                //Reversing the powerup effect
                Destroy(timer, magnetDuration);
                StartCoroutine(MagnetTimer());
            }
            
        }
    }

    IEnumerator MagnetTimer()
    {
        yield return new WaitForSeconds(magnetDuration);
        magneticFieldRadius -= radiusAdder;
        Coin.magneticRadius = magneticFieldRadius;
        Destroy(this.gameObject, magnetDuration);
    }


    private void MagnetMove()
    {
        if (GameManager.instance.player)
        {
            if (Vector3.Distance(this.gameObject.transform.position, GameObject.FindWithTag("Player").transform.position) < magneticFieldRadius)
            {
                transform.LookAt(GameManager.instance.player.transform.position);
                transform.position += transform.forward * magneticSpeed * Time.deltaTime;
            }
        }
    }
}
