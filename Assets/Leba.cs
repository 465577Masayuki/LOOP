using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leba : MonoBehaviour
{
    public GameObject FloorOne;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpinL()
    {
        FloorOne.GetComponent<SPIN_FloorOne>().SetSpin(-1);
    }

    public void SpinR()
    {
        FloorOne.GetComponent<SPIN_FloorOne>().SetSpin(1);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("���o�[");
        if (other.gameObject.CompareTag("Player"))
        {
            //other.GetComponent<Player_Move>().SetHIT_LEVER();
            other.GetComponent<Player>().SetHIT_LEVER();
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("���o�[����");
        if (other.gameObject.CompareTag("Player"))
        {
            //.GetComponent<Player_Move>().ClearHIT_LEVER();
            other.GetComponent <Player>().ClearHIT_LEVER();
        }
    }
}
