using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{

    private GameObject self;
    private EnemyStatus status;

    float HP;
    float MaxHP;

    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 1;

        self = transform.root.gameObject;
        status = self.GetComponent<EnemyStatus>();

        HP = status.getHP();
        MaxHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        HP = status.getHP();
        slider.value = HP / MaxHP;

        if(self.transform.localScale.x < 0){
            slider.direction = Slider.Direction.LeftToRight;
        }
        else
        {
            slider.direction = Slider.Direction.RightToLeft;
        }
    }
}
