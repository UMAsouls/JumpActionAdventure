using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//プレイヤーの動きの制御
public class PlayerMover : MonoBehaviour
{

    [SerializeField] private PlayerStatus status;
    [SerializeField] private Animator animator;
    [SerializeField] private Mover mover;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        status = GetComponent<PlayerStatus>();
        mover = GetComponent<Mover>();
    }

    public void ground_set() { mover.ground_set(); }
    public void air_set() { mover.air_set(); }
    public void collide() { mover.collide(); }
    public void landing() { mover.landing(); }

    //プレイヤーが止まっているときの動作
    public void PlayerStop()
    {
        status.setMoveFlag(false);
        status.setDashFlag(false);
        status.setCollideFlag(false); 

        animator.SetBool("run", false);

        if (status.getMaxDashFlag())
        {
            if (mover.speed_down_to(0, 30))
            {
                status.setMaxDashFlag(false);
            }
        }
        else
        {
            mover.speed_down_to(0);
        }
    }

    //プレイヤーの走り
    public void PlayerMove(float dir)
    {
        status.setMoveFlag(true);
        status.setDashFlag(false);

        animator.SetBool("run", true);

        mover.speed_up_to(status.getMoveSpeed(), status.getMoveAcc(), dir, false);
    }

    //プレイヤーのダッシュ
    public void PlayerDash(float dir)
    {
        status.setMoveFlag(true);
        status.setDashFlag(true);

        animator.SetBool("run", true);

        mover.speed_up_to(status.getDashSpeed(), status.getDashAcc(), dir, false);
    }

    //ジャンプ
    public void PlayerJump()
    {
        Vector2 j = new Vector2(mover.get_vel().x, status.getJumpHeight());
        mover.impact(j);

        status.setJumpFlag(true);
        status.addJumpNum();
        animator.SetBool("jump", true);
    }

    //空中での動き
    public void PlayerAirMove(float dir)
    {
        mover.speed_up_to(1.5f, 5, dir, true);
    }

    //空中ジャンプ
    public void PlayerAirJump(float dir)
    {
        Vector2 vel = mover.get_vel();
        Vector2 j = new Vector2(vel.x, status.getJumpHeight());

        float add = 3;

        //速度がある一定以上だと方向転換での速度が無くなる
        if (vel.x > status.getMoveSpeed() + 1 || vel.x < -1 * status.getMoveSpeed() - 1)
        {
            add = 0;
        }

        //空中での方向転換
        if (dir == 1)
        {
            if (j.x < add) j.x = add;   
        }
        else if (dir == -1)
        {
            if (j.x > -1 * add) j.x = -1 * add;
        }

        if (j.x > 0) status.setDirForward();
        else if(j.x < 0) status.setDirBack();

        mover.impact(j);

        status.addJumpNum();
        animator.SetBool("airJump", true);
        
    }

    //敵に接触した際のノックバック
    public void knockBack(int level)
    {
        Vector2 k;
        float dir = mover.getDir();
        if(dir == 0) { dir = status.getDirection(); }

        if (level == 1)
        {
            k = new Vector2(6*dir*-1, 3);
        }
        else
        {
            k = new Vector2(7 * dir * -1, 8);
        }

        mover.impact(k);
        
    }

    public void stamp()
    {
        Vector2 k = new Vector2(mover.get_vel().x, 6f);

        mover.impact(k);
    }

    public void punchMove()
    {
        if (status.getCollideDir() != 1)
        {
            mover.attackMove(status.getPunchLong() * status.getDirection());
        }
        else
        {
            mover.collide();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (mover.get_vel().y < 0 && !status.getGroundFlag()) 
        { 
            status.setFallingFlag(true);
            status.setJumpFlag(false);
            animator.SetBool("jump", false);
            animator.SetBool("airJump", false);
        }
        else
        {
            status.setFallingFlag(false);
        }
    }
}
