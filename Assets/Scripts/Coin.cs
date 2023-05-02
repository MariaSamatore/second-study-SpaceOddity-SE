using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 80;
    [SerializeField] private float magneticSpeed = 1.3f;
    [SerializeField] public static float magneticRadius = 1.0f;
    [SerializeField] private float timeToDisappear = 3f;
        


    void Start()
    {
       StartCoroutine(DisappearCoin());
    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        MagnetMove(magneticRadius);
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {         
            GameManager.instance.AddCoins();                
            Destroy(this.gameObject);
        }
    }

    IEnumerator DisappearCoin()
    {
        yield return new WaitForSeconds(timeToDisappear);
        Destroy(gameObject);
    }

    public void MagnetMove(float radius)
    {
        if (GameManager.instance.player)
        {
            if (Vector3.Distance(this.gameObject.transform.position, GameObject.FindWithTag("Player").transform.position) <= radius)
            {
                transform.LookAt(GameManager.instance.player.transform.position);
                transform.position += transform.forward * magneticSpeed * Time.deltaTime;
            }
        }
    }
}
