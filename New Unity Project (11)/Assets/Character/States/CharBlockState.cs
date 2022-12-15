using UnityEngine;

public class CharBlockState : State
{
    Animator anim;
    float speed;
    Rigidbody rb;
    public override void Enter(Character character)
    {
        anim = character.anim;
        speed = character.speed*0.5f;
        rb= character.rb;
        anim.SetBool("isBlocking", true);
        anim.SetFloat("walkingSpeed", 0.5f);
    }
    public override void Update(Character character)
    {
        SpeedControl();
        if (!Input.GetKey(KeyCode.R)) character.SwitchState(character.walkState);
    }
    public override void FixedUpdate(Character character)
    {
        character.rb.AddForce(character.moveDirection * character.speed * character.angleSpeedReduction * 500, ForceMode.Force);
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
        anim.SetBool("isBlocking", false);
    }
}
