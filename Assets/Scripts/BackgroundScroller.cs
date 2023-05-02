using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(-5f, 5f)]
    [SerializeField] float scrollSpeed = 3.0f;
    private float offset;
    private Material mat;


    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    
    void Update()
    {
        offset += (Time.deltaTime * -scrollSpeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
