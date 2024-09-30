using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearReact : MonoBehaviour
{
    public void NextStage()
    {
        Gmanager.instance.nextStage();
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
