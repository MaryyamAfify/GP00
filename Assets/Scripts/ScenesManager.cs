using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    public void OpenURL1()
    {
        Application.OpenURL("https://www.turbosquid.com/Search/Index.cfm?keyword=wedding+decorations&media_typeid=2");
    }
    public void OpenURL2()
    {
        Application.OpenURL("https://www.elegantweddinginvites.com/");
    }
    public void OpenURL3()
    {
        Application.OpenURL("https://www.turbosquid.com/Search/3D-Models/free/wedding");
    }
    
}
