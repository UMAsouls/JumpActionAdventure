using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Head : MonoBehaviour
{

    private GameObject player;
    private Mover mover;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.root.gameObject;
        mover = player.GetComponent<Mover>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mover.collideH();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
