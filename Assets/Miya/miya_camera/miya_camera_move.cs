using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_camera_move : MonoBehaviour
{
	// �ϐ�
	float Degree = -180;
	public float Speed_Rotate = 30.0f;
	float Length_FromCenter;
	float Height;

    // ������
    void Start()
    {
		// �����l�擾
		Length_FromCenter = Mathf.Abs(this.transform.position.z);
		Height = this.transform.position.y;
	}

    // ����X�V
    void FixedUpdate()
    {
		// �ړ�
		{
			// ����
			if (Input.GetKey(KeyCode.LeftArrow)	) Degree += Speed_Rotate * Time.deltaTime;
			if (Input.GetKey(KeyCode.RightArrow)) Degree -= Speed_Rotate * Time.deltaTime;

			// �ړ�
			Vector3 result = new Vector3(0, 0, 0);
			result.x = Mathf.Sin(Degree * Mathf.Deg2Rad) * Length_FromCenter;
			result.z = Mathf.Cos(Degree * Mathf.Deg2Rad) * Length_FromCenter;
			result.y = Height;
			this.transform.position = result;
		}
	}
}
