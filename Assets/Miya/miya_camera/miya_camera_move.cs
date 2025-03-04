using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// クリアカメラ
using Cinemachine;



public class miya_camera_move : MonoBehaviour
{
	// 参照
	public miya_player_state sc_state;

	// 定数
	[SerializeField]
	float HEIGHT_MAX = 17.5f;
	const float HEIGHT_MIN = 4.0f;

	// 変数------------------------------------------------------------------------------------------
	// 視点モード
	bool	Looking_FromUp_m	= false;
	// 基本
	float	Length_FromCenter	= 0;
	float Length_FromCenter_Current = 0;
	[SerializeField] private float Speed_Rotate = 60.0f;
	//[SerializeField] private float Speed_Height = 2.0f;
	float	Height_Default	= 0;
	float	Height			= 0;
	// 角度
	float Degree = -180;
	// タワー操作時
	float Length_FromCenter_Zoom = 7;
	// オブジェクト参照
	public GameObject GazePoint = null;
	public GameObject Tower_m = null;



	// クリアカメラ
	CinemachineVirtualCamera clear_camera;




	// 初期化--------------------------------------------------------------------------------------------
	void Start()
    {
		// 初期値取得
		Length_FromCenter = Mathf.Abs(this.transform.position.z);
		Length_FromCenter_Current = Length_FromCenter;
		Height_Default = this.transform.position.y;
		Height = Height_Default;
		Length_FromCenter_Zoom = 7;



		// クリアカメラ
		clear_camera = this.GetComponent<CinemachineVirtualCamera>();



	}





	// クリアカメラ
	public void Set_ClearCamera()
	{
		clear_camera.Priority = 10;
	}
	public void Set_DefaultCamera()
	{
		clear_camera.Priority = 50;
	}
	// デバッグ
	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.O))
		{
			Set_ClearCamera();
		}
		if (Input.GetKey(KeyCode.P))
		{
			Set_DefaultCamera();
		}
	}







	// 更新
	void Update()
	{
		// 入力
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
		{
			if (Input.GetKey(KeyCode.LeftArrow)) Degree += Speed_Rotate * Time.deltaTime;
			if (Input.GetKey(KeyCode.RightArrow)) Degree -= Speed_Rotate * Time.deltaTime;
		}
		// ゲームパッド// 原田君用2
		else if (Mathf.Abs(Input.GetAxis("Horizontal_c")) > 0)
		{
			Degree += Input.GetAxis("Horizontal_c") * Speed_Rotate * Time.deltaTime;
		}

		// 原田君用('ω')タワーのためのズーム
		if (Tower_m && sc_state.Get_AnimationState() == (int)miya_player_state.e_PlayerAnimationState.WAITING_TOWER)
		{
			// 注視点
			// 原田君用('ω')new Vector3(0, 3, 0)に変更
			Vector3 new_pos = new Vector3(0, 3, 0);
			new_pos.x = Tower_m.transform.position.x;
			new_pos.z = Tower_m.transform.position.z;
			GazePoint.transform.position = new_pos;
			// Length制御
			//if (Length_FromCenter_Current < Length_MostNear) Length_FromCenter_Current = Length_MostNear;
			//if (Length_FromCenter_Current > Length_MostFar) Length_FromCenter_Current = Length_MostFar;
			// カメラ位置
			Vector3 result = new Vector3(0, 0, 0);
			result.x = Mathf.Sin(Degree * Mathf.Deg2Rad) * Length_FromCenter_Zoom;
			result.z = Mathf.Cos(Degree * Mathf.Deg2Rad) * Length_FromCenter_Zoom;
			result.y = Height_Default;
			this.transform.position = result;
		}
		else
		{
			// 注視点
			// 原田君用('ω')new Vector3(0, 2, 0)に変更
			Vector3 new_pos = new Vector3(0, 2, 0);//注意
			GazePoint.transform.position = new_pos;

			// 移動
			if (!Looking_FromUp_m)
			{
				// 見下ろし視点へ切替
				// ゲームパッド// 原田君用2
				if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxisRaw("Change_c") == 1) Looking_FromUp_m = true;
				// 過去可変
				//if (Input.GetKey(KeyCode.UpArrow	)) Height += Speed_Height * Time.deltaTime;
				//if (Height > HEIGHT_MAX - 0.1f) Height = HEIGHT_MAX - 0.1f;

				// 移動
				Vector3 result = new Vector3(0, 0, 0);
				result.x = Mathf.Sin(Degree * Mathf.Deg2Rad) * Length_FromCenter;
				result.z = Mathf.Cos(Degree * Mathf.Deg2Rad) * Length_FromCenter;
				result.y = Height_Default;
				this.transform.position = result;
			}
			else
			{
				// 高さ
				Height = HEIGHT_MAX - 0.1f;

				// 高さの変更に伴う中央からの距離変更
				float degree = Height * 90.0f / HEIGHT_MAX;// 0.0f~10.0f = 0°~90°
				Length_FromCenter_Current = Mathf.Cos(degree * Mathf.Deg2Rad) * Length_FromCenter;// 90°= 0.0f

				// 移動
				Vector3 result = new Vector3(0, 0, 0);
				result.x = Mathf.Sin(Degree * Mathf.Deg2Rad) * Length_FromCenter_Current;
				result.z = Mathf.Cos(Degree * Mathf.Deg2Rad) * Length_FromCenter_Current;
				result.y = Height;
				this.transform.position = result;

				// 通常視点へ切替
				// ゲームパッド// 原田君用2
				if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxisRaw("Change_c") == -1) Looking_FromUp_m = false;
				// 過去可変
				//if (Input.GetKey(KeyCode.DownArrow)) Height -= Speed_Height * Time.deltaTime;
			}
		}
	}

	public void Set_Tower(GameObject _Tower)
	{
		Tower_m = _Tower;
	}
	public void Release_Tower()
	{
		Tower_m = null;
	}
}
