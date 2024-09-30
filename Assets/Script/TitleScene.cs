using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeScene()
    {
        Gmanager.instance.nextStage();
    }

    public void goTutorial()
    {
        Gmanager.instance.goTutorial();
    }
}
