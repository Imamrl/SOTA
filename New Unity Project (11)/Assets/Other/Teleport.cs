using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Capsule;
    public GameObject projectile;
    public float projectileSpeed;
    private Transform player;
    public Character character;
    private float maxAttackRange=5f;
    private float lastAttackT;
    private float attackDelay=2f;
    private int attackCount=0;
    public float HP = 100;
    public int M = 0;
    public int T = 3;
    public int u = 0;
    public int y = 0;
    public int e = 10;
    public int er = 0;
    public void Start()
    {
        player = GameObject.Find("FinalCharacter").transform;

    }
    private void OnTriggerStay(Collider other)
    {
        
        if (u == 0)
        {
            if (Input.GetKey(KeyCode.W))
            {
                M = M + 1;
            }
            if (M == 200)
            {
                M = 0;
                u = 1;
                Capsule.transform.position = player.position + (Capsule.transform.forward * UnityEngine.Random.Range(0, 10))+Vector3.up*1.2f;
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
    private void Update()
    {
        //Debug.Log(HP);
        if (HP <= 0) Destroy(GameObject.Find("Totem"));
        transform.LookAt(character.transform.position+Vector3.up*1.5f);
        Attack();
    }
    private void Attack()
    {
     
            if ((transform.position - character.transform.position).magnitude <= maxAttackRange && Time.time - lastAttackT > attackDelay)
            {
                Rigidbody rb = Instantiate(projectile, transform.position, transform.rotation).GetComponent<Rigidbody>();
                rb.velocity =-(transform.position - character.transform.position-Vector3.up*1.5f).normalized * projectileSpeed;
                lastAttackT = Time.time;
                attackCount++;
            }
      
        
    }
}