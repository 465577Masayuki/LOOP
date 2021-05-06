using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_move : MonoBehaviour
{
	// �ϐ�
	[SerializeField] private GameObject Camera;                                                                       // �����I�ɕ����̃J�����̒�����A�N�e�B�u�Ȃ��̈��I�Ԃ��ƂɂȂ�
	[SerializeField] private float Speed_Move = 2.0f;
	[SerializeField] private float Speed_Fall = 4.0f;
	Rigidbody Rigid;
	private Vector3 Position_Latest_m;
	public float RotateSpeed = 5.0f;

	// ������
	void Start()
	{
		// Rigidbody�擾
		Rigid = this.GetComponent<Rigidbody>();
		// �ߋ��̈ʒu
		Position_Latest_m = this.transform.position;

		// �J�������ݒ莞
		if ( !Camera ) Debug.Log("�ymiya_player_move�zthere is no camera");
	}

	// ����X�V
	void FixedUpdate()
	{
		// �J�����x�N�g���擾
		Vector3 camera_front = Camera.transform.forward;
		Vector3 camera_right = Camera.transform.right;

		// �ړ�//�J�����̊O���̎��ɕs���R�����璼��
		{
			// ����
			Vector3 direction_move = new Vector3(0, 0, 0);
			if (Input.GetKey(KeyCode.W)) direction_move += camera_front;
			if (Input.GetKey(KeyCode.S)) direction_move -= camera_front;
			if (Input.GetKey(KeyCode.D)) direction_move += camera_right;
			if (Input.GetKey(KeyCode.A)) direction_move -= camera_right;

			// ���K��
			if (direction_move != new Vector3(0, 0, 0))
			{
				// Y�������폜
				direction_move.y = 0;
				direction_move = direction_move.normalized;// * Time.deltaTime;
			}

			// �ړ�
			Rigid.velocity = direction_move * Speed_Move;

			// ��]
			Vector3 difference = this.transform.position - Position_Latest_m;
			Position_Latest_m = this.transform.position;
			if (difference.magnitude > 0.001f)
			{
				// ��������
				//miya_player_state��m_AnimationState��HOVERING�ɐݒ�

				// ����
				Rigid.velocity = new Vector3(direction_move.x, -Speed_Fall, direction_move.z);

				// ����
				difference.y = 0;

				// ��]�v�Z
				Quaternion rot = Quaternion.LookRotation(difference);
				rot = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * RotateSpeed);
				this.transform.rotation = rot;
			}
		}
	}
}
