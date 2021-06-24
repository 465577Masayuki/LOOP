using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class THE_WORLD : MonoBehaviour
{
    public int USE_BUTTON_NUM;  //�X�e�[�W�ɂ���{�^���̐�
    public Button[] bs = new Button[5]; //�{�^���X�N���v�g�i�[��
    bool[] USE_FLAG = new bool[5];      //�e�{�^���̓�����

    private GameObject FloorOne;
    private GameObject FloorTwo;
    private GameObject FloorThree;

    // Start is called before the first frame update
    void Start()
    {
        FloorOne = GameObject.Find("FloorOne");
        FloorTwo = GameObject.Find("FloorTwo");
        FloorThree = GameObject.Find("FloorThree");

        for (int i = 0 ; i < 5 ; i++)
        {
            USE_FLAG[i] = false;
        }

        CFadeManager.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void world_stop()
    {
        for(int i = 0; i< USE_BUTTON_NUM;i++)
        {
            bs[i].Set_stop();
        }

        FloorOne.GetComponent<SPIN_FloorOne>().Set_No_SPIN(true);
        FloorTwo.GetComponent<SPIN_FloorOne>().Set_No_SPIN(true);
        FloorThree.GetComponent<SPIN_FloorOne>().Set_No_SPIN(true);
    }

    public void world_start()
    {
        for (int i = 0; i < USE_BUTTON_NUM; i++)
        {
            bs[i].Clare_stop();
        }

        FloorOne.GetComponent<SPIN_FloorOne>().Set_No_SPIN(false);
        FloorTwo.GetComponent<SPIN_FloorOne>().Set_No_SPIN(false);
        FloorThree.GetComponent<SPIN_FloorOne>().Set_No_SPIN(false);
    }

    public void SET_USE(Button BS)
    {
        for (int i = 0; i < USE_BUTTON_NUM; i++)
        {
            if(BS == bs[i])
            {
                USE_FLAG[i] = true;
            }
        }
    }

    public void END_USE(Button BS)
    {
        for (int i = 0; i < USE_BUTTON_NUM; i++)
        {
            if (BS == bs[i])
            {
                USE_FLAG[i] = false;
            }
        }

        for (int i = 0; i < USE_BUTTON_NUM; i++)
        {
            if(USE_FLAG[i])
            {
                //�v���C���[�ɓ��쒆���Z�b�g����
                return; //�������f
            }

            //�S���̃{�^��������I�t�Ȃ�
            //�v���C���[�ɓ��슮�����Z�b�g����
            
        }
    }
}
