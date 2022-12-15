using Unity.VisualScripting;
using UnityEngine;

public class CharRunState : State
{
    float speed;
    Rigidbody rb;
    float footstepTimer=0.33f;
    AudioSource sound;
    public override void Enter(Character character)
    {
        character.anim.SetBool("isRunning", true);
        speed = character.speed*character.runSpeedMultiplier;
        rb = character.rb;
        sound = character.sound;
        sound.PlayOneShot(character.footstepSound, 0.7f);
    }
    public override void Update(Character character)
    {
        if (character.isGrounded())
        {
            SpeedControl();

            if (Input.GetKey(KeyCode.LeftShift) && character.animDirection == new Vector3(1, 0, 0))
            {
                character.stamina -= character.staminaDrainFac * Time.deltaTime;
                footstepTimer -= Time.deltaTime;
                if (footstepTimer <= 0)
                {
                    sound.PlayOneShot(character.footstepSound, 1.5f);
                    footstepTimer = 0.33f;
                }

            }
            else character.SwitchState(character.walkState);
        }
        else character.SwitchState(character.jumpState);
        
    }
    public override void FixedUpdate(Character character)
    {
        character.rb.AddForce(character.moveDirection * character.speed * character.runSpeedMultiplier* character.angleSpeedReduction* 500, ForceMode.Force);
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
    public override void Exit(Character character)
    {
        character.anim.SetBool("isRunning", false);
    }
}
