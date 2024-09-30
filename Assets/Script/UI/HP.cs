using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{

    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)(Gmanager.instance.getHP()) / (float)(Gmanager.maxHP);
    }
}
