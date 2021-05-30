
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
    RectTransform thisTransform;    // ���g�̃��N�g�g�����X�t�H�[���i���W�ύX�p)
    int CursorPosition;             // �J�[�\���ʒu�i���h���̖��j

    GameObject Parent;              // �e�I�u�W�F�N�g�iMenuCanvas�j�̂��߂̓��ꕨ
    UI_Menu ParentMenu;             // �e�I�u�W�F�N�g�ɂ����X�N���v�g���~����


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
    }


    // ------------------------------------------------------------------------------------------

    void Update()
    {
        // �J�[�\���ړ�
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CursorPosition++;
            if (CursorPosition > (int)CursorPos.Return)
            {
                CursorPosition = (int)CursorPos.Retry;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (CursorPosition)
            {
                case (int)CursorPos.Retry:
                    ParentMenu.Show = false;                                        // ���j���[�������Ȃ��悤�ɂ���
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);     // ���݃V�[����Ǎ����Ȃ���
                    break;

                case (int)CursorPos.Stage:
                    ParentMenu.Show = false;        // ���j���[�������Ȃ��悤�ɂ���
                    // �X�e�[�W�I����ʃV�[���֑J��
                    break;

                case (int)CursorPos.Return:
                    ParentMenu.Show = false;        // ���j���[�������Ȃ��悤�ɂ���
                    break;

                default:
                    break;
            }
        }
    }
}