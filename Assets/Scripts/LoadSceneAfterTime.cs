using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterTime : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 10f;

    [SerializeField]
    private string sceneNameToLoad;
    
    private float timeElapsed;

    private bool timeToLoad = true;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > delayBeforeLoading && timeToLoad)
        {
            if (timeToLoad)
            {
                GameManager.deathCountPerLevel = 0; //This line is to reset the number of deaths at the beginning of each level. This way, we ensure that the 6 deaths per level constraint is set. 
                SceneManager.LoadScene(sceneNameToLoad);
                timeToLoad = false;
            }
        }
    }
}
