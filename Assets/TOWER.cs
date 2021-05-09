using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOWER : MonoBehaviour
{
    public GameObject hole1;
    public GameObject hole2;
    public GameObject hole1_base;
    public GameObject hole2_base;
    public GameObject hole3_base;

    int pos_hole1;  //���P�̍���
    int pos_hole2;  //���Q�̍���
    int pos_hole_b; //�ύX�O�̍���
    int rot_hole1;  //���P�̉�]
    int rot_hole2;  //���Q�̉�]
    Vector3 moveVec;

    int MOVE;
    int SPIN;

    int count;  //�ړ��A��]�����J�E���g

    // Start is called before the first frame update
    void Start()
    {
        MOVE = 0;
        SPIN = 0;
        pos_hole1 = 1;
        pos_hole2 = 2;
        rot_hole1 = 1;
        rot_hole2 = 4;
        moveVec = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //���P�̍����ύX
        if (Input.GetKey(KeyCode.J) && MOVE == 0 && SPIN == 0)
        {
            MOVE = 1;
            count = 90;



            //�ʒu����Ɉړ�
            pos_hole1++;

            //����𒴂������ԉ���
            if(pos_hole1 == 4)
            {
                pos_hole1 = 1;
            }
            
            //���������Ō��̍������d�Ȃ�ꍇ�͂�����i�i�߂�
            if(pos_hole1 == pos_hole2 && rot_hole1 == rot_hole2)
            {
                pos_hole1++;
            }
            if (pos_hole1 == 4)
            {
                pos_hole1 = 1;
            }




        }
        */
        /*
        //���Q�̍����ύX
        if (Input.GetKey(KeyCode.K) && MOVE == 0 && SPIN == 0)
        {
            MOVE = 2;
            count = 90;
        }

        //���P�̌����ύX
        if (Input.GetKey(KeyCode.U) && MOVE == 0 && SPIN == 0)
        {
            SPIN = 1;
            count = 90;
        }

        //���Q�̌����ύX
        if (Input.GetKey(KeyCode.I) && MOVE == 0 && SPIN == 0)
        {
            SPIN = 2;
            count = 90;
        }
        */
    }

    void FixedUpdate()
    {
        if (count > 0)
        {
            if (MOVE == 1)
            {
                hole1.transform.position += moveVec;
            }

            if (MOVE == 2)
            {
                hole2.transform.position += moveVec;
            }

            if (SPIN == 1)
            {
                hole1_base.transform.Rotate(0, 90 / 5, 0);
            }

            if (SPIN == 2)
            {
                hole2_base.transform.Rotate(0, 90 / 5, 0);
            }

            if (SPIN == 3)
            {
                hole3_base.transform.Rotate(0, 90 / 5, 0);
            }

            count--;
        }

        if(count > 0)
        {
            count--;

            if(MOVE != 0 && count == 0)
            {
                MOVE = 0;
            }

            if (SPIN != 0 && count == 0)
            {
                SPIN = 0;
            }
        }

    }

    //���P�̍����ύX
    public void HoleMove_1()
    {
        if(MOVE !=0 || count != 0)
        {
            return;
        }

        //�ʒu����Ɉړ�
        pos_hole_b = pos_hole1;
        pos_hole1++;

        //����𒴂������ԉ���
        if (pos_hole1 == 4)
        {
            pos_hole1 = 1;
        }

        //���������Ō��̍������d�Ȃ�ꍇ�͂�����i�i�߂�
        //if (pos_hole1 == pos_hole2 && rot_hole1 == rot_hole2)
        if (pos_hole1 == pos_hole2)
        {
            pos_hole1++;
        }

        //����ς蒴�����牺�i��
        if (pos_hole1 == 4)
        {
            pos_hole1 = 1;
        }

        //��i���
        if(pos_hole1 == pos_hole_b + 1)
        {
            moveVec.y = 0.08f;
        }
        //��i���
        else if(pos_hole1 == pos_hole_b + 2)
        {
            moveVec.y = 0.16f;
        }
        //��i����
        else if(pos_hole1 == pos_hole_b - 1)
        {
            moveVec.y = -0.08f;
        }
        //��i����
        else if (pos_hole1 == pos_hole_b - 2)
        {
            moveVec.y = -0.16f;
        }

        MOVE = 1;
        count = 50;

    }

    //���Q�̍����ύX
    public void HoleMove_2()
    {
        if (MOVE != 0 || count != 0)
        {
            return;
        }

        //�ʒu����Ɉړ�
        pos_hole_b = pos_hole2;
        pos_hole2++;

        //����𒴂������ԉ���
        if (pos_hole2 == 4)
        {
            pos_hole2 = 1;
        }

        //���������Ō��̍������d�Ȃ�ꍇ�͂�����i�i�߂�
        //if (pos_hole1 == pos_hole2 && rot_hole1 == rot_hole2)
        if (pos_hole2 == pos_hole1)
        {
            pos_hole2++;
        }

        //����ς蒴�����牺�i��
        if (pos_hole2 == 4)
        {
            pos_hole2 = 1;
        }

        //��i���
        if (pos_hole2 == pos_hole_b + 1)
        {
            moveVec.y = 0.08f;
        }
        //��i���
        else if (pos_hole2 == pos_hole_b + 2)
        {
            moveVec.y = 0.16f;
        }
        //��i����
        else if (pos_hole2 == pos_hole_b - 1)
        {
            moveVec.y = -0.08f;
        }
        //��i����
        else if (pos_hole2 == pos_hole_b - 2)
        {
            moveVec.y = -0.16f;
        }

        MOVE = 2;
        count = 50;
    }

    //���P�̌����ύX
    public void HoleSpin_1()
    {
        if (SPIN != 0 || count != 0)
        {
            return;
        }

        if (pos_hole1 == 1)
        {
            SPIN = 1;
        }
        else if(pos_hole1 == 2)
        {
            SPIN = 2;
        }
        else if (pos_hole1 == 3)
        {
            SPIN = 3;
        }

        count = 50;
    }

    //���Q�̌����ύX
    public void HoleSpin_2()
    {
        if (SPIN != 0 || count != 0)
        {
            return;
        }

        if (pos_hole2 == 1)
        {
            SPIN = 1;
        }
        else if (pos_hole2 == 2)
        {
            SPIN = 2;
        }
        else if (pos_hole2 == 3)
        {
            SPIN = 3;
        }

        count = 50;
    }
}
