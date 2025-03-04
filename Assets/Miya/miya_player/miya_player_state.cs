// ギミック操作時キャンセル待ちに

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_state : MonoBehaviour
{
	// 参照
	public miya_player_move sc_move;
	public miya_forword		sc_forword;
	public miya_check		sc_check;

	// 列挙
	public enum e_PlayerAnimationState
	{
		WAITING,        // 待機
		WAITING_TOWER,	// タワー操作
		WALKING,		// 歩き
		ABANDONED,		// 放置
		RUNNING,		// 走る
		CLIMBING,		// よじ登る
		PUSH_WAITING,	// 押す待機
		PUSH_PUSHING,	// 押す
		PULL_WAITING,	// 引く待機
		PULL_PULLING,	// 引く
		LEVER_WAITING,	// レバー待機
		LEVER_RIGHT,	// レバー右
		LEVER_LEFT,		// レバー左
		HOVERING,		// 空中
		LANDING,		// 着地
	}

	// 変数
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

	// 原田君用('ω')
	public GameObject m_parent;

	// デバッグ用
	int state_past = (int)e_PlayerAnimationState.WAITING;

	// 原田君用3
	bool IsRunning = false;
	public void Set_IsRunning(bool _is) { IsRunning = _is; }

	// 初期化
	void Start()
	{
		// Rigidbody取得
		Rigid = this.GetComponent<Rigidbody>();
	}

	// 更新
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

		// デバッグ
		if (state_past != m_AnimationState)
		{
			state_past = m_AnimationState;
			Debug.Log("Animation State：" + m_AnimationState);
		}
		// 佐々木デバッグ用
		//Debug.Log("Horizontal_p：" + Input.GetAxis("Horizontal_p"));
		//Debug.Log("Vertical_p：" + Input.GetAxis("Vertical_p"));
		//Debug.Log("Horizontal_c：" + Input.GetAxis("Horizontal_c"));
		//Debug.Log("Change_c：" + Input.GetAxisRaw("Change_c"));
		//Debug.Log("OK：" + Input.GetButton("OK"));
		//Debug.Log("NO：" + Input.GetButton("NO"));
		//Debug.Log("Run：" + Input.GetButton("Run"));
		//Debug.Log("Climb：" + Input.GetButton("Climb"));
		//Debug.Log("Menu：" + Input.GetButton("Menu"));

		// アクション可能
		if ( m_CanAction )
		{
			// 何もしていない
			m_AnimationState = (int)e_PlayerAnimationState.WAITING;

			// 歩行
			if
			(
			Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
			Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
			)
			{
				// 原田君用3変更
				if (!IsRunning) m_AnimationState = (int)e_PlayerAnimationState.WALKING;
				else m_AnimationState = (int)e_PlayerAnimationState.RUNNING;
			}
			// ゲームパッド// 原田君用2// camera_moveにも同じように変更あるので注意お願いします(´ω`)
			else if (Mathf.Abs(Input.GetAxis("Vertical_p")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal_p")) > 0)
			{
				// 原田君用3変更
				if (!IsRunning) m_AnimationState = (int)e_PlayerAnimationState.WALKING;
				else m_AnimationState = (int)e_PlayerAnimationState.RUNNING;
			}

			// デバッグ
			//Debug.Log("F : " + m_CanClimb_forword);
			//Debug.Log("C : " + m_CanClimb_check);

			// よじ登る
			// 原田君用2 || で繋げてるのが以下に複数あります！
			if (Input.GetKey(KeyCode.Space) || Input.GetButton("Climb"))
			{
				// 登れるものがあれば
				if (m_CanClimb_forword && !m_CanClimb_check)
				{
					m_AnimationState	= (int)e_PlayerAnimationState.CLIMBING;
					m_CanAction			= false;

					// 原田君用('ω')ワープだからいらなくなったね
					//Rigid.useGravity	= false;

					sc_move.Set_StartPosition(this.transform.position);
				}
			}

			// 作動
			if (Input.GetKey(KeyCode.J) || Input.GetButton("OK"))
			{
				// 対象によってステート変更
				// ブロック
				if (IsBlock)
				{
					m_AnimationState = (int)e_PlayerAnimationState.PUSH_WAITING;
					m_CanAction = false;

					// 原田君用('ω')
					// ブロックをプレイヤーの子に
					if (sc_forword.Get_Block())
					{
						sc_forword.Get_Block().transform.parent = this.transform;
						// プレイヤーを中心軸の子に
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
			// 押す
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
				// ゲームパッド// 原田君用2
				else if (Mathf.Abs(Input.GetAxis("Vertical_p")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal_p")) > 0)
				{
					m_AnimationState = (int)e_PlayerAnimationState.PUSH_PUSHING;
				}
			}
			else if (m_AnimationState == (int)e_PlayerAnimationState.PUSH_PUSHING)
			{
				// ゲームパッド// 原田君用2
				if
				(
				!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) &&
				!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) &&
				Mathf.Abs(Input.GetAxis("Vertical_p")) == 0 && Mathf.Abs(Input.GetAxis("Horizontal_p")) == 0
				)
				{
					m_AnimationState = (int)e_PlayerAnimationState.PUSH_WAITING;
				}
			}
		}

		// キャンセル
		if (Input.GetKey(KeyCode.K) || Input.GetButton("NO"))
		{
			// 該当動作チェック
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

	// 定期更新
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
