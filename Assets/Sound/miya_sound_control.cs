using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miya_sound_control : MonoBehaviour
{
	// �ǂ���
	public bool BGMtrueSEfalse = true;
	
	// �\�[�X
	AudioSource Sound;

	// �{�����[��
	float Volume;

	// Start is called before the first frame update
	void Start()
    {
		// �ǂ���
		BGMtrueSEfalse = true;

		// �\�[�X
		Sound = this.GetComponent<AudioSource>();

		// �{�����[��
		Volume = Sound.volume;
		//Debug.Log(Volume);

		// �ǂ���
		if ( BGMtrueSEfalse )	Sound.volume = Volume * miya_test_UI.Magnification_BGM;
		else					Sound.volume = Volume * miya_test_UI.Magnification_SE;
		//Debug.Log(miya_test_UI.Magnification_BGM);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
