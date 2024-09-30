using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //Ground��Ԃ𔻕�
    [SerializeField] private bool GroundFlag = false;
    public bool getGroundFlag() { return GroundFlag; }
    public void setGroundFlag(bool value) { GroundFlag = value; }

    //run��Ԃ𔻕�
    [SerializeField] private bool MoveFlag = false;
    public bool getMoveFlag() { return MoveFlag; }
    public void setMoveFlag(bool value) { MoveFlag = value; }

    //Jump��Ԃ𔻕�
    [SerializeField] private bool JumpFlag = false;
    public bool getJumpFlag() { return JumpFlag; }
    public void setJumpFlag(bool value) { JumpFlag = value; }

    //Dash��Ԃ𔻕�
    [SerializeField] private bool DashFlag = false;
    public bool getDashFlag() { return DashFlag; }
    public void setDashFlag(bool value) { DashFlag = value; }

    //�����ɂԂ����Ă邩����
    [SerializeField] private bool CollideFlag = false;
    public bool getCollideFlag() { return CollideFlag; }
    public void setCollideFlag(bool value) { CollideFlag = value; }

    //���S����
    [SerializeField] private bool DeathFlag = false;
    public bool getDeathFlag() { return DeathFlag; }
    public void setDeathFlag(bool value) { DeathFlag = value; }

    //Dash�̍ō����x���ǂ���
    [SerializeField] private bool MaxDashFlag = false;
    public bool getMaxDashFlag() { return MaxDashFlag; }
    public void setMaxDashFlag(bool value) {  MaxDashFlag = value; }

    //������Ԃ��ǂ���
    [SerializeField] private bool FallingFlag = false;
    public bool getFallingFlag() {  return FallingFlag; }
    public void setFallingFlag(bool value) {  FallingFlag = value; }

    //�p���`���Ă邩
    [SerializeField] private bool isPunch = false;
    public bool IsPunch() {  return isPunch; }
    public void setIsPunch(bool value) { isPunch = value; }

    //�Ԃ���������(dir��1 or 0 or -1)
    [SerializeField] private int CollideDir;
    public int getCollideDir() { return CollideDir; }
    public void noCollide() { CollideDir = 0; }
    public void forwardCollide() { CollideDir = 1; }
    public void backCollide() {  CollideDir = -1; }

    public float getTrueCollideDir() { return getCollideDir() * getDirection(); }

    //�E�������Ɍ������Ƃ��ɃL�������Փ˂��邩�ǂ���
    public bool checkRightCollide()
    {
        return getTrueCollideDir() == 1;
    }
    //���������Ɍ������Ƃ��ɃL�������Փ˂��邩�ǂ���
    public bool checkLeftCollide()
    {
        return getTrueCollideDir() == -1;
    }

    //�L�����̌���
    [SerializeField] private int Direction;
    public int getDirection() { return Direction; }
    public void setDirForward() { Direction = 1; }
    public void setDirBack() { Direction = -1; }

    //�L�����̒ʏ�ړ����x
    [SerializeField] private float MoveSpeed = 6;
    public float getMoveSpeed() { return MoveSpeed; }

    //�ʏ�ړ����̉����x
    [SerializeField] private float MoveAcc = 12;
    public float getMoveAcc() { return MoveAcc; }

    //�L�����̃_�b�V���ړ����x;
    [SerializeField] private float DashSpeed = 12;
    public float getDashSpeed() { return DashSpeed; }

    //�L�����̃_�b�V�������x
    [SerializeField] private float DashAcc = 18;
    public float getDashAcc() { return DashAcc; }

    //�W�����v�̌��E��
    [SerializeField] private int MaxJumpNum = 3;
    public int getMaxJumpNum() { return MaxJumpNum; }

    //�W�����v�̍���
    [SerializeField] private float JumpHeight;
    public float getJumpHeight() {  return JumpHeight; }

    //punch�̈ړ�����
    [SerializeField] private float PunchLong;
    public float getPunchLong() { return PunchLong; }

    //HP
    [SerializeField] public int HitPoint;
    public void SetHP(int value) { HitPoint = value; }
    public int GetHP() { return HitPoint; }

    [SerializeField] private int footDamage;
    public int getFootDamage() {  return footDamage; }

    //�W�����v�����񐔁i���n����܂Łj
    [SerializeField] private int JumpNum = 0;
    public int getJumpNum() { return JumpNum; }
    public bool JumpCheck() { return JumpNum < MaxJumpNum; }
    public void addJumpNum() { if (JumpCheck()) { JumpNum++; } }
    public void resetJumpNum() { JumpNum = 0; }

    // Start is called before the first frame update
    void Start()
    {
        GroundFlag = false;
        MoveFlag = false;
        JumpFlag = false;
        DashFlag = false;
        MaxDashFlag = false;
        CollideFlag = false;

        CollideDir = 0;
        Direction = -1;

        resetJumpNum();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
