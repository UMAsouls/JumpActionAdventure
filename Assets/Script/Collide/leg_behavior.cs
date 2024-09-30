using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class leg_behavior : MonoBehaviour
{
    public GameObject chara;

    [SerializeField] private UnityEvent OnGround = new UnityEvent();
    [SerializeField] private UnityEvent ExitGround = new UnityEvent();


    // Start is called before the first frame update
    void Start()
    {
        chara = transform.root.gameObject;
        Debug.Log("start");
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("enter_g_c");
        OnGround.Invoke();
    }
    */
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            OnGround.Invoke();
        }

        if(chara.tag == "Player")
        {
            if(collision.gameObject.tag == "Enemy")
            {
                GameObject en = collision.gameObject.transform.root.gameObject;
                en.GetComponent<Enemy>().damage(chara.GetComponent<PlayerStatus>().getFootDamage(), 0, 0);
                chara.GetComponent<PlayerMover>().stamp();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            ExitGround.Invoke();
        }
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
    }
}
