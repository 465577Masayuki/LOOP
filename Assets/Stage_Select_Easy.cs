using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage_Select_Easy : MonoBehaviour
{
    public Stage_Select ss;

    bool con_U; //�R���g���[���[���͏�
    bool con_D; //�R���g���[���[���͉�

    public Stage_Select_CArrow Stage_Select_CArrow;
    public Stage_Select_CArrow Stage_Select_CArrow2;
    public Image fadeImg;

    bool USE;
    bool END;
    bool OK;
    bool SELECT_END;

    public bool isPlaying = false;

    private bool ClearFade = false;
    private Color fadecolor;
    public float FadeTime = 1.0f;
    float time = 0f;
    private float start;
    private float end;

    RectTransform rt;
    float size;
    int Diray;

    const int MAX_OBJ = 3;

    int wait;
    public int Select;

    public GameObject[] Obj = new GameObject[MAX_OBJ];

    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
        size = 0.0f;
        Diray = 20;
        wait = 0;
        USE = false;
        OK = false;

        start = 0f;
        end = 1f;
        Select = 1;
        SELECT_END = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(USE)
        {
            if(Diray==0 && !OK)
            {
                size += 0.05f;
                if(size > 1)
                {
                    size = 1;
                    OK = true;
                }
            }

            if(Diray > 0)
            {
                Diray--;
            }
            
            rt.localScale = new Vector3(size, size, size);

            if (OK && !END)
            {
                if ((Input.GetKeyDown(KeyCode.K) || Input.GetButton("NO")) && wait == 0)
                {
                    END = true;
                }
            }


            if(END)
            {
                size -= 0.05f;
                if (size < 0)
                {
                    size = 0;
                    USE = false;
                    OK = false;
                    END = false;
                    rt.localScale = new Vector3(size, size, size);

                    ss.Back();
                }
            }

            Check_Cont();

            if (!SELECT_END)
            {
                if ((Input.GetKeyDown(KeyCode.J) || Input.GetButton("OK")) && wait == 0 && OK)
                {
                    OutStartFadeAnim();
                }

                if ((Input.GetKey(KeyCode.UpArrow) || con_U) && wait == 0 && Select < MAX_OBJ)
                {
                    Obj[Select - 1].GetComponent<Stage_Select_Slide>().SetDown_IN(false);
                    Obj[Select].GetComponent<Stage_Select_Slide>().SetDown_IN(true);
                    Stage_Select_CArrow.SetBig();

                    wait = 50;

                    Select++;
                }

                if ((Input.GetKey(KeyCode.DownArrow) || con_D) && wait == 0 && Select > 1)
                {
                    Obj[Select - 1].GetComponent<Stage_Select_Slide>().SetUp_IN(false);
                    Obj[Select - 2].GetComponent<Stage_Select_Slide>().SetUp_IN(true);
                    Stage_Select_CArrow2.SetBig();

                    wait = 50;

                    Select--;
                }
            }
            else if (SELECT_END)
            {

            }

            ChoiceStage();

        }


    }

    void FixedUpdate()
    {
        if (wait > 0)
        {
            wait--;
        }
    }

    public void SET_USE()
    {
        USE = true;
        Diray = 20;
        size = 0;
    }

    private void Check_Cont()
    {
        float UD;
        UD = Input.GetAxis("Vertical_p"); //��Ղ�

        con_U = false;
        con_D = false;

        if (UD > 0.5f)
        {
            con_U = true;
        }

        if (UD < -0.5f)
        {
            con_D = true;
        }
    }

    public void OutStartFadeAnim()
    {

        if (isPlaying == true)
        {
            return;
        }

        StartCoroutine(fadeinplay());
    }

    IEnumerator fadeinplay()
    {
        isPlaying = true;

        fadecolor = fadeImg.color;

        time = 0f;

        fadecolor.a = Mathf.Lerp(start, end, time);


        while (fadecolor.a <= 0.99f)

        {

            time += Time.deltaTime / FadeTime;

            fadecolor.a = Mathf.Lerp(start, end, time);

            fadeImg.color = fadecolor;

            yield return null;

        }
        ClearFade = true;
        isPlaying = false;
    }

    private void ChoiceStage()
    {

        if (ClearFade)
        {
            switch (Select)
            {
                case 1:
                    SceneManager.LoadScene("Stage_1_1");
                    break;
                case 2:
                    SceneManager.LoadScene("Stage_1_2");
                    break;
                case 3:
                    SceneManager.LoadScene("Stage_1_3");
                    break;

            }
        }
    }
}
