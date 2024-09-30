using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeText = gameObject.GetComponent<TextMeshProUGUI>();
        float time = Gmanager.instance.getClearTime();
        int t1 = (int)time;
        int t2 = (int)((time - t1) * 100);
        timeText.text = "Time : " + t1.ToString() + "." + t2.ToString();

    }
}
