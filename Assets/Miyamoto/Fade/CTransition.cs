
// //                          // //
// //   Author:�{�{ ����       // //
// //   ���ꉉ�o�t���t�F�[�h   // //
// //                          // //


// // �C���N���[�h�t�@�C���I�Ȃ�� // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// // �N���X // //
public class CTransition : UnityEngine.UI.Graphic, InterfaceFade
{
    // �}�X�N���郋�[���摜
    [SerializeField] private Texture MaskTexture = null;

    // �}�X�N�͈�
    [SerializeField, Range(0, 1)] private float CutoutRange;


    // �}�X�N�͈͂̓��o��
    public float Range
    {
        // �Q�b�^
        get
        {
            return CutoutRange;
        }

        // �Z�b�^
        set
        {
            CutoutRange = value;
            UpdateMaskCutout(CutoutRange);
        }
    }


    // ������
    protected override void Start()
    {
        // �e�̏��������Ă�
        base.Start();

        // �}�X�N�͈͂̍X�V
        UpdateMaskTexture(MaskTexture);
    }


    // �}�X�N�͈͂̍X�V
    private void UpdateMaskCutout(float range)
    {
        // ������悤�ɂ���
        enabled = true;

        // �}�e���A���͈̔͂̐ݒ�
        material.SetFloat("_Range", 1 - range);


        // �[���ȉ��Ō����Ȃ�����
        if (range <= 0)
        {
            this.enabled = false;
        }
    }


    public void UpdateMaskTexture(Texture texture)
    {
        // �e�N�X�`���ݒ�
        material.SetTexture("_MaskTex", texture);

        // �J���[�ݒ�
        material.SetColor("_Color", color);
    }


    // Unity�G�f�B�^�Ŏ��s���̎�
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        UpdateMaskCutout(Range);
        UpdateMaskTexture(MaskTexture);
    }
#endif
}
