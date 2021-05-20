using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam_other : MonoBehaviour
{
    LineRenderer LineRenderer;
    Vector3 Pos_End;
    Vector3 Base_position;
    void Start()
    {
        LineRenderer = this.GetComponent<LineRenderer>();

        var positions = new Vector3[]{
        new Vector3(0, 0, 0),               // �J�n�_
        new Vector3(8, 0, 0),               // �I���_
        };

        LineRenderer.startWidth = 0.5f;                   // �J�n�_�̑�����0.1�ɂ���
        LineRenderer.endWidth = 0.5f;                     // �I���_�̑�����0.1�ɂ���

        // ���������ꏊ���w�肷��
        LineRenderer.SetPositions(positions);
    }


    void Update()
    {
        Vector3 Pos_base = transform.position;
        Vector3 Ditector = transform.forward;
        
        var positions = new Vector3[]{
        Base_position,               // �J�n�_
        transform.position + Ditector * 5,               // �I���_
        };

        LineRenderer.SetPositions(positions);
        LineRenderer.SetPosition(1, Pos_End);
    }

    public void Set_End(Vector3 end)
    {
        Pos_End = end;
    }

    public void Set_Base(Vector3 Base)
    {
        Base_position = Base;
    }
}
