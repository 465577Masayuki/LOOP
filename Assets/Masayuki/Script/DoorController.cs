using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�h�A�Ƀ��[�U�������������ɁA�h�A���J�������̊Ǘ�
//�h�A�ƃ��C�Ƃ̓����蔻����s���R���C�_�[��Layer��Ignore Raycast��Tag��RayCollederDoor���Z�b�g����K�v������܂�
public class DoorController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�`�悳�����̃h�A")]
    private GameObject m_visible_door = default;
    [SerializeField]
    [Tooltip("���C�Ƃ̓����蔻��p�̃h�A")]
    private GameObject m_ray_collider_door = default;
    [SerializeField]
    [Tooltip("���C�������������Ƀh�A���J����")]
    private bool m_open_door_when_ray_hit = true;
    [SerializeField]
    [Tooltip("�h�A�����b�ŊJ���悤�ɂ��邩")]
    private float m_move_time = 3.0f;
    [SerializeField]
    [Tooltip("�h�A�̓��������BY�������ɓ����܂�")]
    private float m_moving_distance = -2.05f;
    [SerializeField]
    [Tooltip("���̃��X�g�Ƀ{�^������ꂽ�ꍇ�́A���X�g���ɂ���ǂ̃{�^���������Ă��h�A���J���܂��B")]
    private List<GameObject> m_or_buttons = default;
    [SerializeField]
    [Tooltip("���̃��X�g�Ƀ{�^������ꂽ�ꍇ�́A���X�g���ɂ���S�Ẵ{�^���������Ȃ���΃h�A���J���܂���B")]
    private List<GameObject> m_and_buttons = default;


   

    private MoveController m_move_controller = default;

    private bool m_collision_with_light = false;
    //�{�^����������ăh�A���J���������s���Ă���ꍇtrue
    private bool m_request_open_door_from_button = false;
    //���C�ƏՓ˂��Ă�����h�A���J��
    //details   ��ԋ߂��h�A�̓����蔻��ɓ�����ƈȍ~�A������Ƀh�A�̓����蔻�肪�����Ă��h�A���J���������s���܂���B
    //          �܂��A�h�A�ƃ��C�Ƃ̓����蔻����s���R���C�_�[��Layer��Ignore Raycast��Tag��RayCollederDoor���Z�b�g����K�v������܂��B
    public static void CollideWithRayOpenDoor(Vector3 origin, Vector3 direction)
    {
        int layer = 0;
        layer = ~layer;
        RaycastHit[] hit = Physics.RaycastAll(origin, direction, 30.0f, layer);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("RayColliderDoor"))
            {
                DoorController temp = hit[i].collider.transform.parent.GetComponent<DoorController>();
                if (temp.m_open_door_when_ray_hit == true)
                {
                    temp.OpenDoor();
                    temp.m_collision_with_light = true;
                    return;
                }
            }
        }
        
        //GameObject[] ray_collider_doors = GameObject.FindGameObjectsWithTag("RayColliderDoor");
        //foreach (GameObject ray_collider_door in ray_collider_doors)
        //{
        //    DoorController temp = ray_collider_door.transform.parent.GetComponent<DoorController>();
            
    }

    // Start is called before the first frame update
    private void Start()
    {
        Vector3 init_pos = m_ray_collider_door.transform.position;
        Vector3 finish_pos = CalcFinshPos();
        m_move_controller = new MoveController(m_visible_door.transform, init_pos,finish_pos,m_move_time);
    }

    // Update is called once per frame
    private void Update()
    {
        m_move_controller.SetInitPos(m_ray_collider_door.transform.position);
        m_move_controller.SetFinishPos(CalcFinshPos());
        CheckButton();
    }

    private void LateUpdate()
    {
            if (m_collision_with_light == false && m_request_open_door_from_button == false)
            {
                CloseDoor();
            }
        
        m_collision_with_light = false;
        m_request_open_door_from_button = false;
    }

    private void OpenDoor()
    {
        m_move_controller.MoveFinishPos();
        
    }

    private void CloseDoor()
    {
        m_move_controller.MoveInitPos();
    }

    private Vector3 CalcFinshPos()
    {
        Vector3 pos = m_ray_collider_door.transform.position;
        pos  += m_ray_collider_door.transform.up * m_moving_distance;
        return pos;
    }

    private void CheckButton()
    {
        bool can_open_door = false;
        foreach (var and_button in m_and_buttons)
        {
            if (and_button.GetComponent<ButtonController>().PressingButton() == false)
            {
                can_open_door = false;
                break;
            }
            else
            {
                can_open_door = true;
            }
        }
        foreach (var or_button in m_or_buttons)
        {
            if(or_button.GetComponent<ButtonController>().PressingButton() == true)
            {
                can_open_door = true;
            }
        }
        if (can_open_door == true && m_collision_with_light == false)
        {
            m_request_open_door_from_button = true;
            OpenDoor();
        }
    }
}
