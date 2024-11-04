using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attack2 : Attack1
{
    public Attack2(string name, Animator an, Transform subject, Transform target, float rotateSpeed) : base(name, an, subject, target, rotateSpeed)
    {
    }

    protected override void OnStart()
    {
        animator.SetTrigger("attack2");
    }

    protected override NodeStatus OnUpdate()
    {

        var pos = new Vector3(target.position.x, subject.position.y, target.position.z) - subject.position;
        subject.rotation = Quaternion.Slerp(subject.rotation, Quaternion.LookRotation(pos), rotateSpeed * Time.deltaTime);

        if (animator.GetAnimatorTransitionInfo(0).IsName("attack2_3 -> moving_guard"))
            return NodeStatus.SUCCESS;

        return NodeStatus.RUNNING;
    }
}
