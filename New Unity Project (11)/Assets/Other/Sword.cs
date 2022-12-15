using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool dealDamage;
    public float damage;
    public AudioSource hitSound;
    // Start is called before the first frame update
    void Start()
    {
        hitSound= GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy" && dealDamage && !other.isTrigger)
        {
            hitSound.Play();
            Teleport enemy = other.gameObject.GetComponent<Teleport>();
            enemy.HP -= damage;
            dealDamage = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
