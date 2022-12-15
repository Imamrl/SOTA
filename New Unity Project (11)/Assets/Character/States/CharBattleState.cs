using System;
using System.Collections;
using UnityEngine;

public class CharBattleState : State
{
    Animator anim;
    Sword sword;
    float time;
    float exitDelay;
    float speed;
    Rigidbody rb;
    AudioSource sound;
    public override void Enter(Character character)
    {
        anim = character.anim;
        rb = character.rb;
        speed = character.speed * 0.5f;
        sword = character.sword;
        sound= character.sound;
        if (Input.GetButtonDown("Fire1")) 
        {
            exitDelay = 0.75f;
            sword.dealDamage = true;
            sword.damage = 40;
            anim.SetTrigger("attack");
            character.PlaySoundDelayed(character.attackSound, 0.3f, 0.05f);
        }
        
        if (Input.GetButtonDown("Fire2")) 
        {
            exitDelay = 1f;
            sword.dealDamage = true;
            sword.damage = 80;
            anim.SetTrigger("heavyAttack");
            character.PlaySoundDelayed(character.attackSound, 0.7f, 0.2f);
        } 
        time = Time.time;
        
    }
    public override void Update(Character character)
    {
        BattleSystem(character);
        SpeedControl();
        if (Time.time - time > exitDelay)
        {
            sword.dealDamage = false;
            if (character.moveDirection.magnitude != 0) character.SwitchState(character.walkState);
            if (Input.GetKey(KeyCode.R)) character.SwitchState(character.blockState);
        }
    }
    void SpeedControl()
    {
        Vector3 velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (velocity.magnitude > speed)
        {
            Vector3 limitedVelocity = velocity.normalized * speed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
    public override void FixedUpdate(Character character)
    {
        character.rb.AddForce(character.moveDirection * character.speed * character.angleSpeedReduction * 500, ForceMode.Force);
    }
    public override void Exit(Character character)
    {
    }
    private void BattleSystem(Character character)
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            
            anim.SetTrigger("attack");
            exitDelay = 0.75f;
            sword.dealDamage = true;
            sword.damage = 40;
            character.PlaySoundDelayed(character.attackSound, 0.3f, 0.05f);
        }
        if (Input.GetButtonDown("Fire2"))
        {

            exitDelay = 1f;
            sword.dealDamage = true;
            anim.SetTrigger("heavyAttack");
            sword.damage = 80;
            character.PlaySoundDelayed(character.attackSound, 0.7f, 0.2f);
        }
        if (Input.GetKey(KeyCode.R))
        {
            anim.SetBool("isBlocking", true);
        }
        else anim.SetBool("isBlocking", false);
    }
    
}
