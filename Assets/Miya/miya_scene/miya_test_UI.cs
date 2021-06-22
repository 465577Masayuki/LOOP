using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miya_test_UI : MonoBehaviour
{
	public GameObject UI_window;

	// �e�X�g�T�E���h
	public GameObject TestBGM;
	public GameObject TestSE;
	AudioSource TestBGM_audio;
	AudioSource TestSE_audio;
	float FirstVolume_BGM;
	float FirstVolume_SE;

	// �V�����_�[
	Slider slider_bgm;
	Slider slider_se;

	// �{�����[��
	static public float Magnification_BGM = 0.5f;
	static public float Magnification_SE = 0.5f;

	// �f�o�b�O
	bool active = false;

    // Start is called before the first frame update
    void Start()
	{
		// �e�X�g�T�E���h
		if (TestBGM) TestBGM_audio = TestBGM.GetComponent<AudioSource>();
		if (TestSE) TestSE_audio = TestSE.GetComponent<AudioSource>();
		FirstVolume_BGM = TestBGM_audio.volume;
		FirstVolume_SE = TestSE_audio.volume;

		// �V�����_�[
		GameObject back = this.transform.Find("Back").gameObject;
		slider_bgm = back.transform.Find("Slider_BGM").GetComponent<Slider>();
		slider_se = back.transform.Find("Slider_SE").GetComponent<Slider>();

		// �{�����[��
		Magnification_BGM = 0.5f;
		Magnification_SE = 0.5f;

		// �f�o�b�O
		active = false;
	}
	
	// Update is called once per frame
	void Update()
	{
		// �{�����[��
		Magnification_BGM = slider_bgm.value;
		Magnification_SE = slider_se.value;

		// �e�X�g�v���C
		TestBGM_audio.volume = FirstVolume_BGM * Magnification_BGM;
		TestSE_audio.volume = FirstVolume_SE * Magnification_SE;

		// SE�e�X�g�v���C
		if (Input.GetMouseButtonUp(0)) TestSE_audio.Play();

		// �f�o�b�O
		{
			if (Input.GetKeyUp(KeyCode.P))
			{
				active = !active;
			}

			if (active)
			{
				Show_Window();
				//Debug.Log("Show");
			}
			else
			{
				Close_Window();
				//Debug.Log("Close");
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
