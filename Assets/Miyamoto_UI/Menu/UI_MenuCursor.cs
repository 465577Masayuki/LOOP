
// //                              // //
// //   Author�F�{�{�@����         // //
// //   ���j���[�̏���             // //
// //                              // //

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ------------------------------------------------------------------------------------------

public class UI_MenuCursor : MonoBehaviour
{
    public Fade fade;
    public Player_State ps;
    RectTransform thisTransform;    // ���g�̃��N�g�g�����X�t�H�[���i���W�ύX�p)
    int CursorPosition;             // �J�[�\���ʒu�i���h���̖��j

    GameObject Parent;              // �e�I�u�W�F�N�g�iMenuCanvas�j�̂��߂̓��ꕨ
    UI_Menu ParentMenu;             // �e�I�u�W�F�N�g�ɂ����X�N���v�g���~����

    bool con_U; //�R���g���[���[���͍�
    bool con_D; //�R���g���[���[���͉E

    int keywait;

    // �J�[�\���̏ꏊ�ꗗ
    enum CursorPos
    {
        Retry,
        Stage,
        Return,
        end
    };

    // ------------------------------------------------------------------------------------------

    void Start()
    {
        thisTransform = GetComponent<RectTransform>();        // ���N�g�g�����X�t�H�[���擾
        CursorPosition = (int)CursorPos.Retry;                // �J�[�\�������ʒu����

        Parent = GameObject.Find("MenuCanvas");               // �e�I�u�W�F�N�g���擾
        ParentMenu = Parent.GetComponent<UI_Menu>();          // �e�I�u�W�F�N�g�ɂ����X�N���v�g���擾

        keywait = 0;
    }


    // ------------------------------------------------------------------------------------------

    void Update()
    {
        Check_Cont();

        // �J�[�\���ړ�
        if (Input.GetKeyDown(KeyCode.DownArrow) || con_D)
        {
            CursorPosition++;
            if (CursorPosition > (int)CursorPos.Return)
            {
                CursorPosition = (int)CursorPos.Retry;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || con_U)
        {
            CursorPosition--;
            if (CursorPosition < (int)CursorPos.Retry)
            {
                CursorPosition = (int)CursorPos.Return;
            }
        }
        
        // �J�[�\���ʒu�ɍ��킹�č��W��ύX
        switch (CursorPosition)
        {
            case (int)CursorPos.Retry:
                thisTransform.anchoredPosition = new Vector2(70.0f, 280.0f);
                break;

            case (int)CursorPos.Stage:
                thisTransform.anchoredPosition = new Vector2(70.0f, 100.0f);
                break;

            case (int)CursorPos.Return:
                thisTransform.anchoredPosition = new Vector2(70.0f, -60.0f);
                break;

            default:
                break;
        }


        // ���肪�����ꂽ��
        if (Input.GetKeyDown(KeyCode.J) || Input.GetButton("OK"))
        {
            switch (CursorPosition)
            {
                case (int)CursorPos.Retry:
                    ParentMenu.Show = false;                                        // ���j���[�������Ȃ��悤�ɂ���
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);     // ���݃V�[����Ǎ����Ȃ���
                    fade.SetOut();
                    fade.SetNext(1);
                    ParentMenu.SetNot();    //�J���Ȃ���Ԃ�
                    break;

                case (int)CursorPos.Stage:
                    ParentMenu.Show = false;        // ���j���[�������Ȃ��悤�ɂ���
                    fade.SetOut();
                    fade.SetNext(2);
                    ParentMenu.SetNot();    //�J���Ȃ���Ԃ�
                    // �X�e�[�W�I����ʃV�[���֑J��
                    break;

                case (int)CursorPos.Return:
                    ps.Menu_OFF();
                    ParentMenu.Show = false;        // ���j���[�������Ȃ��悤�ɂ���
                    break;

                default:
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if (keywait > 0)
        {
            keywait--;
        }
    }

    private void Check_Cont()
    {
        float UD;
        UD = Input.GetAxis("Vertical_p"); //��Ղ�

        con_U = false;
        con_D = false;

        if (UD > 0.5f && keywait == 0)
        {
            con_U = true;
            keywait = 25;
        }

        if (UD < -0.5f && keywait == 0)
        {
            con_D = true;
            keywait = 25;
        }
    }
}