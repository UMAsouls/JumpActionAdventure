using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class base_behavior : MonoBehaviour

{

    //Ground状態を判別
    public bool GroundFlag = false;

    //run状態を判別
    public bool MoveFlag = false;

    //Jump状態を判別
    public bool JumpFlag = false;

    //Dash状態を判別
    public bool DashFlag = false;

    //何かにぶつかってるか判別
    public bool CollideFlag = false;

    //ぶつかっている方向
    public int CollideDir = 0;

    //死亡判定
    public bool DeathFlag = false;

    //Dashの最高速度かどうか
    public bool MaxDashFlag = false;

    //キャラの向き
    public float Direction = -1;

    //キャラの通常移動速度
    public float MoveSpeed = 6;

    //通常移動時の加速度
    public float MoveAcc = 12;

    //キャラのダッシュ移動速度;
    public float DashSpeed = 12;

    //キャラのダッシュ加速度
    public float DashAcc = 18;

    //ジャンプ回数
    public int JumpNum = 0;

    //Flame間の時間
    float dt;

    Dictionary<string, KeyCode> key_config;

    //Rigidbody
    Rigidbody2D m_rigidbody;

    Animator m_animator;

    Mover mover;

    //止まるときの動作
    //止まるまではDashFlagはtrue
    void stop()
    {
        MoveFlag = false;
        DashFlag = false;
        CollideFlag = false;    
        m_animator.SetBool("run", false);

        if (MaxDashFlag)
        {
            if (mover.speed_down_to(0, 30))
            {
                MaxDashFlag = false;
                
            }
        }
        else
        {
            mover.speed_down_to(0);
        }
        
    }


    //地面での移動動作
    //Dashが移動途中で終了したら元の速度へ減速
    void move(float dir = 1.0f)
    {
        MoveFlag = true;
        m_animator.SetBool("run", true);
        if (Input.GetKey(key_config["dash"]))
        {
            DashFlag = true;
            if (mover.speed_up_to(DashSpeed, DashAcc, dir)) MaxDashFlag = true;
        }
        else
        {
            DashFlag = false;
            mover.speed_up_to(MoveSpeed, MoveAcc, dir);
        }
    }

    //空中での移動動作
    void airMove(float dir = 1.0f)
    {
        mover.speed_up_to(1.0f, 3, dir, true);
    }

    //ジャンプの動作
    void jump()
    {
        Vector2 j = new Vector2(mover.get_vel().x, 5f);
        mover.impact(j);

        JumpFlag = true;
        JumpNum++;
    }

    //空中ジャンプの動作
    void airJump()
    {
        Vector2 vel = mover.get_vel();
        Vector2 j = new Vector2(vel.x, 5f);

        float add = 3;

        //速度がある一定以上だと方向転換での速度が無くなる
        if (vel.x > MoveSpeed+1 || vel.x < -1*MoveSpeed-1)
        {
            add = 0;
        }

        //空中での方向転換
        if (Input.GetKey(key_config["forward"]))
        {
            if (j.x < add) j.x = add;
        }
        else if (Input.GetKey(key_config["back"]))
        {
            if (j.x > -1*add) j.x = -1*add;
        }

        
        mover.impact(j);

        JumpNum++;
    }

    public void onGround()
    {
        if(!GroundFlag)
        {
            JumpFlag = false;
            JumpNum = 0;
        }

        GroundFlag = true;
    }

    public void exitGround()
    {
        GroundFlag = false;
    }


    //地上にいるときの動作
    void ground_behavior()
    {
        if (!JumpFlag)
        {
            mover.ground_set();

            if (Input.GetKey(key_config["forward"]))
            {
                if(CollideDir * Direction != -1)
                {
                    move(1.0f);
                }

                Direction = -1;
            }
            else if (Input.GetKey(key_config["back"]))
            {
                if (CollideDir * Direction != 1)
                {
                    move(-1.0f);
                }

                Direction = 1;
            }
            else stop();

            if (Input.GetKeyDown(key_config["up"])) jump();
        }
    }

    //空中での動作
    void airBehavior()
    {
        mover.air_set();

        if (JumpFlag)
        {
            if (mover.get_vel().y < 0) JumpFlag = false;
        }

        if (Input.GetKey(key_config["forward"]))
        {
            if(CollideDir*Direction != -1)
            {
                airMove(1.0f);
            }   
        }
        else if (Input.GetKey(key_config["back"]))
        {
            if (CollideDir*Direction != 1)
            {
                airMove(-1.0f);
            }
        }

        if (Input.GetKeyDown(key_config["up"])) { 
            if(JumpNum < 3) airJump(); 
        }

    }

    public void forwardOnCollisionEnter()
    {
        Debug.Log("Enter_r");
        CollideFlag = true;
        CollideDir = 1;
        mover.collide();
    }

    public void forwardOnCollisionExit()
    {
        Debug.Log("Exit_r");
        CollideFlag = false;
        CollideDir = 0;
    }

    public void backOnCollisionEnter()
    {
        Debug.Log("Enter_l");
        CollideFlag = true;
        CollideDir = -1;
        mover.collide();
    }

    public void backOnCollisionExit()
    {
        Debug.Log("Exit_l");
        CollideFlag = false;
        CollideDir = 0;
    }

    // Start is called before the first frame update
    void Start()
    {   //keyの設定
        key_config = Gmanager.instance.key_config;
        key_config.Add("forward", KeyCode.RightArrow);
        key_config.Add("back", KeyCode.LeftArrow);
        key_config.Add("up", KeyCode.UpArrow);

        key_config.Add("dash", KeyCode.D);

        //GroundFlag = true;

        MoveFlag = false;

        m_animator = GetComponent<Animator>();

        mover = GetComponent<Mover>();
    }

    // Update is called once per frame
    void Update()
    {
        //重力加速度
        //acc.y = -9.8f;
        mover.set_gravity();

        //時間変位の取得
        dt = Time.deltaTime;

        //速度の取得
        //vel = m_rigidbody.velocity;

        //入力による操作
        if (GroundFlag) ground_behavior();
        else airBehavior();

        //キャラの向き設定
        transform.localScale = new Vector3(Direction, 1.0f, 1.0f);
        
    }
}
