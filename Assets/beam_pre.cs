﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam_pre : MonoBehaviour
{
    public Beam_ef beam_Ef;
    LineRenderer LineRenderer;

    CRISTAL cristal;
    bool HIT_CRISTAL;
    int color_ID;

    int layerMask = ~(1 << 2 | 1 << 10);    //イグノアとダミーを回避
    void Start()
    {
        LineRenderer = this.GetComponent<LineRenderer>();

        var positions = new Vector3[]{
        new Vector3(0, 0, 0),               // 開始点
        new Vector3(8, 0, 0),               // 終了点
        };

        LineRenderer.startWidth = 0.1f;                   // 開始点の太さを0.1にする
        LineRenderer.endWidth = 0.1f;                     // 終了点の太さを0.1にする

        // 線を引く場所を指定する
        LineRenderer.SetPositions(positions);

        HIT_CRISTAL = false;
    }


    void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, 10.0f))
        // {
        //    Debug.Log(hit.collider.gameObject.transform.position);
        //}

        Vector3 Pos_base = transform.position;
        Vector3 Pos_End = transform.position;
        Vector3 Ditector = transform.forward;   //new Vector3(0, 0, -1);

        RaycastHit hit;
        if (Physics.Raycast(Pos_base, Ditector, out hit, 30.0f, layerMask))
        {
            //Debug.Log(hit.collider.gameObject.transform.position);
            //Pos_End = hit.collider.gameObject.transform.position;
            Pos_End = hit.point;
            //Debug.Log(hit.point);
            //Debug.Log(hit.collider.gameObject);
            if(hit.collider.CompareTag("Door"))
            {
                hit.collider.gameObject.GetComponent<Door>().HitLaser(color_ID);
            }

            if (hit.collider.CompareTag("HOLE"))
            {
                hit.collider.gameObject.GetComponent<TOWER_HOLE>().HitLaser();
            }

            if (hit.collider.CompareTag("MIRROR"))
            {
                hit.collider.gameObject.GetComponent<MIRROR>().SetBasepos(hit.point,Ditector);
                hit.collider.gameObject.GetComponent<MIRROR>().HitLaser(color_ID);
            }

            if (hit.collider.CompareTag("CRISTAL"))
            {
                //hit.collider.gameObject.GetComponent<CRISTAL>().HitCristal();
                cristal = hit.collider.gameObject.GetComponent<CRISTAL>();
                HIT_CRISTAL = true;
            }

            if(hit.collider.CompareTag("Bridge"))
            {
                hit.collider.gameObject.GetComponent<Bridge>().HitLaser();
            }

        }
        else
        {
            Pos_End = transform.position + Ditector * 30;
        }

        DoorController.CollideWithRayOpenDoor(Pos_base, Ditector);

        var positions = new Vector3[]{
        transform.position,               // 開始点
        transform.position + Ditector * 5,               // 終了点
        };

        Vector3 len = Pos_End - transform.position;

        beam_Ef.setlen(len.magnitude * 15);

        LineRenderer.SetPositions(positions);
        LineRenderer.SetPosition(1,Pos_End);
    }

    void FixedUpdate()
    {
        if (HIT_CRISTAL)
        {
            cristal.SetHit();
        }

        HIT_CRISTAL = false;
    }

    public void Setrot(Quaternion rot)
    {
        transform.rotation = rot;
    }

    public void Set_Color_ID(int id)
    {
        color_ID = id;
    }
}
