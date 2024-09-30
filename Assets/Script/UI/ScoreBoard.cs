using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{

    private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score : " + Gmanager.instance.getSceneScore().ToString();
        
    }
}
