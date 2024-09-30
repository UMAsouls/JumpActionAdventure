using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyMover mover;
    private EnemyStatus status; 

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<EnemyMover>();
        status = GetComponent<EnemyStatus>();
    }

    private void SetDirToPlayer()
    {
        Vector2 p_pos = Gmanager.instance.getPlayerPos();
        if (p_pos.x - transform.position.x >= 0) { status.setDirRight(); }
        else { status.setDirLeft(); }
    }


    private void OnBecameVisible()
    {
        status.setVisible(true);
    }

    private void OnBecameInvisible()
    {
        status.setVisible(false);
    }



    public void onGround()
    {
        Debug.Log("E_g");
        if (!status.getGroundFlag())
        {
            mover.landing();
        }

        status.setGroundFlag(true);
    }

    public void exitGround()
    {
        status.setGroundFlag(false);
    }

    public void forwardOnCollisionEnter()
    {
        status.setCollideFlag(true);
        status.forwardCollide();
        mover.collide();
    }

    public void forwardOnCollisionExit()
    {
        status.setCollideFlag(false);
        status.noCollide();
    }

    public void backOnCollisionEnter()
    {
        status.setCollideFlag(true);
        status.backCollide();
        mover.collide();
    }

    public void backOnCollisionExit()
    {
        status.setCollideFlag(false);
        status.noCollide();
    }

    public int collideAttack()
    {
        return status.getCollideDamage();
    }

    public void forwardDamage(int d)
    {
        if(!status.getDamaged())
        {
            status.setDamaged(true);
            status.damage(d);
            mover.knockBack(1, -1 * status.getDirection());
        }
    }

    public void backDamage(int d)
    {
        if (!status.getDamaged())
        {
            status.setDamaged(true);
            status.damage(d);
            mover.knockBack(1, status.getDirection());
        }
    }

    public void damage(int da, int dir, float h = 1)
    {
        if (!status.getDamaged())
        {
            status.setDamaged(true);
            status.damage(da);
            mover.knockBack(1, dir, h);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetDirToPlayer();
        mover.walk();

        //Œü‚«
        transform.localScale = new Vector3(status.getDirection(), 1.0f, 1.0f);

        if (status.getHP() <= 0)
        {
            Destroy(gameObject, 0.2f);
        }
    }

    private void OnDestroy()
    {
        Gmanager.instance.removeEnemy(name);
    }
}
