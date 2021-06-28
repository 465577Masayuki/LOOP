
// //                              // //
// //   Author�F�{�{�@����         // //
// //   ���j���[�̃J�[�\������     // //
// //                              // //

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ------------------------------------------------------------------------------------------

public class UI_Menu : MonoBehaviour
{
    public bool Show;       // ���j���[�������邩�ۂ�
    public bool NotUSE;     // �g�p�֎~���
    Transform Commands;     // �q�I�u�W�F�N�g�i���j���[�ƃJ�[�\���S�̓�������I�u�W�F�N�g�j�̃g�����X�t�H�[��

    // ------------------------------------------------------------------------------------------
    void Start()
    {
        // �S�̓����q�I�u�W�F�N�g�擾
        if (LanguageSetting.Get_Is_Japanese())
        {
            Commands = GameObject.Find("CommandsEnglishVer").transform;
            SetAllInactive();
            Commands = GameObject.Find("Commands").transform; 
        }
        else
        {
            Commands = GameObject.Find("Commands").transform;
            SetAllInactive();
            Commands = GameObject.Find("CommandsEnglishVer").transform;

        }
        
        
        Show = false;                                       // �ŏ��͌����Ȃ��悤�ɂ���
        NotUSE = false;
    }

    // ------------------------------------------------------------------------------------------
    
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetButton("MENU"))    // ���j���[�L�[�i�K�X�ύX���Ă��������B�j
        {
            if(Show)
            {
                Show = false;   // �����Ȃ�
            }
            else
            {
                Show = true;    // ������
            }
        }
        */

        if(Show)    // ������Ƃ��A�S�̓����I�u�W�F�N�g�̎q�I�u�W�F�N�g�����ׂăA�N�e�B�u�ɂ���
        {
            if (!NotUSE)
            {
                foreach (Transform t in Commands)
                {
                    if (!t.gameObject.activeSelf)
                    {
                        t.gameObject.SetActive(true);
                    }
                }
            }
        }
        else       // �����Ȃ��Ƃ��A�S�̓����I�u�W�F�N�g�̎q�I�u�W�F�N�g�����ׂĔ�A�N�e�B�u�ɂ���
        {
            foreach(Transform t in Commands)
            {
                if(t.gameObject.activeSelf)
                {
                    t.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetShow()
    {
        if (Show)
        {
            Show = false;   // �����Ȃ�
        }
        else
        {
            Show = true;    // ������
        }
    }

    public void SetNot()
    {
        NotUSE = true;
    }

    //�q�I�u�W�F�N�g�����ׂĔ�A�N�e�B�u�ɂ���
    private void SetAllInactive()
    {
        foreach (Transform t in Commands)
        {
            if (t.gameObject.activeSelf)
            {
                t.gameObject.SetActive(false);
            }
        }
    }
    //�S�̓����I�u�W�F�N�g�̎q�I�u�W�F�N�g�����ׂăA�N�e�B�u�ɂ���
    private void SetAllActive()
    {
        foreach (Transform t in Commands)
        {
            if (!t.gameObject.activeSelf)
            {
                t.gameObject.SetActive(true);
            }
        }
    }
}
