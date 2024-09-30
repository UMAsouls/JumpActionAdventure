using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Mover : MonoBehaviour
{
    //���x
    [SerializeField] private Vector2 vel;
    //�����x
    [SerializeField] private Vector2 acc;

    //����
    [SerializeField] private int dir; 
    public int getDir() { return dir; }

    //�␳���󂯂Ȃ��ʒu
    private Vector2 position;
    public void setPos(Vector2 v) { position = v; }

    private float dt;

    public Vector2 get_vel() { return vel; }

    public Vector2 get_acc() {  return acc; }

    //�������邽�߂̊֐��Bacc�ɐ��̎����l�����A���̉����x��vel.x�̐�Βl��speed�ɂȂ�܂ňێ�����B
    //acc��9��default
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

    //speed�܂�vel.x�������x��p���đ���������B���̉����x��acc�Őݒ�\�B
    //������dir�Ő���(��������Ȃ����1.0f)
    //speed��acc�͐�Βl��������
    //keepSpeed��true�ɂ����speed�ȏ�̑��x�ɂȂ��Ă��������Ȃ�
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

    //�������x��ݒ肷��֐�
    //�Ռ��ň�u�ł��̑��x�ɕς��悤�ȂƂ��Ɏg��
    public void impact(Vector2 imp)
    {
        vel.Set(imp.x, imp.y);
    }

    //�w�肵��x���W(posx)�܂Őݒ肵���ō����x(speed)�Ɖ����x(acc)�ɏ]����
    //���e����(torelance)�ɓ���܂Ői�ނ��߂̊֐�
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

    //�~�܂肽���ʒu�Ɏ~�܂��悤�ɉ����x��ݒ肷��֐�
    //�~�܂肽���ʒu�Ƒ��x��������x���v�Z
    public void stopAt(float posx)
    {
        float dis = posx - transform.position.x;

        if(dis != 0)
        {
            this.acc.x = -1 * (this.vel.x * this.vel.x / dis) / 2;
        }
    }

    //�}���Ɏ~�܂�B���x��0�ɂȂ�
    public void collide()
    {
        acc.x = 0;
        vel.x = 0;
    }

    //�c������collide
    public void collideH()
    {
        vel.y = 0;
    }

    public void set_gravity()
    {
        acc.y = -9.8f;
    }

    //���n���̏���
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
        //���ԕψʂ̎擾
        dt = Time.deltaTime;

        //�ʒu�v�Z
        vel += acc * dt;
        position += vel * dt;

        if(vel.x > 0) { dir = 1; }
        else if(vel.x < 0) { dir = -1; }
        else { dir = 0; }

        //���ۂ̈ʒu�ݒ�
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
