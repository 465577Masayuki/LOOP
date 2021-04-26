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
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    void FixedUpdate()
    {
        if (count > 0)
        {
            if (MOVE == 1)
            {

            }

            if(SPIN == 1)
            {

            }

            count--;
        }

    }
}
