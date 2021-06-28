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

	// �I�����w�i
	Image Back_BGM;
	Image Back_SE;
	Image Back_Exit;
	Image Back_Reset;

	// �{�����[��
	static public float Magnification_BGM = 0.5f;
	static public float Magnification_SE = 0.5f;

	// �f�o�b�O
	bool active = false;

	// �O���b�h����
	public float GridValue = 0.1f;


	// ����ӏ�
	enum Witch_e
	{
		BGM,
		SE,
		Exit,
		Reset
	}
	int Witch_Control = (int)Witch_e.BGM;   //�c�̈ʒu
	public GameObject Witch_Slider;
	public GameObject Witch_Button;//koko




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

		// �I�����w�i
		Back_BGM	= back.transform.Find("Back_BGM"	).GetComponent<Image>();
		Back_SE		= back.transform.Find("Back_SE"		).GetComponent<Image>(); Back_SE.enabled = false;
		Back_Exit	= back.transform.Find("Back_Exit"	).GetComponent<Image>(); Back_Exit.enabled = false;
		Back_Reset	= back.transform.Find("Back_Reset"	).GetComponent<Image>(); Back_Reset.enabled = false;
		Witch_Control = (int)Witch_e.BGM;
		

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

		if (active)
		{
			// SE�e�X�g�v���C
			if (Input.GetMouseButtonUp(0)) TestSE_audio.Play();

			// �L�[���͑Ή�															�R���g���[���[�v�ǋL
			{
				// �I���ʒu
				{
					// �c
					if (Witch_Control <= (int)Witch_e.Exit)
					{
						if (Input.GetKeyUp(KeyCode.DownArrow))
						{
							if (Witch_Control < (int)Witch_e.Exit) Witch_Control++;
						}
						if (Input.GetKeyUp(KeyCode.UpArrow))
						{
							if (Witch_Control > (int)Witch_e.BGM) Witch_Control--;
						}
					}
					// ��
					if (Witch_Control >= (int)Witch_e.Exit)
					{
						if (Input.GetKeyUp(KeyCode.RightArrow))
						{
							if (Witch_Control == (int)Witch_e.Exit) Witch_Control++;
						}
						if (Input.GetKeyUp(KeyCode.LeftArrow))
						{
							if (Witch_Control == (int)Witch_e.Reset) Witch_Control--;
						}
					}
				}
				// �X���C�h���l
				if (Witch_Control == (int)Witch_e.BGM)
				{
					if (Input.GetKeyUp(KeyCode.RightArrow))
					{
						slider_bgm.value += GridValue;
					}
					if (Input.GetKeyUp(KeyCode.LeftArrow))
					{
						slider_bgm.value -= GridValue;
					}
					if (slider_bgm.value > 1) slider_bgm.value = 1;
					if (slider_bgm.value < 0) slider_bgm.value = 0;
				}
				else if (Witch_Control == (int)Witch_e.SE)
				{
					if (Input.GetKeyUp(KeyCode.RightArrow))
					{
						slider_se.value += GridValue;
						TestSE_audio.Play();
					}
					if (Input.GetKeyUp(KeyCode.LeftArrow))
					{
						slider_se.value -= GridValue;
						TestSE_audio.Play();
					}
					if (slider_se.value > 1) slider_se.value = 1;
					if (slider_se.value < 0) slider_se.value = 0;
				}
				// �I�����w�i
				switch(Witch_Control)
				{
					case (int)Witch_e.BGM:
						Back_BGM.enabled = true;
						Back_SE.enabled = false;
						Back_Exit.enabled = false;
						Back_Reset.enabled = false;
						break;
					case (int)Witch_e.SE:
						Back_BGM.enabled = false;
						Back_SE.enabled = true;
						Back_Exit.enabled = false;
						Back_Reset.enabled = false;
						break;
					case (int)Witch_e.Exit:
						Back_BGM.enabled = false;
						Back_SE.enabled = false;
						Back_Exit.enabled = true;
						Back_Reset.enabled = false;
						break;
					case (int)Witch_e.Reset:
						Back_BGM.enabled = false;
						Back_SE.enabled = false;
						Back_Exit.enabled = false;
						Back_Reset.enabled = true;
						break;
				}
				// �I���AExit,Reset
				if (Input.GetKeyUp(KeyCode.Return))
				{
					if (Witch_Control == (int)Witch_e.Exit)
					{
						Close_Window();
						TestSE_audio.Play();
					}
					if (Witch_Control == (int)Witch_e.Reset)
					{
						Reset_Value();
						TestSE_audio.Play();
					}
				}
			}
		}

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

		Witch_Control = (int)Witch_e.BGM;
	}

	public void Reset_Value()
	{
		slider_bgm.value = 0.5f;
		slider_se.value = 0.5f;
	}

    //��L�[���͂ɂ��Ăяo��
    public void UpKey()
    {
        if(Witch_Control == (int)Witch_e.Reset)
        {
            return;
        }

        Witch_Control--;

        if (Witch_Control == -1)
        {
            Witch_Control = (int)Witch_e.Exit;
        }
    }

    //���L�[���͂ɂ��Ăяo��
    public void DownKey()
    {
        if (Witch_Control == (int)Witch_e.Reset)
        {
            return;
        }

        Witch_Control++;

        if (Witch_Control == (int)Witch_e.Reset)
        {
            Witch_Control = (int)Witch_e.BGM;
        }
    }

    //�E�L�[���͂ɂ��Ăяo��
    public void RightKey()
    {
        //�ŉ��i�ɂ���ꍇ
        if (Witch_Control == (int)Witch_e.Exit || Witch_Control == (int)Witch_e.Reset)
        {
            if (Witch_Control == (int)Witch_e.Exit)
            {
                Witch_Control = (int)Witch_e.Reset;
            }
            else
            {
                Witch_Control = (int)Witch_e.Exit;
            }
        }
        else
        {
            //���ʐ؂�ւ��̏ꍇ
            if (Witch_Control == (int)Witch_e.BGM)
            {
                slider_bgm.value += GridValue;
                if (slider_bgm.value > 1) slider_bgm.value = 1;
            }

            if (Witch_Control == (int)Witch_e.SE)
            {
                
                slider_se.value += GridValue;
                if (slider_se.value > 1) slider_se.value = 1;
                TestSE_audio.Play();               
            }
        }
    }

    //���L�[���͂ɂ��Ăяo��
    public void LeftKey()
    {
        //�ŉ��i�ɂ���ꍇ
        if (Witch_Control == (int)Witch_e.Exit || Witch_Control == (int)Witch_e.Reset)
        {
            if (Witch_Control == (int)Witch_e.Exit)
            {
                Witch_Control = (int)Witch_e.Reset;
            }
            else
            {
                Witch_Control = (int)Witch_e.Exit;
            }
        }
        else
        {
            //���ʐ؂�ւ��̏ꍇ
            if (Witch_Control == (int)Witch_e.BGM)
            {
                slider_bgm.value -= GridValue;
                if (slider_bgm.value < 0) slider_bgm.value = 0;
            }

            if (Witch_Control == (int)Witch_e.SE)
            {
                slider_se.value -= GridValue;
                if (slider_se.value < 0) slider_se.value = 0;
                TestSE_audio.Play();
            }
        }
    }

    public bool ActionKey()
    {
        if (Witch_Control == (int)Witch_e.Exit)
        {
            Close_Window();
            TestSE_audio.Play();
            return true;
        }
        if (Witch_Control == (int)Witch_e.Reset)
        {
            Reset_Value();
            TestSE_audio.Play();
        }

        return false;
    }
}
