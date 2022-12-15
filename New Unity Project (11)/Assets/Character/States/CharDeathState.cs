using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharDeathState : State
{
    
    public override void Enter(Character character)
    {
        character.anim.SetTrigger("Die");
    }
    public override void Update(Character character)
    {
     
    }
    public override void FixedUpdate(Character character)
    {
    }
    public override void Exit(Character character)
    {

    }
}
