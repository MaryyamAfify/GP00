using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [Range(1,10)][SerializeField] private float delay= 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1);
    }

}
