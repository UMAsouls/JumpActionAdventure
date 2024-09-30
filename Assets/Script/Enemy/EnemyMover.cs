using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private Mover mover;
    private EnemyStatus status;

    public void collide() { mover.collide(); }
    public void landing() { mover.landing(); }


    public void walk()
    {
        if (status.getCollideDir() != 1)
        {
            mover.speed_up_to(status.getWalkSpeed(), status.getWalkSpeed(), status.getDirection(), true);
        }
    }

    public void knockBack(int level, float h)
    {
        Vector2 k;
        float dir = mover.getDir();
        if (dir == 0) { dir = status.getDirection(); }

        if (level == 1)
        {
            k = new Vector2(6 * dir * -1, 3*h);
        }
        else
        {
            k = new Vector2(7 * dir * -1, 8*h);
        }

        mover.impact(k);

    }

    public void knockBack(int level, float dir, float h)
    {
        Vector2 k;
        if (level == 1) { k = new Vector2(6 * dir , 3*h); }
        else { k = new Vector2(7 * dir , 8*h); }
        mover.impact(k);

    }

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<Mover>();
        status = GetComponent<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if(status.getGroundFlag()) { mover.ground_set(); }
        else { mover.air_set(); }
    }
}
