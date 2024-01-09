using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void ChangeScene(string _sceneName){
        SceneManager.LoadScene(_sceneName);
    }

    public void Disable(GameObject _canvasName){
        _canvasName.SetActive(false);
    }

    public void Enable(GameObject _canvasName){
        _canvasName.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
