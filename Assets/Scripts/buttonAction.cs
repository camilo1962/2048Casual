using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class buttonAction : MonoBehaviour
{

    public void botonMetodo(string name)
    {
        SceneManager.LoadScene(name);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
