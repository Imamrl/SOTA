using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Capsule;
    private Transform player;
    public int M = 0;
    public int T = 3;
    public int u = 0;
    public int y = 0;
    public int e = 10;
    public int er = 0;
    public void Start()
    {
        player = GameObject.Find("Capsule").transform;
    }
    private void OnTriggerStay(Collider other)
    {
        y++;
        if (y == 100)
        {
            Go.HP = Go.HP - e;
            er = Go.HP;
            e = e + 10;
            y = 0;
        }
        if (u == 0)
        {
            if (Input.GetKey(KeyCode.W))
            {
                M = M + 1;
            }
            if (M == 20)
            {
                M = 0;
                u = 1;
                Capsule.transform.position = player.position + (Capsule.transform.forward * UnityEngine.Random.Range(0, 30));
            }
        }
        else
        {
            M = M + 1;
            if (M == 80)
            {
                M = 0;
                u = 0;
            }
        }

    }
}