using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackCollide : MonoBehaviour
{
    [SerializeField] private int Damage;
    public int getDamage() { return Damage; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            GameObject plr = transform.root.gameObject;
            GameObject en = collision.gameObject.transform.root.gameObject;
            int dir;
            if(plr.transform.position.x >= en.transform.position.x) { dir = -1; }
            else { dir = 1;  }
            en.GetComponent<Enemy>().damage(Damage, dir);
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
