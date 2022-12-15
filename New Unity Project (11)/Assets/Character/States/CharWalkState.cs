using UnityEngine;

public class CharWalkState : State
{
    float speed;
    Rigidbody rb;
    Animator anim;
    AudioSource sound;
    float footstepTimer=0.5f;
    public override void Enter(Character character) 
    {
        speed=character.speed;
        rb= character.rb;
        anim= character.anim;
        sound= character.sound;
        anim.SetFloat("walkingSpeed", 1);
    }
    public override void Update(Character character) 
    {
        if (character.isGrounded())
        {
            SpeedControl();
            footstepTimer-=Time.deltaTime;
            if (footstepTimer <= 0 && character.moveDirection.magnitude>0.2f) 
            {
                sound.PlayOneShot(character.footstepSound, 1f);
                footstepTimer = 0.5f;
            }
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) 
            {
                character.SwitchState(character.battleState);
            }
            if (Input.GetKey(KeyCode.R)) 
            {
                character.SwitchState(character.blockState);
                Debug.Log("Block");
            }
            if (Input.GetKey(KeyCode.LeftShift) && character.animDirection == new Vector3(1, 0, 0))
            {
                character.SwitchState(character.runState);
            }
        }
        else character.SwitchState(character.jumpState);
    }
    public override void FixedUpdate(Character character)
    {
        character.rb.AddForce(character.moveDirection * character.speed* character.angleSpeedReduction*500, ForceMode.Force);
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

    }
}
