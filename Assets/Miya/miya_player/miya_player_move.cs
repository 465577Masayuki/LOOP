using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_move : MonoBehaviour
{
	// �ϐ�
	[SerializeField] private GameObject Camera;                                                                       // �����I�ɕ����̃J�����̒�����A�N�e�B�u�Ȃ��̈��I�Ԃ��ƂɂȂ�
	[SerializeField] private float Speed_Move = 2.0f;
	Rigidbody Rigid;


    /// <�����{���ǉ�>
     
    //���j���[��ʂ�on,off���Ǘ�����ϐ�
    public bool UseMenu = false;

    //���j���[��ʂ̂̃C���[�W�I�u�W�F�N�g
    [SerializeField]
    public GameObject Image_MenuWindow;
    //���j���[��ʈȊO�𔼓����ɂ���p���l���I�u�W�F�N�g
    [SerializeField]
    public GameObject Image_PanelWindow;
    //�J�[�\���̈ړ��̑ҋ@����
    public int WaitCount = 0;


    // ������
    void Start()
	{
		// Rigidbody�擾
		Rigid = this.GetComponent<Rigidbody>();

		// �J�������ݒ莞
		if ( !Camera ) Debug.Log("�ymiya_player_move�zthere is no camera");
	}

	// ����X�V
	void FixedUpdate()
	{
		// �J�����x�N�g���擾
		Vector3 camera_front = Camera.transform.forward;
		Vector3 camera_right = Camera.transform.right;

		// �ړ�
		{
			// ����
			Vector3 direction_move = new Vector3(0, 0, 0);
			if (Input.GetKey(KeyCode.W)) direction_move += camera_front;
			if (Input.GetKey(KeyCode.S)) direction_move -= camera_front;
			if (Input.GetKey(KeyCode.D)) direction_move += camera_right;
			if (Input.GetKey(KeyCode.A)) direction_move -= camera_right;

            /// <�����{���ǉ�>

            WaitCount--;
            if(Input.GetKey(KeyCode.P) && !UseMenu && WaitCount < 0)
            {
                UseMenu = true;
                Image_PanelWindow.SetActive(true);
                Image_MenuWindow.SetActive(true);
                WaitCount = 30;
            }
            if (Input.GetKey(KeyCode.P) && UseMenu && WaitCount < 0)
            {
                UseMenu = false;
                Image_PanelWindow.SetActive(false);
                Image_MenuWindow.SetActive(false);
                WaitCount = 30;
            }

            /// <�����{���ǉ�>


            // ���K��
            if (direction_move != new Vector3(0, 0, 0))
			{
				// Y�������폜
				direction_move.y = 0;
				direction_move = direction_move.normalized * Speed_Move;// * Time.deltaTime;
				//Debug.Log(direction_move);
			}

			// �ړ�
            if(!UseMenu)�@//�����{���ǉ�
			Rigid.velocity = direction_move;
		}
	}
}
