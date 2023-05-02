using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject npcSpaceshipPrefab;
    [SerializeField] private int whenToAppear;
    [SerializeField] GameObject npcText;    
    [SerializeField] AudioClip appearanceSFX;


    void Start()
    {
        StartCoroutine(InstantiateNPC());
    }

    public IEnumerator InstantiateNPC()
    {
        yield return new WaitForSeconds(whenToAppear);
        Instantiate(npcSpaceshipPrefab, transform.position, Quaternion.identity);
        npcText.SetActive(true);
        AudioSource.PlayClipAtPoint(appearanceSFX, transform.position);
        yield return new WaitForSeconds(15); //for the length of the animation
        npcText.SetActive(false);
    }

}
