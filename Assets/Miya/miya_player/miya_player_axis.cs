using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_player_axis : MonoBehaviour
{
	public miya_player_state sc_state;
	// 変数
	public GameObject child;
	public float len = 1.0f;
	Vector3 Position_Latest_m;
	[SerializeField]
	private float RotateSpeed = 1;
	[SerializeField]
	private float Rotate_Tolerance_Block = 0.1f;

	// Start is called before the first frame update
	void Start()
	{
		// 過去の位置
		Position_Latest_m = this.transform.position;
	}

    // Update is called once per frame
    void FixedUpdate()
	{
		// 原田君用('ω')
		if (sc_state.Get_AnimationState() == (int)miya_player_state.e_PlayerAnimationState.PUSH_PUSHING)
		{
			// 情報
			Vector3 difference = this.transform.position - Position_Latest_m;
			Position_Latest_m = this.transform.position;
			// 位置回転更新
			child.transform.parent = null;

			Vector3 pos = new Vector3(0, 0, 0);
			pos = child.transform.position;
			pos += child.transform.forward * len;
			this.transform.position = pos;
			this.transform.rotation = child.transform.rotation;

			child.transform.parent = this.transform;

			// 回転
			difference.y = 0;
			if (difference.magnitude > Rotate_Tolerance_Block)
			{
				// 回転計算//親を回転
				Quaternion rot = Quaternion.LookRotation(difference);
				rot = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * RotateSpeed);
				this.transform.rotation = rot;
			}//difference.magnitude > Rotate_Tolerance
		}
	}
}
