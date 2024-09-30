using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //Ground状態を判別
    [SerializeField] private bool GroundFlag = false;
    public bool getGroundFlag() { return GroundFlag; }
    public void setGroundFlag(bool value) { GroundFlag = value; }

    //run状態を判別
    [SerializeField] private bool MoveFlag = false;
    public bool getMoveFlag() { return MoveFlag; }
    public void setMoveFlag(bool value) { MoveFlag = value; }

    //Jump状態を判別
    [SerializeField] private bool JumpFlag = false;
    public bool getJumpFlag() { return JumpFlag; }
    public void setJumpFlag(bool value) { JumpFlag = value; }

    //Dash状態を判別
    [SerializeField] private bool DashFlag = false;
    public bool getDashFlag() { return DashFlag; }
    public void setDashFlag(bool value) { DashFlag = value; }

    //何かにぶつかってるか判別
    [SerializeField] private bool CollideFlag = false;
    public bool getCollideFlag() { return CollideFlag; }
    public void setCollideFlag(bool value) { CollideFlag = value; }

    //死亡判定
    [SerializeField] private bool DeathFlag = false;
    public bool getDeathFlag() { return DeathFlag; }
    public void setDeathFlag(bool value) { DeathFlag = value; }

    //Dashの最高速度かどうか
    [SerializeField] private bool MaxDashFlag = false;
    public bool getMaxDashFlag() { return MaxDashFlag; }
    public void setMaxDashFlag(bool value) {  MaxDashFlag = value; }

    //落下状態かどうか
    [SerializeField] private bool FallingFlag = false;
    public bool getFallingFlag() {  return FallingFlag; }
    public void setFallingFlag(bool value) {  FallingFlag = value; }

    //パンチしてるか
    [SerializeField] private bool isPunch = false;
    public bool IsPunch() {  return isPunch; }
    public void setIsPunch(bool value) { isPunch = value; }

    //ぶつかった方向(dirは1 or 0 or -1)
    [SerializeField] private int CollideDir;
    public int getCollideDir() { return CollideDir; }
    public void noCollide() { CollideDir = 0; }
    public void forwardCollide() { CollideDir = 1; }
    public void backCollide() {  CollideDir = -1; }

    public float getTrueCollideDir() { return getCollideDir() * getDirection(); }

    //右側方向に向かうときにキャラが衝突するかどうか
    public bool checkRightCollide()
    {
        return getTrueCollideDir() == 1;
    }
    //左側方向に向かうときにキャラが衝突するかどうか
    public bool checkLeftCollide()
    {
        return getTrueCollideDir() == -1;
    }

    //キャラの向き
    [SerializeField] private int Direction;
    public int getDirection() { return Direction; }
    public void setDirForward() { Direction = 1; }
    public void setDirBack() { Direction = -1; }

    //キャラの通常移動速度
    [SerializeField] private float MoveSpeed = 6;
    public float getMoveSpeed() { return MoveSpeed; }

    //通常移動時の加速度
    [SerializeField] private float MoveAcc = 12;
    public float getMoveAcc() { return MoveAcc; }

    //キャラのダッシュ移動速度;
    [SerializeField] private float DashSpeed = 12;
    public float getDashSpeed() { return DashSpeed; }

    //キャラのダッシュ加速度
    [SerializeField] private float DashAcc = 18;
    public float getDashAcc() { return DashAcc; }

    //ジャンプの限界回数
    [SerializeField] private int MaxJumpNum = 3;
    public int getMaxJumpNum() { return MaxJumpNum; }

    //ジャンプの高さ
    [SerializeField] private float JumpHeight;
    public float getJumpHeight() {  return JumpHeight; }

    //punchの移動距離
    [SerializeField] private float PunchLong;
    public float getPunchLong() { return PunchLong; }

    //HP
    [SerializeField] public int HitPoint;
    public void SetHP(int value) { HitPoint = value; }
    public int GetHP() { return HitPoint; }

    [SerializeField] private int footDamage;
    public int getFootDamage() {  return footDamage; }

    //ジャンプした回数（着地するまで）
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
