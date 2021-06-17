using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miya_test_UI : MonoBehaviour
{
	public GameObject UI_window;

	// �V�����_�[
	Slider slider_bgm;
	Slider slider_se;

	// �f�o�b�O
	bool active = false;

    // Start is called before the first frame update
    void Start()
    {
		// �V�����_�[
		GameObject back = this.transform.Find("Back").gameObject;
		slider_bgm = back.transform.Find("Slider_BGM").GetComponent<Slider>();
		slider_se = back.transform.Find("Slider_SE").GetComponent<Slider>();

		// �f�o�b�O
		active = false;
	}

    // Update is called once per frame
    void Update()
	{
		// �f�o�b�O
		{
			if (Input.GetKeyUp(KeyCode.P))
			{
				active = !active;
			}

			if (active)
			{
				Show_Window();
				Debug.Log("Show");
			}
			else
			{
				Close_Window();
				Debug.Log("Close");
			}
		}
	}

	public void Show_Window()
	{
		UI_window.SetActive(true);
		active = true;
	}
	public void Close_Window()
	{
		UI_window.SetActive(false);
		active = false;
	}

	public void Reset_Value()
	{
		slider_bgm.value = 0.5f;
		slider_se.value = 0.5f;
	}
}
