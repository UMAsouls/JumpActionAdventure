using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverReact : MonoBehaviour
{
    public void Retry()
    {
        Gmanager.instance.Retry();
    }

    public void Return()
    {
        Gmanager.instance.ReturnTitle();
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
