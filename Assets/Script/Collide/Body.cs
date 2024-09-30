using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Body : MonoBehaviour
{
    public GameObject chara;

    [SerializeField] private UnityEvent Collide = new UnityEvent();
    [SerializeField] private UnityEvent Exit = new UnityEvent();

    [Serializable] public class DamageEvent : UnityEvent<int> { }
    [SerializeField] DamageEvent Damage = new DamageEvent();
    

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(chara.gameObject.tag);

        if (chara.gameObject.tag == "Player")
        {
            if (collision.gameObject.tag == "Goal poal")
            {
                Gmanager.instance.GameClear();
            }

            if (collision.gameObject.tag == "Enemy")
            {
            }

            
        }
        else if (chara.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.tag == "Attack")
            {
                //Damage.Invoke(collision.gameObject.GetComponent<AttackCollide>().getDamage());
            }

            if (collision.gameObject.tag == "Player")
            { 
                GameObject plr = collision.gameObject.transform.root.gameObject;
                plr.GetComponent<Player>().Damage(chara.gameObject.GetComponent<Enemy>().collideAttack()); 
            }
        }

        if (collision.gameObject.tag == "ground")
        {
            Collide.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            Exit.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
