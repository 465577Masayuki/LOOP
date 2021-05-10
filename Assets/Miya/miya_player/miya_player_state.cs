using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_state : MonoBehaviour
{
	// �Q��
	public miya_player_move sc_move;

	// ��
	public enum e_PlayerAnimationState
	{
		WAITING,		// �ҋ@
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
	int		m_AnimationState	= (int)e_PlayerAnimationState.WAITING;
	bool	m_CanAction			= true;
	//bool	m_IsClockwise		= true;
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
		// �f�o�b�O
		if (state_past != m_AnimationState)
		{
			state_past = m_AnimationState;
			Debug.Log("Animation State�F" + m_AnimationState);
		}

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
				m_AnimationState = (int)e_PlayerAnimationState.WALKING;

			// �悶�o��
			if (Input.GetKey(KeyCode.Space))
			{
				// �o�����̂������
				if (true)
				{
					m_AnimationState = (int)e_PlayerAnimationState.CLIMBING;
					m_CanAction = false;

					Rigid.useGravity = false;

					sc_move.Set_StartPosition(this.transform.position);
				}
			}

			// �쓮
			if (Input.GetKey(KeyCode.J))// A�{�_��
			{
				// �Ώۂɂ���ăX�e�[�g�ύX

			}
			// �L�����Z��
			if (Input.GetKey(KeyCode.K))// B�{�^��
			{
				// �Y������`�F�b�N
				if
				(
					m_AnimationState == (int)e_PlayerAnimationState.PUSH_WAITING ||
					m_AnimationState == (int)e_PlayerAnimationState.PUSH_PUSHING ||
					m_AnimationState == (int)e_PlayerAnimationState.PULL_WAITING ||
					m_AnimationState == (int)e_PlayerAnimationState.PULL_PULLING ||
					m_AnimationState == (int)e_PlayerAnimationState.LEVER_WAITING ||
					m_AnimationState == (int)e_PlayerAnimationState.LEVER_RIGHT ||
					m_AnimationState == (int)e_PlayerAnimationState.LEVER_LEFT
				)
				{
					m_AnimationState = (int)e_PlayerAnimationState.WAITING;
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

	public int Get_AnimationState()
	{
		return m_AnimationState;
	}
	public bool Get_CanAction()
	{
		return m_CanAction;
	}
}
