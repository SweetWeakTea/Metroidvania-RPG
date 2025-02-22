using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
            jumpCounter = 0;
            player.anim.SetInteger("JumpCounter", jumpCounter);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
            jumpCounter = 0;
            player.anim.SetInteger("JumpCounter", jumpCounter);
        }

        if (xInput != 0)
            player.SetVelocity(xInput * .8f * player.moveSpeed, rb.velocity.y);
    }
}
