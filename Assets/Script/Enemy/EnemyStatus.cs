using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private float WalkSpeed;
    public float getWalkSpeed() { return  WalkSpeed; }

    [SerializeField] private float WalkAcc;
    public float getWalkAcc() { return WalkAcc;}

    [SerializeField] private float Direction;
    public float getDirection() { return Direction; }
    public void setDirRight() { Direction = 1; }
    public void setDirLeft() { Direction = -1; }

    //FLAG(Ground)
    [SerializeField] private bool GroundFlag;
    public bool getGroundFlag() { return GroundFlag; }
    public void setGroundFlag(bool value) { GroundFlag = value; }

    //何かにぶつかってるか判別
    [SerializeField] private bool CollideFlag = false;
    public bool getCollideFlag() { return CollideFlag; }
    public void setCollideFlag(bool value) { CollideFlag = value; }

    //ぶつかった方向(dirは1 or 0 or -1)
    [SerializeField] private int CollideDir;
    public int getCollideDir() { return CollideDir; }
    public void noCollide() { CollideDir = 0; }
    public void forwardCollide() { CollideDir = 1; }
    public void backCollide() { CollideDir = -1; }

    //ぶつかった際に与えるダメージ
    [SerializeField] private int CollideDamage;
    public int getCollideDamage() {  return CollideDamage; }

    //HP
    [SerializeField] private int HP;
    public int getHP() { return HP; }
    public void setHP(int v) { HP = v; }
    public void damage(int v) { HP -= v; }

    //ダメージ後無敵状態か
    [SerializeField] private bool damaged;
    public bool getDamaged() { return damaged; }
    public void setDamaged(bool value) { damaged = value; }

    //ダメージ後の無敵時間
    [SerializeField] private float DamageCount;

    //倒したときのスコア
    [SerializeField] private int score;
    public int getScore() { return score; }

    //うつってるかどうか(出現処理に必要)
    private bool visible;
    public bool getVisible() { return visible; }
    public void setVisible(bool value) { visible = value; }

    private float count;
    private float max;

    // Start is called before the first frame update
    void Start()
    {
        GroundFlag = false;
        count = 0;
        max = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(getDamaged())
        {
            max = DamageCount;
        }

        if(max > 0)
        {
            count += Time.deltaTime;
            if(count > max)
            {
                count = 0;
                setDamaged(false);
            }
        }
    }
}
