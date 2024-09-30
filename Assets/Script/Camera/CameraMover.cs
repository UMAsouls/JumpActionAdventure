using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    //�v���C���[�I�u�W�F�N�g
    private GameObject player;

    //�v���C���[�̈ʒu�x�N�g��
    private Vector2 playerPos;

    //�v���C���[�܂ł̋������x�N�g����
    public Vector2 dis;

    public float acceleration;

    //���g��mover
    private Mover mover;

    //�v���C���[�̏�ԃX�N���v�g
    private PlayerStatus playerStatus;

    [SerializeField] private float prangeY;
    [SerializeField] private float nrangeY;


    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("test_chara2");

        playerStatus = player.GetComponent<PlayerStatus>();

        playerPos = player.transform.position - transform.position;

        mover = GetComponent<Mover>();

        dis = player.transform.position - (transform.position + (Vector3)playerPos);
    }

    // Update is called once per frame
    void Update()
    {
        float speed;
        float acc;

        dis = player.transform.position - (transform.position + (Vector3)playerPos);

        if (playerStatus.getDashFlag())
        {
            speed = playerStatus.getDashSpeed();
            acc = playerStatus.getDashAcc()-3;
        }
        else if (playerStatus.getMoveFlag())
        {
            speed = playerStatus.getMoveSpeed();
            acc = playerStatus.getMoveAcc();
        }
        else
        {
            if(dis.magnitude > 3.0f)
            {
                speed = 30;
                acc = 12;
            }
            else
            {
                speed = 8;
                acc = 3;
            }
        }


        mover.moveTo(player.transform.position.x - playerPos.x, speed, acc, 0.5f);


        //�J�����̏㉺�ړ�
        if(dis.y > prangeY)
        {
            float y = transform.position.y + (dis.y - prangeY);
            Vector2 set = new Vector2(transform.position.x, y);
            mover.setPos(set);
        }
        else if(dis.y < -1*nrangeY)
        {
            float y = transform.position.y + (dis.y + nrangeY);
            Vector2 set = new Vector2(transform.position.x, y);
            if (set.y < 0) { set = new Vector2(transform.position.x, 0f); }
            mover.setPos(set);
        }

    }
}
