// �M�~�b�N���쎞�L�����Z���҂���

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_state : MonoBehaviour
{
	// �Q��
	public miya_player_move sc_move;
	public miya_forword		sc_forword;
	public miya_check		sc_check;

	// ��
	public enum e_PlayerAnimationState
	{
		WAITING,        // �ҋ@
		WAITING_TOWER,	// �^���[����
		WALKING,		// ����
		ABANDONED,		// ���u
		RUNNING,		// ����
		CLIMBING,		// �悶�o��
		PUSH_WAITING,	// �����ҋ@
		PUSH_PUSHING,	// ����
		PULL_WAITING,	// �����ҋ@
		PULL_PULLING,	// ����
		LEVER_WAITING,	// ���o�[�ҋ@
		LEVER_RIGHT,	// ���o�[�E
		LEVER_LEFT,		// ���o�[��
		HOVERING,		// ��
		LANDING,		// ���n
	}

	// �ϐ�
	Rigidbody Rigid;
	[SerializeField]
	int		m_AnimationState	= (int)e_PlayerAnimationState.WAITING;
	[SerializeField]
	bool	m_CanAction			= true;
	//bool	m_IsClockwise		= true;
	[SerializeField]
	bool	m_CanClimb_forword	= false;
	[SerializeField]
	bool	m_CanClimb_check	= false;
	private bool IsBlock = false;
	private bool IsStage = false;

	// ���c�N�p('��')
	public GameObject m_parent;

	// �f�o�b�O�p
	int state_past = (int)e_PlayerAnimationState.WAITING;

	// ������
	void Start()
	{
		// Rigidbody�擾
		Rigid = this.GetComponent<Rigidbody>();
	}

	// �X�V
	void Update()
	{
		if
		(
			m_AnimationState == (int)e_PlayerAnimationState.LEVER_WAITING	||
			m_AnimationState == (int)e_PlayerAnimationState.LEVER_RIGHT		||
			m_AnimationState == (int)e_PlayerAnimationState.LEVER_LEFT
		)
		{
			
		}

		// �f�o�b�O
		if (state_past != m_AnimationState)
		{
			state_past = m_AnimationState;
			//Debug.Log("Animation State�F" + m_AnimationState);
		}
		// ���X�؃f�o�b�O�p
		Debug.Log("Horizontal_p�F" + Input.GetAxis("Horizontal_p"));
		Debug.Log("Vertical_p�F" + Input.GetAxis("Vertical_p"));
		Debug.Log("Horizontal_c�F" + Input.GetAxis("Horizontal_c"));
		Debug.Log("Change_c�F" + Input.GetAxisRaw("Change_c"));
		Debug.Log("OK�F" + Input.GetButton("OK"));
		Debug.Log("NO�F" + Input.GetButton("NO"));
		Debug.Log("Run�F" + Input.GetButton("Run"));
		Debug.Log("Climb�F" + Input.GetButton("Climb"));
		Debug.Log("Menu�F" + Input.GetButton("Menu"));

		// �A�N�V�����\
		if ( m_CanAction )
		{
			// �������Ă��Ȃ�
			m_AnimationState = (int)e_PlayerAnimationState.WAITING;

			// ���s
			if
			(
			Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
			Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
			)
			{
				m_AnimationState = (int)e_PlayerAnimationState.WALKING;
			}
			// �Q�[���p�b�h// ���c�N�p2// camera_move�ɂ������悤�ɕύX����̂Œ��ӂ��肢���܂�(�L��`)
			else if (Mathf.Abs(Input.GetAxis("Vertical_p")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal_p")) > 0)
			{
				m_AnimationState = (int)e_PlayerAnimationState.WALKING;
			}

			// �f�o�b�O
			//Debug.Log("F : " + m_CanClimb_forword);
			//Debug.Log("C : " + m_CanClimb_check);

			// �悶�o��
			// ���c�N�p2 || �Ōq���Ă�̂��ȉ��ɕ�������܂��I
			if (Input.GetKey(KeyCode.Space) || Input.GetButton("Climb"))
			{
				// �o�����̂������
				if (m_CanClimb_forword && !m_CanClimb_check)
				{
					m_AnimationState	= (int)e_PlayerAnimationState.CLIMBING;
					m_CanAction			= false;

					// ���c�N�p('��')���[�v�����炢��Ȃ��Ȃ�����
					//Rigid.useGravity	= false;

					sc_move.Set_StartPosition(this.transform.position);
				}
			}

			// �쓮
			if (Input.GetKey(KeyCode.J) || Input.GetButton("OK"))
			{
				// �Ώۂɂ���ăX�e�[�g�ύX
				// �u���b�N
				if (IsBlock)
				{
					m_AnimationState = (int)e_PlayerAnimationState.PUSH_WAITING;
					m_CanAction = false;

					// ���c�N�p('��')
					// �u���b�N���v���C���[�̎q��
					if (sc_forword.Get_Block())
					{
						sc_forword.Get_Block().transform.parent = this.transform;
						// �v���C���[�𒆐S���̎q��
						//Vector3 pos = this.transform.position;
						//pos += this.transform.forward * 1.0f;
						//pos.y = this.transform.position.y;
						//m_parent.transform.position = pos;
						this.transform.parent = m_parent.transform;
					}
				}
			}
		}//m_CanAction
		else
		{
			// ����
			if (m_AnimationState == (int)e_PlayerAnimationState.PUSH_WAITING)
			{
				if
				(
				Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
				Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
				)
				{
					m_AnimationState = (int)e_PlayerAnimationState.PUSH_PUSHING;
				}
				// �Q�[���p�b�h// ���c�N�p2
				else if (Mathf.Abs(Input.GetAxis("Vertical_p")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal_p")) > 0)
				{
					m_AnimationState = (int)e_PlayerAnimationState.PUSH_PUSHING;
				}
			}
			else if (m_AnimationState == (int)e_PlayerAnimationState.PUSH_PUSHING)
			{
				if
				(
				!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) &&
				!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)
				)
				{
					m_AnimationState = (int)e_PlayerAnimationState.PUSH_WAITING;
				}
				// �Q�[���p�b�h// ���c�N�p2
				else if (Mathf.Abs(Input.GetAxis("Vertical_p")) == 0 && Mathf.Abs(Input.GetAxis("Horizontal_p")) == 0)
				{
					m_AnimationState = (int)e_PlayerAnimationState.PUSH_WAITING;
				}
			}
		}

		// �L�����Z��
		if (Input.GetKey(KeyCode.K) || Input.GetButton("NO"))
		{
			// �Y������`�F�b�N
			if
			(
				m_AnimationState == (int)e_PlayerAnimationState.PUSH_WAITING ||
				m_AnimationState == (int)e_PlayerAnimationState.PUSH_PUSHING ||
				m_AnimationState == (int)e_PlayerAnimationState.PULL_WAITING ||
				m_AnimationState == (int)e_PlayerAnimationState.PULL_PULLING ||
				m_AnimationState == (int)e_PlayerAnimationState.LEVER_WAITING ||
				m_AnimationState == (int)e_PlayerAnimationState.LEVER_RIGHT
			)
			{
				m_AnimationState = (int)e_PlayerAnimationState.WAITING;
				m_CanAction = true;

				if (sc_forword.Get_Block())
				{
					sc_forword.Get_Block().transform.parent = null;
					this.transform.parent = null;
				}
			}
		}
	}

	// ����X�V
	void FixedUpdate()
	{

	}

	public void Set_CanAction(bool _can)
	{
		m_CanAction = _can;
	}
	public void Set_AnimationState(e_PlayerAnimationState _state)
	{
		m_AnimationState = (int)_state;
	}
	public void Set_CanClimb_Forword(bool _can)
	{
		m_CanClimb_forword = _can;
	}
	public void Set_CanClimb_Check(bool _can)
	{
		m_CanClimb_check = _can;
	}
	public void Set_IsBlock(bool _is)
	{
		IsBlock = _is;
	}
	public void Set_IsStage(bool _is)
	{
		IsStage = _is;
	}

	public int Get_AnimationState()
	{
		return m_AnimationState;
	}
	public bool Get_CanAction()
	{
		return m_CanAction;
	}
	public bool Get_IsBlock()
	{
		return IsBlock;
	}
	public bool Get_IsStage()
	{
		return IsStage;
	}

	public GameObject Get_Parent()
	{
		return m_parent;
	}
}
