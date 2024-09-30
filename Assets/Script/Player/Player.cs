using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Dictionary<string, KeyCode> key_config;
    private PlayerMover mover;
    private PlayerStatus status;

    private Animator animator;

    private bool stopper;

    private GameObject Sprite;
    

    //地上でのふるまい
    private void groundBehave()
    {
        if (!status.getJumpFlag())
        {
            mover.ground_set();

            if(!status.IsPunch())
            {
                //移動操作
                if (Input.GetKey(key_config["forward"]))
                {
                    //ぶつかっている方向と進もうとしている方向が違うかどうか
                    if (!status.checkRightCollide())
                    {
                        if (Input.GetKey(key_config["dash"])) { mover.PlayerDash(1.0f); }
                        else { mover.PlayerMove(1.0f); }
                    }

                    status.setDirForward();
                }
                else if (Input.GetKey(key_config["back"]))
                {
                    //ぶつかっている方向と進もうとしている方向が違うかどうか
                    if (!status.checkLeftCollide())
                    {
                        if (Input.GetKey(key_config["dash"])) { mover.PlayerDash(-1.0f); }
                        else { mover.PlayerMove(-1.0f); }
                    }

                    status.setDirBack();
                }
                else mover.PlayerStop();
            }

            

            if (Input.GetKey(key_config["attack"]) && !stopper && !animator.GetBool("attack"))
            {
                animator.SetBool("attack", true);
                status.setIsPunch(true);
                mover.punchMove();
                stopper = true;
            }
            else
            {
                stopper = false;
            }

            //ジャンプ操作
            if (Input.GetKeyDown(key_config["up"]) && !status.IsPunch()) mover.PlayerJump();
        }
    }

    //キャラの空中での振る舞い
    private void airBehave()
    {
        mover.air_set();

        int jump_dir = 0;

        //移動操作
        if (Input.GetKey(key_config["forward"]))
        {
            if (!status.checkRightCollide())
            {
                mover.PlayerAirMove(1.0f);
                jump_dir = 1;
            }
        }
        else if (Input.GetKey(key_config["back"]))
        {
            if (!status.checkLeftCollide())
            {
                mover.PlayerAirMove(-1.0f);
                jump_dir = -1;
            }      
        }

        //ジャンプ
        if (Input.GetKeyDown(key_config["up"]))
        {
            if (status.JumpCheck()) mover.PlayerAirJump(jump_dir);
        }
    }


    public void onGround()
    {
        if (!status.getGroundFlag())
        {
            status.setJumpFlag(false);
            animator.SetBool("ground", true);
            status.resetJumpNum();
            mover.landing();
        }

        status.setGroundFlag(true);
    }

    public void exitGround()
    {
        status.setGroundFlag(false);
        animator.SetBool("ground", false);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Goal poal")
        {
            Gmanager.instance.GameClear();
        }
    }

    public void forwardOnCollisionEnter()
    {
        Debug.Log("Enter_r");
        status.setCollideFlag(true);
        status.forwardCollide();
        mover.collide();
    }

    public void forwardOnCollisionExit()
    {
        Debug.Log("Exit_r");
        status.setCollideFlag(false);
        status.noCollide();
    }

    public void backOnCollisionEnter()
    {
        Debug.Log("Enter_l");
        status.setCollideFlag(true);
        status.backCollide();
        mover.collide();
    }

    public void backOnCollisionExit()
    {
        Debug.Log("Exit_l");
        status.setCollideFlag(false);
        status.noCollide();
    }

    public void Damage(int dam)
    {
        status.SetHP(status.GetHP() - dam);
        mover.knockBack(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        key_config = Gmanager.instance.key_config;
        mover = GetComponent<PlayerMover>();
        status = GetComponent<PlayerStatus>();

        //keyの設定
        key_config = Gmanager.instance.key_config;

        animator = GetComponent<Animator>();

        status.setIsPunch(false);
        Sprite = transform.Find("Sprite").gameObject;
        stopper = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (status.getGroundFlag()) groundBehave();
        else airBehave();

        if(!status.IsPunch() && animator.GetBool("attack"))
        {
            //transform.position += Sprite.transform.position;
            //Sprite.transform.position = Vector3.zero;
            animator.SetBool("attack", false);
            mover.collide();
            stopper = false;
        } else if(status.IsPunch())
        {
            mover.punchMove();
        }

        //キャラの向き設定
        transform.localScale = new Vector3(status.getDirection() * -1, 1.0f, 1.0f);
        //Sprite.transform.localScale = transform.localScale;

        if(transform.position.y < -7 || status.GetHP() <= 0)
        {
            Gmanager.instance.GameOver();
            if(Gmanager.instance!= null) { Debug.Log("ar"); }
            else { Debug.Log("no"); }
        }
    }
}
