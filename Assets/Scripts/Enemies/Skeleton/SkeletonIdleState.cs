using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
    public SkeletonIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();

        //AudioManager.instance.PlaySFX(14, enemy.transform); // û�� Audio ���ز�
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0f)
            stateMachine.ChangeState(enemy.moveState);
    }
}
