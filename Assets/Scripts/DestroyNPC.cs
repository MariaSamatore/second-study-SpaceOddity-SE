using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyNPC : MonoBehaviour
{
    public GameObject explosionVFX; //Graphic explosion effect
    [SerializeField] AudioClip explosionSFX;
    //[SerializeField] GameObject npcText;


    void Start()
    {
        StartCoroutine(DestroyNPCSpaceship());
    }

    private IEnumerator DestroyNPCSpaceship()
    {
        yield return new WaitForSeconds(15); // after 15 seconds of flying
        //npcText.SetActive(false);
        GameObject effect = Instantiate(explosionVFX, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
        Destroy(this.gameObject);
    }
}
