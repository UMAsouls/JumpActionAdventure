using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Mover : MonoBehaviour
{
    //速度
    [SerializeField] private Vector2 vel;
    //加速度
    [SerializeField] private Vector2 acc;

    //向き
    [SerializeField] private int dir; 
    public int getDir() { return dir; }

    //補正を受けない位置
    private Vector2 position;
    public void setPos(Vector2 v) { position = v; }

    private float dt;

    public Vector2 get_vel() { return vel; }

    public Vector2 get_acc() {  return acc; }

    //減速するための関数。accに正の実数値を入れ、その加速度をvel.xの絶対値がspeedになるまで維持する。
    //accは9がdefault
    public bool speed_down_to(float speed, float acc = 18)
    {
        float dir;

        if (vel.x >= 0) dir = 1.0f;
        else dir = -1.0f;

        float da = acc * dt;

        if (this.vel.x * dir -da > speed)
        {
            this.acc.x = acc * -1.0f * dir;
        }
        else if (this.vel.x * dir - speed < 0.01 && this.vel.x * dir - speed > -0.01)
        {
            this.acc.x = 0f;
            //m_rigidbody.velocity.Set(0f, vel.y);
            this.vel.x = speed * dir;
            return true;
        }
        else
        {
            this.acc.x = -1.0f * (this.vel.x * dir - speed) / dt;
        }

        return false;

    }

    //speedまでvel.xを加速度を用いて増加させる。その加速度はaccで設定可能。
    //向きはdirで制御(何も入れなければ1.0f)
    //speedとaccは絶対値を代入する
    //keepSpeedをtrueにするとspeed以上の速度になっても減速しない
    public bool speed_up_to(float speed, float acc, float dir = 1.0f, bool keepSpeed = false, float downacc = 18)
    {
        if (dir >= 0) dir = 1.0f;
        else dir = -1.0f;

        float da = acc * dt;

        if (this.vel.x * dir + da < speed)
        {
            this.acc.x = acc * dir;
            return false;
        }
        else if (this.vel.x * dir > speed)
        {   
            if(keepSpeed) { this.acc.x = 0; }
            else { speed_down_to(speed,downacc); }
        }
        else
        {
            this.acc.x = dir * (speed - this.vel.x * dir) / dt;
        }

        return true;
    }

    //ただ速度を設定する関数
    //衝撃で一瞬でその速度に変わるようなときに使う
    public void impact(Vector2 imp)
    {
        vel.Set(imp.x, imp.y);
    }

    //指定したx座標(posx)まで設定した最高速度(speed)と加速度(acc)に従って
    //許容距離(torelance)に入るまで進むための関数
    public void moveTo(float posx, float speed, float acc, float tolerance)
    {
        int todir;

        float dis = posx - transform.position.x;

        if (dis >= 0){ todir = 1; }
        else { todir = -1; dis = -1 * dis; }


        if (dis > tolerance)
        {
            speed_up_to(speed, acc, todir, false);
        }
        else
        {
            //speed_down_to(0, downAcc);
            if (this.dir != todir || dis < tolerance / 5)
            {
                transform.position.Set(posx, transform.position.y, transform.position.z);
                this.acc.x = 0;
                this.vel.x = 0;
            }
            else
            {
                stopAt(posx);
            }  
            
        }
    }

    //止まりたい位置に止まれるように加速度を設定する関数
    //止まりたい位置と速度から加速度を計算
    public void stopAt(float posx)
    {
        float dis = posx - transform.position.x;

        if(dis != 0)
        {
            this.acc.x = -1 * (this.vel.x * this.vel.x / dis) / 2;
        }
    }

    //急激に止まる。速度が0になる
    public void collide()
    {
        acc.x = 0;
        vel.x = 0;
    }

    //縦方向のcollide
    public void collideH()
    {
        vel.y = 0;
    }

    public void set_gravity()
    {
        acc.y = -9.8f;
    }

    //着地時の処理
    public void landing()
    {
        acc.y = 0;
        vel.y = 0;
    }

    public void ground_set()
    {
        acc.y = 0;
    }

    public void air_set()
    {
        acc.y = -9.8f;
        acc.x = 0;
    }

    public void attackMove(float v)
    {
        vel.x = v;
    }

    protected void Start()
    {
        vel = new Vector2(0, 0);
        acc = new Vector2(0, 0);
        position = new Vector2(transform.position.x, transform.position.y);

        dt = Time.deltaTime;
        dir = 0;
    }

    protected void Update()
    {
        //時間変位の取得
        dt = Time.deltaTime;

        //位置計算
        vel += acc * dt;
        position += vel * dt;

        if(vel.x > 0) { dir = 1; }
        else if(vel.x < 0) { dir = -1; }
        else { dir = 0; }

        //実際の位置設定
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
