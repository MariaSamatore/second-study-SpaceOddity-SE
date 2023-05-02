using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector3 screenBounds;
    private float objectHeight;
    private float objectWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x/2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y/2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPosition = transform.position;
        viewPosition.x = Mathf.Clamp(viewPosition.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
        viewPosition.y = Mathf.Clamp(viewPosition.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
        transform.position = viewPosition;
    }
}
