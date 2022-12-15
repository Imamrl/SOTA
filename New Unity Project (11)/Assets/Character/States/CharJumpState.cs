using Unity.VisualScripting;
using UnityEngine;

public class CharJumpState : State
{
    public override void Enter(Character character)
    {
    }
    public override void Update(Character character)
    {
        if (character.isGrounded()) 
        {
            character.SwitchState(character.walkState);
        }
    }
    public override void FixedUpdate(Character character)
    {
    }
    public override void Exit(Character character)
    {
        //character.health -= -character.rb.velocity.y * 10;
    }
}
