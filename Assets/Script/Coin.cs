using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private int score;
    public int getScore() { return score; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("get");
        if (collision.gameObject.tag == "Player")
        {
            Gmanager.instance.addSceneScore(score);
            Destroy(this.gameObject);
        }
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
