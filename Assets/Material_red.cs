using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material_red : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //�ύX�������F
        Color setColor = new Color(0.0f, 0f, 0f);

        //A���R�s�[
        //GameObject targetB = Instantiate(targetA);

        //�Ώۂ̃V�F�[�_�[�����擾
        Shader sh = this.GetComponent<MeshRenderer>().material.shader;

        //�擾�����V�F�[�_�[�����ɐV�����}�e���A�����쐬
        Material mat = new Material(sh);

        //�쐬�����}�e���A���̐F��ύX
        mat.color = setColor;

        //�ΏۃI�u�W�F�N�g�Ɋ��蓖�Ă�
        this.GetComponent<MeshRenderer>().material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
