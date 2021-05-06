using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_state : MonoBehaviour
{
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
	int		m_AnimationState	= (int)e_PlayerAnimationState.WAITING;
	//bool	m_IsClockwise		= true;
	// �f�o�b�O�p
	int state_past = (int)e_PlayerAnimationState.WAITING;

	// ������
	void Start()
	{

	}

	// �X�V
	void Update()
	{
		if (state_past != m_AnimationState)
		{
			state_past = m_AnimationState;
			Debug.Log("Animation State�F" + m_AnimationState);
		}

		// �ؑ�
		if
		(
		Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
		Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
		) 
		m_AnimationState = (int)e_PlayerAnimationState.WALKING;

		if (Input.GetKey(KeyCode.J))// A�{�_��
		{
			// �Ώۂɂ���ăX�e�[�g�ύX

		}
		if (Input.GetKey(KeyCode.K))// B�{�^��
		{
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

	// ����X�V
	void FixedUpdate()
	{

	}
}
